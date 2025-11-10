using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace WebViewRequestCapture
{
    public partial class MainForm : Form
    {
        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        public static extern int ExtractIconEx(string lpszFile, int nIconIndex,

        IntPtr[] phiconLarge, IntPtr[] phiconSmall, int nIcons);

        private Dictionary<string, object> _requestHeaders = new();
        private Dictionary<int, Dictionary<string, string>> respostas = new();

        public MainForm()
        {
            InitializeComponent();
            Text = Properties.Settings.Default.APP_Title;
            Icon = GetIconFromExe(Application.ExecutablePath);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InicializarWebView2();
            IniciarServidorApi();
        }

        private void CoreWebView2_DOMContentLoaded(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2DOMContentLoadedEventArgs e)
        {
            webView.CoreWebView2.DOMContentLoaded -= CoreWebView2_DOMContentLoaded;

            IniciarCaptura();

            if (Properties.Settings.Default.Auto_Refresh > 0)
            {
                var timer = new System.Windows.Forms.Timer();
                timer.Interval = Properties.Settings.Default.Auto_Refresh * 1000;
                timer.Tick += (sender, args) =>
                {
                    webView.CoreWebView2.Reload();
                };
                timer.Start();
            }
        }

        private async void InicializarWebView2()
        {
            webView = new WebView2
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(webView);

            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.DOMContentLoaded += CoreWebView2_DOMContentLoaded;
            webView.CoreWebView2.Navigate(Properties.Settings.Default.Start_Page);

            webView.BringToFront();
            webView.Focus();
        }

        private async void IniciarCaptura()
        {
            await webView.CoreWebView2.CallDevToolsProtocolMethodAsync("Network.enable", "{}");

            webView.CoreWebView2
                .GetDevToolsProtocolEventReceiver("Network.requestWillBeSent")
                .DevToolsProtocolEventReceived += async (sender, args) =>
                {
                    var json = JObject.Parse(args.ParameterObjectAsJson);
                    var request = json["request"];
                    var url = request?["url"]?.ToString();
                    var requestId = json["requestId"]?.ToString();

                    if (!string.IsNullOrEmpty(url) && url.StartsWith(Properties.Settings.Default.URL_Filter) && !string.IsNullOrEmpty(requestId))
                    {
                        object? headers = request?["headers"];
                        if (headers == null)
                        {
                            return;
                        }

                        Dictionary<string, object>? requestHeaders = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(headers.ToString());
                        if (requestHeaders?.ContainsKey("Autorizacao-Usuario") ?? false)
                        {
                            var cookie = await webView.CoreWebView2.CallDevToolsProtocolMethodAsync(
                                "Network.getCookies",
                                $"{{\"urls\":[\"{url}\"]}}"
                            );
                            if (cookie == null)
                            {
                                return;
                            }

                            Dictionary<string, object>? cookies = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(cookie);
                            if (cookies == null)
                            {
                                return;
                            }

                            if (cookies.TryGetValue("cookies", out var cookiesValue) && cookiesValue != null)
                            {
                                List<Dictionary<string, object>>? cookiesList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(cookiesValue.ToString());
                                if (cookiesList == null)
                                {
                                    return;
                                }

                                requestHeaders["Cookie"] = string.Join("; ", cookiesList.Select((cookie) => $"{cookie["name"]}={cookie["value"]}"));
                                _requestHeaders = requestHeaders;
                            }
                        }
                    }
                };

            webView.CoreWebView2
                .GetDevToolsProtocolEventReceiver("Network.responseReceived")
                .DevToolsProtocolEventReceived += async (sender, args) =>
                {
                    var json = JObject.Parse(args.ParameterObjectAsJson);
                    var response = json["response"];
                    var requestId = json["requestId"]?.ToString();
                    var mimeType = response?["mimeType"]?.ToString();
                    var url = response?["url"]?.ToString();

                    if (mimeType?.Contains(Properties.Settings.Default.Mime_Type_Filter) == true &&
                        !string.IsNullOrEmpty(url) &&
                        url.Contains(Properties.Settings.Default.URL_Filter) &&
                        requestId != null)
                    {
                        try
                        {
                            var resultado = await webView.CoreWebView2.CallDevToolsProtocolMethodAsync(
                                "Network.getResponseBody",
                                $"{{\"requestId\":\"{requestId}\"}}"
                            );

                            Dictionary<string, string> dcDados = new Dictionary<string, string>()
                            {
                                { "requestId", requestId },
                                { "url", url },
                                { "mimeType", mimeType },
                                { "response", resultado.ToString() }
                            };

                            respostas[respostas.Count + 1] = dcDados;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao capturar resposta: {ex.Message}");
                        }
                    }
                };
        }

        private void IniciarServidorApi()
        {
            if (Properties.Settings.Default.API_Port == 0)
            {
                return;
            }

            Task.Run(() =>
            {
                var builder = WebApplication.CreateBuilder();
                var app = builder.Build();

                app.MapGet("/requestheaders", (HttpContext context) =>
                {
                    if (!context.Request.Headers.TryGetValue("key", out var key) || key != Properties.Settings.Default.API_Key)
                    {
                        return Results.Unauthorized();
                    }

                    return Results.Content(Newtonsoft.Json.JsonConvert.SerializeObject(_requestHeaders), "application/json", Encoding.UTF8);
                });

                app.MapGet("/count", (HttpContext context) =>
                {
                    if (!context.Request.Headers.TryGetValue("key", out var key) || key != Properties.Settings.Default.API_Key)
                    {
                        return Results.Unauthorized();
                    }

                    return Results.Content(respostas.Count.ToString(), "text/plain", Encoding.UTF8);
                });

                app.MapGet("/request/{index:int}", (HttpContext context, int index) =>
                {
                    if (!context.Request.Headers.TryGetValue("key", out var key) || key != Properties.Settings.Default.API_Key)
                    {
                        return Results.Unauthorized();
                    }

                    if (respostas.TryGetValue(index, out var resposta))
                    {
                        return Results.Content(Newtonsoft.Json.JsonConvert.SerializeObject(resposta), "application/json", Encoding.UTF8);
                    }
                    else
                    {
                        return Results.Content(null, "application/json", Encoding.UTF8);
                    }
                });

                app.Run($"http://localhost:{Properties.Settings.Default.API_Port}");
            });
        }

        public static Icon GetIconFromExe(string path, bool largeIcon = true)
        {
            try
            {
                IntPtr[] large = new IntPtr[1];
                IntPtr[] small = new IntPtr[1];

                ExtractIconEx(path, 0, large, small, 1);
                return Icon.FromHandle(largeIcon ? large[0] : small[0]);
            }
            catch (Exception ex)
            {
                return SystemIcons.Application;
            }
        }
    }
}
