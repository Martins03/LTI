using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikrotikManagerApp
{
    public partial class CriarDHCPForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private string _enderecoSugerido = "192.168.88.1"; // valor por defeito

        public CriarDHCPForm(HttpClient httpClient, string baseUrl)
        {
            InitializeComponent();
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            CarregarInterfacesEPools();
        }

        private async void CarregarInterfacesEPools()
        {
            try
            {
                var ifaceResponse = await _httpClient.GetAsync($"{_baseUrl}/rest/interface");
                ifaceResponse.EnsureSuccessStatusCode();
                var ifaceJson = await ifaceResponse.Content.ReadAsStringAsync();
                var ifaceDoc = JsonDocument.Parse(ifaceJson);

                cmbInterface.Items.Clear();
                foreach (var item in ifaceDoc.RootElement.EnumerateArray())
                {
                    if (item.TryGetProperty("name", out var name))
                        cmbInterface.Items.Add(name.GetString());
                }

                var poolResponse = await _httpClient.GetAsync($"{_baseUrl}/rest/ip/pool");
                poolResponse.EnsureSuccessStatusCode();
                var poolJson = await poolResponse.Content.ReadAsStringAsync();
                var poolDoc = JsonDocument.Parse(poolJson);

                cmbPool.Items.Clear();
                foreach (var item in poolDoc.RootElement.EnumerateArray())
                {
                    if (item.TryGetProperty("name", out var name))
                        cmbPool.Items.Add(name.GetString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar interfaces/pools: " + ex.Message);
            }
        }

        private async void cmbInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            string iface = cmbInterface.SelectedItem?.ToString();
            if (!string.IsNullOrWhiteSpace(iface))
            {
                _enderecoSugerido = await ObterEnderecoServidorSugeridoAsync(iface);
            }
        }

        private async Task<string> ObterEnderecoServidorSugeridoAsync(string interfaceSelecionada)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/ip/address");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    if (item.TryGetProperty("interface", out var ifaceProp) &&
                        ifaceProp.GetString().Equals(interfaceSelecionada, StringComparison.OrdinalIgnoreCase))
                    {
                        var fullAddress = item.GetProperty("address").GetString();
                        return fullAddress.Split('/')[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter IP da interface: {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return "192.168.88.1"; // Fallback
        }

        private async void btnCriar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string iface = cmbInterface.SelectedItem?.ToString();
            string pool = cmbPool.SelectedItem?.ToString();
            string lease = txtLeaseTime.Text.Trim();

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(iface) ||
                string.IsNullOrWhiteSpace(pool) || string.IsNullOrWhiteSpace(lease))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            var data = new Dictionary<string, string>
            {
                { "name", nome },
                { "interface", iface },
                { "address-pool", pool },
                { "lease-time", lease },
            };

            string json = JsonSerializer.Serialize(data);
            var response = await _httpClient.PostAsync(
                $"{_baseUrl}/rest/ip/dhcp-server/add",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("DHCP criado com sucesso.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Erro ao criar DHCP: " + error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
