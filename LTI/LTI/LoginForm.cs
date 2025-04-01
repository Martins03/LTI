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
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            BaseUrl = txtBaseUrl.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            HttpClient = new HttpClient();
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                (sender2, cert, chain, sslPolicyErrors) => true;

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
