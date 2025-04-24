using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikrotikManagerApp
{
    public partial class ConfigurarWirelessForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ConfigurarWirelessForm(HttpClient httpClient, string baseUrl)
        {
            InitializeComponent();
            _httpClient = httpClient;
            _baseUrl = baseUrl;

            CarregarInterfacesWireless();
            CarregarSecurityProfiles();
            cmbBand.Items.AddRange(new string[] { "2ghz-b/g/n", "5ghz-a/n", "5ghz-a/n/ac" });
            cmbBand.SelectedIndex = 0; // Valor por defeito
        }

        private async void CarregarInterfacesWireless()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/interface/wireless");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                cmbInterface.Items.Clear();

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    if (item.TryGetProperty("name", out var name))
                        cmbInterface.Items.Add(name.GetString());
                }

                if (cmbInterface.Items.Count > 0)
                    cmbInterface.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar interfaces: " + ex.Message);
            }
        }

        private async void CarregarSecurityProfiles()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/interface/wireless/security-profiles");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                cmbSecurityProfile.Items.Clear();

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    if (item.TryGetProperty("name", out var name))
                        cmbSecurityProfile.Items.Add(name.GetString());
                }

                if (cmbSecurityProfile.Items.Count > 0)
                    cmbSecurityProfile.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar perfis de segurança: " + ex.Message);
            }
        }

        private async void btnAplicar_Click(object sender, EventArgs e)
        {
            string iface = cmbInterface.SelectedItem?.ToString();
            string ssid = txtSSID.Text.Trim();
            string profile = cmbSecurityProfile.SelectedItem?.ToString();
            string band = cmbBand.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(iface) || string.IsNullOrWhiteSpace(ssid) ||
                string.IsNullOrWhiteSpace(profile) || string.IsNullOrWhiteSpace(band))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            var data = new Dictionary<string, string>
            {
                { "ssid", ssid },
                { "security-profile", profile },
                { "band", band }
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_baseUrl}/rest/interface/wireless/{iface}")
                {
                    Content = content
                };

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Interface wireless configurada com sucesso!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Erro ao aplicar configuração: " + error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
