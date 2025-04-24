using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikrotikManagerApp
{
    public partial class EditarRotaForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _routeId;

        public EditarRotaForm(HttpClient httpClient, string baseUrl, string routeId, string dst, string gateway, string distance)
        {
            InitializeComponent();
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            _routeId = routeId;

            txtDst.Text = dst;
            txtGateway.Text = gateway;
            txtDistance.Text = distance;
        }

        private async void btnAplicar_Click(object sender, EventArgs e)
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
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_baseUrl}/rest/ip/route/{_routeId}")
                {
                    Content = content
                };

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Rota atualizada com sucesso!");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Erro ao editar rota: " + error);
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
