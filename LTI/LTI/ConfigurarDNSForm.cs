using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikrotikManagerApp
{
    public partial class ConfigurarDNSForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ConfigurarDNSForm(HttpClient httpClient, string baseUrl)
        {
            InitializeComponent();
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            CarregarConfiguracoes();
        }

        private async void CarregarConfiguracoes()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/ip/dns");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                txtServers.Text = root.GetProperty("servers").GetString() ?? "";
                chkAllowRemote.Checked = root.TryGetProperty("allow-remote-requests", out var allowRemote) && allowRemote.GetString() == "true";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar configuração atual: " + ex.Message);
            }
        }

        private async void btnAplicar_Click(object sender, EventArgs e)
        {
            string servidores = txtServers.Text.Trim();
            bool permitirRemoto = chkAllowRemote.Checked;

            if (string.IsNullOrWhiteSpace(servidores))
            {
                MessageBox.Show("Indica pelo menos um servidor DNS.");
                return;
            }

            var data = new Dictionary<string, object>
    {
        { "servers", servidores },
        { "allow-remote-requests", permitirRemoto }
    };

            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            try
            {
                var request = new HttpRequestMessage(
                    new HttpMethod("PATCH"),
                    new Uri($"{_baseUrl}/rest/ip/dns")
                )
                {
                    Content = content
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Servidor DNS configurado com sucesso!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Erro ao configurar DNS: " + error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }





        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
