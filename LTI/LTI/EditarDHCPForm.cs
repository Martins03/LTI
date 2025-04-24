using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikrotikManagerApp
{
    public partial class EditarDHCPForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _dhcpId;

        public EditarDHCPForm(HttpClient httpClient, string baseUrl, string dhcpId)
        {
            InitializeComponent();
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            _dhcpId = dhcpId;
            CarregarDadosAtuais();
        }

        private async void CarregarDadosAtuais()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/ip/dhcp-server/{_dhcpId}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var item = JsonDocument.Parse(json).RootElement;

                txtNome.Text = item.GetProperty("name").GetString();
                txtLeaseTime.Text = item.GetProperty("lease-time").GetString();

                await CarregarInterfacesEPools();

                cmbInterface.SelectedItem = item.GetProperty("interface").GetString();
                cmbPool.SelectedItem = item.GetProperty("address-pool").GetString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados atuais: {ex.Message}");
            }
        }

        private async Task CarregarInterfacesEPools()
        {
            try
            {
                var ifaceResponse = await _httpClient.GetAsync($"{_baseUrl}/rest/interface");
                ifaceResponse.EnsureSuccessStatusCode();
                var ifaceDoc = JsonDocument.Parse(await ifaceResponse.Content.ReadAsStringAsync());
                cmbInterface.Items.Clear();
                foreach (var iface in ifaceDoc.RootElement.EnumerateArray())
                {
                    if (iface.TryGetProperty("name", out var name))
                        cmbInterface.Items.Add(name.GetString());
                }

                var poolResponse = await _httpClient.GetAsync($"{_baseUrl}/rest/ip/pool");
                poolResponse.EnsureSuccessStatusCode();
                var poolDoc = JsonDocument.Parse(await poolResponse.Content.ReadAsStringAsync());
                cmbPool.Items.Clear();
                foreach (var pool in poolDoc.RootElement.EnumerateArray())
                {
                    if (pool.TryGetProperty("name", out var name))
                        cmbPool.Items.Add(name.GetString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar interfaces e pools: " + ex.Message);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
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
                { "lease-time", lease }
            };

            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_baseUrl}/rest/ip/dhcp-server/{_dhcpId}")
            {
                Content = content
            };

            try
            {
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("DHCP editado com sucesso.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Erro ao editar DHCP: " + error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar DHCP: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
