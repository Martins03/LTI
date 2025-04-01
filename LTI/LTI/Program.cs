using System;
using System.Windows.Forms;
using MikrotikManagerApp; // Adiciona este using para aceder ao LoginForm

namespace LTI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Aqui deverás abrir o MainForm (ainda vais criar)
                MessageBox.Show("Login com sucesso!"); // temporário para testar
                Application.Run(new MainForm(loginForm.HttpClient, loginForm.BaseUrl));
                // Application.Run(new MainForm(loginForm.HttpClient, loginForm.BaseUrl));
            }
        }
    }
}
