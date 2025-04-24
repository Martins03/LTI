using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikrotikManagerApp
{
    public partial class CriarRotaForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CriarRotaForm(HttpClient httpClient, string baseUrl)
        {
            InitializeComponent();
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            txtDistance.Text = "1"; // valor por defeito
        }

        private async void btnCriar_Click(object sender, EventArgs e)
        {
            string dst = txtDst.Text.Trim();
            string gw = txtGateway.Text.Trim();
            string distance = txtDistance.Text.Trim();

            if (string.IsNullOrWhiteSpace(dst) || string.IsNullOrWhiteSpace(gw))
            {
                MessageBox.Show("Preenche os campos obrigatórios (dst-address e gateway).");
                return;
            }

            var data = new Dictionary<string, string>
            {
                { "dst-address", dst },
                { "gateway", gw },
                { "distance", distance }
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PutAsync($"{_baseUrl}/rest/ip/route", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Rota criada com sucesso!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Erro ao criar rota: " + error);
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
