// MainForm.cs
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic; // ← este aqui

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

        private async void btnAddBridge_Click(object sender, EventArgs e)
        {
            string name = Prompt.ShowDialog("Digite o nome da bridge (apenas letras, números e hífens):", "Criar Bridge");
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("O nome da bridge não pode ser vazio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z0-9-]+$"))
            {
                MessageBox.Show("O nome da bridge deve conter apenas letras, números e hífens.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var data = new { name = name, mtu = "1500" };
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/rest/interface/bridge/add", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Bridge '{name}' criada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadData("interface/bridge", dgvBridges);
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Erro ao criar bridge: {response.StatusCode}\n{error}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar bridge: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDeleteBridge_Click(object sender, EventArgs e)
        {
            if (dgvBridges.SelectedRows.Count > 0)
            {
                string id = dgvBridges.SelectedRows[0].Cells[0].Value.ToString();
                string name = dgvBridges.SelectedRows[0].Cells[1].Value.ToString();

                // Impede apagar a bridge1
                if (name.Equals("bridge1", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("A bridge 'bridge1' não pode ser apagada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var response = await _httpClient.DeleteAsync($"{_baseUrl}/rest/interface/bridge/{id}");
                    MessageBox.Show(response.IsSuccessStatusCode ? "Apagado com sucesso!" : "Erro ao apagar.");
                    await LoadData("interface/bridge", dgvBridges);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao apagar bridge: {ex.Message}", "Erro");
                }
            }
        }


        private async void btnEditBridge_Click(object sender, EventArgs e)
        {
            if (dgvBridges.SelectedRows.Count > 0)
            {
                string id = dgvBridges.SelectedRows[0].Cells[0].Value.ToString();
                string currentName = dgvBridges.SelectedRows[0].Cells[1].Value.ToString();

                if (currentName.Equals("bridge1", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("A bridge 'bridge1' não pode ser editada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string newName = Prompt.ShowDialog("Novo nome da bridge:", "Editar Bridge");

                if (!string.IsNullOrWhiteSpace(newName))
                {
                    try
                    {
                        var json = JsonSerializer.Serialize(new { name = newName });
                        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_baseUrl}/rest/interface/bridge/{id}")
                        {
                            Content = content
                        };

                        var response = await _httpClient.SendAsync(request);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Bridge atualizada com sucesso!", "Sucesso");
                            await LoadData("interface/bridge", dgvBridges);
                        }
                        else
                        {
                            string error = await response.Content.ReadAsStringAsync();
                            MessageBox.Show($"Erro ao editar bridge: {response.StatusCode}\n{error}", "Erro");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro: {ex.Message}", "Exceção");
                    }
                }
            }
        }
        private async void btnGetBridgePorts_Click(object sender, EventArgs e)
        {
            await LoadBridgePorts();
        }


        private async void btnGetSecurityProfiles_Click(object sender, EventArgs e)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/interface/wireless/security-profiles");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                dgvSecurityProfiles.Rows.Clear();

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    string id = item.GetProperty(".id").GetString();
                    string name = item.GetProperty("name").GetString();
                    dgvSecurityProfiles.Rows.Add(id, name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar perfis: {ex.Message}", "Erro");
            }
        }



        private async void btnAddSecurityProfile_Click(object sender, EventArgs e)
        {
            string name = Prompt.ShowDialog("Digite o nome do perfil (apenas letras, números e hífens):", "Criar Perfil");

            if (string.IsNullOrWhiteSpace(name) || !System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z0-9-]+$"))
            {
                MessageBox.Show("Nome inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string psk = Prompt.ShowDialog("Digite a chave WPA2 (mínimo 8 caracteres):", "Chave de Segurança");

            if (string.IsNullOrWhiteSpace(psk) || psk.Length < 8)
            {
                MessageBox.Show("A chave deve ter pelo menos 8 caracteres.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var data = new Dictionary<string, object>
{
    { "name", name },
    { "mode", "dynamic-keys" },
    { "authentication-types", "wpa2-psk" },
    { "wpa2-pre-shared-key", psk }
};

                var options = new JsonSerializerOptions { PropertyNamingPolicy = null };
                var json = JsonSerializer.Serialize(data, options);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_baseUrl}/rest/interface/wireless/security-profiles", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Perfil '{name}' criado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGetSecurityProfiles_Click(null, null);
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Erro ao criar perfil: {response.StatusCode}\n{error}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar perfil: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }








        private async void btnEditSecurityProfile_Click(object sender, EventArgs e)
        {
            if (dgvSecurityProfiles.SelectedRows.Count > 0)
            {
                string id = dgvSecurityProfiles.SelectedRows[0].Cells[0].Value.ToString();
                string currentName = dgvSecurityProfiles.SelectedRows[0].Cells[1].Value.ToString();

                string newName = Prompt.ShowDialog("Novo nome do perfil:", "Editar Perfil");

                if (string.IsNullOrWhiteSpace(newName)) return;

                var profile = new { name = newName };

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_baseUrl}/rest/interface/wireless/security-profiles/{id}")
                {
                    Content = new StringContent(JsonSerializer.Serialize(profile), System.Text.Encoding.UTF8, "application/json")
                };

                try
                {
                    var response = await _httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Perfil atualizado!");
                        btnGetSecurityProfiles_Click(null, null);
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro: {error}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}");
                }
            }
        }


        private async void btnDeleteSecurityProfile_Click(object sender, EventArgs e)
        {
            if (dgvSecurityProfiles.SelectedRows.Count > 0)
            {
                string id = dgvSecurityProfiles.SelectedRows[0].Cells[0].Value.ToString();

                var result = MessageBox.Show("Deseja realmente apagar este perfil?", "Confirmar", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes) return;

                try
                {
                    var response = await _httpClient.DeleteAsync($"{_baseUrl}/rest/interface/wireless/security-profiles/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Perfil apagado.");
                        btnGetSecurityProfiles_Click(null, null);
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro: {error}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}");
                }
            }
        }



        private async void btnGetIP_Click(object sender, EventArgs e)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/ip/address");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                dgvIPAddresses.Rows.Clear();

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    string id = item.GetProperty(".id").GetString();
                    string address = item.GetProperty("address").GetString();
                    string iface = item.GetProperty("interface").GetString();
                    dgvIPAddresses.Rows.Add(id, address, iface);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar IPs: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnAddIP_Click(object sender, EventArgs e)
        {
            string address = Prompt.ShowDialog("Digite o IP (ex: 192.168.88.10/24):", "Novo Endereço IP");
            string iface = Prompt.ShowDialog("Digite a interface (ex: ether1):", "Interface");

            if (string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(iface)) return;

            var data = new Dictionary<string, string>
    {
        { "address", address },
        { "interface", iface }
    };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/rest/ip/address", content);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Endereço IP adicionado com sucesso!");
                btnGetIP_Click(null, null);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Erro ao adicionar IP: {error}");
            }
        }

        private async void btnEditIP_Click(object sender, EventArgs e)
        {
            if (dgvIPAddresses.SelectedRows.Count == 0) return;

            string id = dgvIPAddresses.SelectedRows[0].Cells[0].Value.ToString();
            string currentAddr = dgvIPAddresses.SelectedRows[0].Cells[1].Value.ToString();
            string currentIface = dgvIPAddresses.SelectedRows[0].Cells[2].Value.ToString();

            string newAddr = Prompt.ShowDialog("Novo endereço IP:", "Editar IP", currentAddr);
            string newIface = Prompt.ShowDialog("Nova interface:", "Editar Interface", currentIface);

            if (string.IsNullOrWhiteSpace(newAddr) || string.IsNullOrWhiteSpace(newIface)) return;

            var data = new Dictionary<string, string>
    {
        { "address", newAddr },
        { "interface", newIface }
    };

            var content = new StringContent(JsonSerializer.Serialize(data), System.Text.Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_baseUrl}/rest/ip/address/{id}")
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("IP atualizado.");
                btnGetIP_Click(null, null);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Erro ao atualizar IP: {error}");
            }
        }

        private async void btnDeleteIP_Click(object sender, EventArgs e)
        {
            if (dgvIPAddresses.SelectedRows.Count == 0) return;

            string id = dgvIPAddresses.SelectedRows[0].Cells[0].Value.ToString();

            var result = MessageBox.Show("Deseja realmente apagar este IP?", "Confirmar", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            var response = await _httpClient.DeleteAsync($"{_baseUrl}/rest/ip/address/{id}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("IP removido.");
                btnGetIP_Click(null, null);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Erro ao apagar IP: {error}");
            }
        }









        private async Task LoadBridgePorts()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/interface/bridge/port");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                dgvBridgePorts.Rows.Clear();

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    string bridge = item.GetProperty("bridge").GetString();
                    string iface = item.GetProperty("interface").GetString();
                    dgvBridgePorts.Rows.Add(bridge, iface);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar portas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    string id = item.GetProperty(".id").GetString();
                    string name = item.GetProperty("name").GetString();
                    string type = item.TryGetProperty("type", out var t) ? t.GetString() : "";
                    grid.Rows.Add(id, name, type);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                Text = caption,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label lbl = new Label() { Left = 10, Top = 20, Width = 260, Text = text };
            TextBox inputBox = new TextBox() { Left = 10, Top = 50, Width = 260, Text = defaultValue };
            Button confirmation = new Button() { Text = "OK", Left = 10, Width = 260, Top = 80 };

            confirmation.Click += (sender, e) => { prompt.DialogResult = DialogResult.OK; prompt.Close(); };
            prompt.Controls.Add(lbl);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "";
        }
    }

}