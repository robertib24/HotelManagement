using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Services;

namespace HotelManagement.Client
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Inițializează clientul HTTP pentru API
                ApiHelper.InitializeClient();

                // Setează handler pentru excepții negestionate
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                // Pornește aplicația cu formularul principal
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la pornirea aplicației: {ex.Message}\n\nDetalii: {ex.StackTrace}",
                    "Eroare critică", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            try
            {
                if (ex != null)
                {
                    // Log excepția (implementare viitoare)

                    // Afișează mesaj de eroare
                    MessageBox.Show($"A apărut o eroare neașteptată: {ex.Message}\n\nDetalii: {ex.StackTrace}",
                        "Eroare neașteptată", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("A apărut o eroare neașteptată",
                        "Eroare neașteptată", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                try
                {
                    MessageBox.Show("A apărut o eroare critică", "Eroare critică", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    // Nu se poate face nimic dacă nici MessageBox nu funcționează
                }
            }
        }
    }
}