using System;
using System.Windows.Forms;
using qlspdigital.Shared;

namespace qlspdigital
{
    internal static class Program
    {
        //  The main entry point for the application.
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "--test-ui")
            {
                int exitCode = UiTester.Run();
                Environment.Exit(exitCode);
                return;
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new qlspdigital.Views.Main.MainForm());
        }
    }
}
