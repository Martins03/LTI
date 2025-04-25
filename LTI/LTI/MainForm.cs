// MainForm.cs
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using MikrotikManagerApp;
using System.Text;


namespace MikrotikManagerApp
{
    public partial class MainForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public MainForm(HttpClient client, string baseUrl)
        {
            try
            {
                InitializeComponent();
                _httpClient = client;
                _baseUrl = baseUrl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao iniciar MainForm: " + ex.Message);
            }
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



        private async void btnGetWG_Click(object sender, EventArgs e)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/interface/wireguard");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                dgvWireGuard.Rows.Clear();

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    string id = item.GetProperty(".id").GetString();
                    string name = item.GetProperty("name").GetString();
                    string address = item.TryGetProperty("listen-port", out var addrProp) ? addrProp.GetRawText() : "N/A";
                    dgvWireGuard.Rows.Add(id, name, address);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar WireGuard: {ex.Message}", "Erro");
            }
        }

        private async void btnAddWG_Click(object sender, EventArgs e)
        {
            string name = Prompt.ShowDialog("Nome da interface WireGuard:", "Criar WireGuard");
            if (string.IsNullOrWhiteSpace(name)) return;

            string listenPort = Prompt.ShowDialog("Porta de escuta (ex: 51820):", "Porta WireGuard");
            if (string.IsNullOrWhiteSpace(listenPort)) return;

            var data = new Dictionary<string, object>
    {
        {"name", name},
        {"listen-port", listenPort}
    };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/rest/interface/wireguard", content);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("WireGuard criado com sucesso!");
                btnGetWG_Click(null, null);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Erro ao criar WireGuard: {error}");
            }
        }

        private async void btnDeleteWG_Click(object sender, EventArgs e)
        {
            if (dgvWireGuard.SelectedRows.Count == 0) return;

            string id = dgvWireGuard.SelectedRows[0].Cells[0].Value.ToString();
            var result = MessageBox.Show("Deseja realmente apagar esta interface WireGuard?", "Confirmar", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            var response = await _httpClient.DeleteAsync($"{_baseUrl}/rest/interface/wireguard/{id}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("WireGuard apagado.");
                btnGetWG_Click(null, null);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Erro ao apagar WireGuard: {error}");
            }
        }

        private async void btnListarDHCP_Click(object sender, EventArgs e)
        {
            try
            {
                // Chama a API para listar os servidores DHCP
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/ip/dhcp-server");
                response.EnsureSuccessStatusCode();

                // Lê o conteúdo da resposta
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                // Limpa o DataGridView antes de adicionar novos dados
                dgvDHCP.Rows.Clear();

                // Adiciona os dados ao DataGridView
                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    string id = item.GetProperty(".id").GetString();
                    string name = item.GetProperty("name").GetString();
                    string interfaceName = item.GetProperty("interface").GetString();
                    string pool = item.GetProperty("address-pool").GetString();
                    dgvDHCP.Rows.Add(id, name, interfaceName, pool);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar DHCP: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddDHCP_Click(object sender, EventArgs e)
        {
            using (var form = new CriarDHCPForm(_httpClient, _baseUrl))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    btnListarDHCP_Click(null, null);
                }
            }
        }

        private void btnEditDHCP_Click(object sender, EventArgs e)
        {
            if (dgvDHCP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleciona uma entrada para editar.");
                return;
            }

            string id = dgvDHCP.SelectedRows[0].Cells[0].Value.ToString();

            using (var form = new EditarDHCPForm(_httpClient, _baseUrl, id))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    btnListarDHCP_Click(null, null);
                }
            }
        }

        private async void btnDeleteDHCP_Click(object sender, EventArgs e)
        {
            if (dgvDHCP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleciona um servidor DHCP para apagar.", "Aviso");
                return;
            }

            string id = dgvDHCP.SelectedRows[0].Cells[0].Value.ToString();
            string name = dgvDHCP.SelectedRows[0].Cells[1].Value.ToString();

            var confirm = MessageBox.Show($"Tens a certeza que queres apagar o servidor '{name}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/rest/ip/dhcp-server/{id}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Servidor DHCP apagado com sucesso.");
                    btnListarDHCP_Click(null, null);
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Erro ao apagar: {error}", "Erro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro");
            }
        }

        private async void btnToggleWireless_Click(object sender, EventArgs e)
        {
            if (dgvWireless.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleciona uma interface wireless.");
                return;
            }

            string id = dgvWireless.SelectedRows[0].Cells[0].Value.ToString();
            string name = dgvWireless.SelectedRows[0].Cells[1].Value.ToString();

            try
            {
                // Obter info atual da interface wireless
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/interface/wireless/{id}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                bool disabled = false;
                if (doc.RootElement.TryGetProperty("disabled", out var disabledProp))
                {
                    disabled = disabledProp.GetString() == "true";
                }

                // Inverter estado
                var body = new Dictionary<string, object>
                {
                    { "disabled", !disabled }
                };

                var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
                var patchRequest = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_baseUrl}/rest/interface/wireless/{id}")
                {
                    Content = content
                };

                var patchResponse = await _httpClient.SendAsync(patchRequest);
                if (patchResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show($"{(disabled ? "Ativada" : "Desativada")} com sucesso.");
                    btnGetWireless_Click(null, null); // Atualiza lista
                }
                else
                {
                    string error = await patchResponse.Content.ReadAsStringAsync();
                    MessageBox.Show($"Erro ao atualizar: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void btnConfigurarWireless_Click(object sender, EventArgs e)
        {
            using (var form = new ConfigurarWirelessForm(_httpClient, _baseUrl))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    btnGetWireless_Click(null, null); // Atualiza a lista após configuração
                }
            }
        }

        private async void btnConfigurarDNS_Click(object sender, EventArgs e)
        {
            string servidores = Prompt.ShowDialog(
                "Digite os servidores DNS (ex: 8.8.8.8,8.8.4.4):",
                "Configurar DNS");

            if (string.IsNullOrWhiteSpace(servidores))
            {
                MessageBox.Show("Deve indicar pelo menos um servidor DNS.", "Aviso");
                return;
            }

            var data = new Dictionary<string, string>
    {
        { "servers", servidores }
    };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/rest/ip/dns/set", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("DNS configurado com sucesso!", "Sucesso");
                }
                else
                {
                    string erro = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Erro ao configurar DNS:\n{erro}", "Erro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao configurar DNS: " + ex.Message, "Erro");
            }
        }

        private async void btnToggleDNS_Click(object sender, EventArgs e)
        {
            try
            {
                // Buscar config atual
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/ip/dns");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                bool currentAllowRemote = root.TryGetProperty("allow-remote-requests", out var prop) && prop.GetString() == "true";

                // Inverter valor
                var updatedConfig = new Dictionary<string, object>
        {
            { "allow-remote-requests", !currentAllowRemote }
        };

                var content = new StringContent(JsonSerializer.Serialize(updatedConfig), Encoding.UTF8, "application/json");
                var setResponse = await _httpClient.PostAsync($"{_baseUrl}/rest/ip/dns/set", content);

                if (setResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show($"DNS {(currentAllowRemote ? "desativado" : "ativado")} com sucesso!", "Sucesso");
                }
                else
                {
                    var error = await setResponse.Content.ReadAsStringAsync();
                    MessageBox.Show("Erro ao aplicar: " + error, "Erro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao comunicar com o Mikrotik: " + ex.Message, "Erro");
            }
        }



        private async void btnListarRotas_Click(object sender, EventArgs e)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/rest/ip/route");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                dgvRoutes.Rows.Clear();

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    string id = item.GetProperty(".id").GetString();
                    string dst = item.TryGetProperty("dst-address", out var dstProp) ? dstProp.GetString() ?? "" : "";
                    string gateway = item.TryGetProperty("gateway", out var gwProp) ? gwProp.GetString() ?? "" : "";
                    dgvRoutes.Rows.Add(id, dst, gateway);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar rotas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCriarRota_Click(object sender, EventArgs e)
        {
            using (var form = new CriarRotaForm(_httpClient, _baseUrl))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    btnListarRotas_Click(null, null); // Atualiza a lista após criação
                }
            }
        }

        private void btnEditarRota_Click(object sender, EventArgs e)
        {
            if (dgvRoutes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleciona uma rota para editar.", "Aviso");
                return;
            }

            string id = dgvRoutes.SelectedRows[0].Cells[0].Value.ToString();
            string dst = dgvRoutes.SelectedRows[0].Cells[1].Value.ToString();
            string gw = dgvRoutes.SelectedRows[0].Cells[2].Value.ToString();
            string distance = "1"; 

            using (var form = new EditarRotaForm(_httpClient, _baseUrl, id, dst, gw, distance))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    btnListarRotas_Click(null, null); // Atualiza a tabela
                }
            }
        }


        private async void btnApagarRota_Click(object sender, EventArgs e)
        {
            if (dgvRoutes.SelectedRows.Count > 0)
            {
                string id = dgvRoutes.SelectedRows[0].Cells[0].Value.ToString();
                string dst = dgvRoutes.SelectedRows[0].Cells[1].Value.ToString();

                var result = MessageBox.Show($"Deseja realmente apagar a rota para '{dst}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes) return;

                try
                {
                    var response = await _httpClient.DeleteAsync($"{_baseUrl}/rest/ip/route/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Rota apagada com sucesso.");
                        btnListarRotas_Click(null, null);
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao apagar rota: {error}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleciona uma rota para apagar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    // Verifica se é a grid de wireless (tem 3 colunas)
                    if (grid == dgvWireless && item.TryGetProperty("disabled", out var disabledProp))
                    {
                        string estado = disabledProp.GetString() == "true" ? "Desativada" : "Ativa";
                        grid.Rows.Add(id, name, estado);
                    }
                    else
                    {
                        grid.Rows.Add(id, name, type);
                    }
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