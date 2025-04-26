using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace MikrotikManagerApp
{
    public partial class LoginForm : Form
    {
        public string BaseUrl { get; private set; }
        public HttpClient HttpClient { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            cmbProtocol.SelectedIndex = 1; // Padrão para https
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string protocol = cmbProtocol.SelectedItem?.ToString() ?? "https";
            string host = txtBaseUrl.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Construir URL base
            BaseUrl = $"{protocol}://{host}";

            HttpClient = new HttpClient();

            // Permitir certificados inválidos apenas em HTTPS
            if (protocol == "https")
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                    (sender2, cert, chain, sslPolicyErrors) => true;
            }

            var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            try
            {
                var response = HttpClient.GetAsync($"{BaseUrl}/rest/interface").Result;
                response.EnsureSuccessStatusCode();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("Falha na autenticação ou ligação.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}