namespace WebViewRequestCapture
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            Application.Run(args.Contains("-config") ? new ConfigForm() : new MainForm());
        }
    }
}