using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebViewRequestCapture
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();

            textAPPTitle.Text = Properties.Settings.Default.APP_Title;
            textAPIKey.Text = Properties.Settings.Default.API_Key;
            numericAPIPort.Value = Properties.Settings.Default.API_Port;
            textStartPage.Text = Properties.Settings.Default.Start_Page;
            textURLFilter.Text = Properties.Settings.Default.URL_Filter;
            textMimeTypeFilter.Text = Properties.Settings.Default.Mime_Type_Filter;
            numericAutoRefresh.Value = Properties.Settings.Default.Auto_Refresh;
        }

        private void btnCarregarIcone_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Icon Files|*.ico";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textIconPath.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            btnSalvar.Enabled = false;
            try
            {
                if (textIconPath.Text.Length > 0 && !File.Exists(textIconPath.Text))
                {
                    MessageBox.Show(
                        "O ícone informado não foi localizado. Por favor, verifique o caminho e tente novamente.",
                        "Ícone Inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Properties.Settings.Default.APP_Title = textAPPTitle.Text;
                Properties.Settings.Default.API_Key = textAPIKey.Text;
                Properties.Settings.Default.API_Port = Convert.ToInt32(numericAPIPort.Value);
                Properties.Settings.Default.Start_Page = textStartPage.Text;
                Properties.Settings.Default.URL_Filter = textURLFilter.Text;
                Properties.Settings.Default.Mime_Type_Filter = textMimeTypeFilter.Text;
                Properties.Settings.Default.Auto_Refresh = Convert.ToInt32(numericAutoRefresh.Value);
                Properties.Settings.Default.Save();

                if (textIconPath.Text.Length > 0)
                {
                    string rceditPath = Path.Combine(Path.GetTempPath(), "rcedit.exe");
                    if (!File.Exists(rceditPath))
                    {
                        File.WriteAllBytes(rceditPath, Properties.Resources.rcedit_x64);
                    }

                    string executableTempPath = Path.Combine(Path.GetTempPath(), "WebViewRequestCapture.exe");
                    File.Copy(Application.ExecutablePath, executableTempPath, true);

                    var psi = new ProcessStartInfo
                    {
                        FileName = rceditPath,
                        Arguments = $"\"{executableTempPath}\" --set-icon \"{textIconPath.Text}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (var proc = Process.Start(psi))
                    {
                        proc.WaitForExit();
                        if (proc.ExitCode != 0)
                        {
                            MessageBox.Show("Erro ao aplicar ícone via rcedit.");
                            return;
                        }
                    }

                    string updaterBat = Path.Combine(Path.GetTempPath(), "WebViewRequestCaptureUpdate.bat");
                    File.WriteAllText(updaterBat, $@"
                    @echo off
                    timeout /t 2 /nobreak >nul
                    taskkill /f /im WebViewRequestCapture.exe >nul 2>&1
                    timeout /t 1 >nul
                    copy /y ""{executableTempPath}"" ""{Application.ExecutablePath}""
                    start """" ""{Application.ExecutablePath}""
                    del ""%~f0""
                    ");

                    // Executar o .bat e fechar o app
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = updaterBat,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSalvar.Enabled = true;
            }
        }
    }
}
