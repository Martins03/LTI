using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikrotikManagerApp
{
    public partial class MainForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public MainForm(HttpClient client, string baseUrl)
        {
            InitializeComponent();
            _httpClient = client;
            _baseUrl = baseUrl;
        }

        private async void btnGetInterfaces_Click(object sender, EventArgs e)
        {
            await LoadData("interface", dgvInterfaces);
        }

        private async void btnGetWireless_Click(object sender, EventArgs e)
        {
            await LoadData("interface/wireless", dgvWireless);
        }

        private async void btnGetBridges_Click(object sender, EventArgs e)
        {
            await LoadData("interface/bridge", dgvBridges);
        }

        private async Task LoadData(string endpoint, DataGridView grid)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/{endpoint}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                grid.Rows.Clear();

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    string name = item.GetProperty("name").GetString();
                    string type = item.TryGetProperty("type", out var t) ? t.GetString() : "";
                    grid.Rows.Add(name, type);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}