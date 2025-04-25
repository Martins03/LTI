namespace MikrotikManagerApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabInterfaces;
        private System.Windows.Forms.TabPage tabWireless;
        private System.Windows.Forms.TabPage tabBridges;
        private System.Windows.Forms.TabPage tabSecurityProfiles;
        private System.Windows.Forms.TabPage tabDHCP;
        private System.Windows.Forms.TabPage tabDNS;
        private System.Windows.Forms.TabPage tabRoutes;
        private System.Windows.Forms.TabPage tabIP;
        private System.Windows.Forms.TabPage tabWireGuard;

        private System.Windows.Forms.DataGridView dgvInterfaces;
        private System.Windows.Forms.DataGridView dgvWireless;
        private System.Windows.Forms.DataGridView dgvBridges;
        private System.Windows.Forms.DataGridView dgvBridgePorts;
        private System.Windows.Forms.DataGridView dgvSecurityProfiles;
        private System.Windows.Forms.DataGridView dgvDHCP;
        private System.Windows.Forms.DataGridView dgvRoutes;
        private System.Windows.Forms.DataGridView dgvIPAddresses;
        private System.Windows.Forms.DataGridView dgvWireGuard;

        private System.Windows.Forms.Button btnGetInterfaces;
        private System.Windows.Forms.Button btnGetWireless;
        private System.Windows.Forms.Button btnGetBridges;
        private System.Windows.Forms.Button btnAddBridge;
        private System.Windows.Forms.Button btnDeleteBridge;
        private System.Windows.Forms.Button btnEditBridge;
        private System.Windows.Forms.Button btnGetBridgePorts;

        private System.Windows.Forms.Button btnListarDHCP;
        private System.Windows.Forms.Button btnAddDHCP;
        private System.Windows.Forms.Button btnEditDHCP;
        private System.Windows.Forms.Button btnDeleteDHCP;

        private System.Windows.Forms.Button btnGetSecurityProfiles;
        private System.Windows.Forms.Button btnAddSecurityProfile;
        private System.Windows.Forms.Button btnEditSecurityProfile;
        private System.Windows.Forms.Button btnDeleteSecurityProfile;

        private System.Windows.Forms.Button btnToggleWireless;
        private System.Windows.Forms.Button btnConfigurarWireless;

        private System.Windows.Forms.Button btnConfigurarDNS;
        private System.Windows.Forms.Button btnToggleDNS;

        private System.Windows.Forms.Button btnListarRotas;
        private System.Windows.Forms.Button btnCriarRota;
        private System.Windows.Forms.Button btnEditarRota;
        private System.Windows.Forms.Button btnApagarRota;

        private System.Windows.Forms.Button btnGetIP;
        private System.Windows.Forms.Button btnAddIP;
        private System.Windows.Forms.Button btnEditIP;
        private System.Windows.Forms.Button btnDeleteIP;

        private System.Windows.Forms.Button btnGetWG;
        private System.Windows.Forms.Button btnAddWG;
        private System.Windows.Forms.Button btnDeleteWG;
        private System.Windows.Forms.Button btnAdicionarPeer;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabInterfaces = new System.Windows.Forms.TabPage();
            this.tabWireless = new System.Windows.Forms.TabPage();
            this.tabBridges = new System.Windows.Forms.TabPage();
            this.tabSecurityProfiles = new System.Windows.Forms.TabPage();
            this.tabDHCP = new System.Windows.Forms.TabPage();
            this.tabDNS = new System.Windows.Forms.TabPage();
            this.tabRoutes = new System.Windows.Forms.TabPage();
            this.tabIP = new System.Windows.Forms.TabPage();
            this.tabWireGuard = new System.Windows.Forms.TabPage();

            this.InitializeInterfacesTab();
            this.InitializeWirelessTab();
            this.InitializeBridgesTab();
            this.InitializeSecurityProfilesTab();
            this.InitializeDHCPTab();
            this.InitializeDNSTab();
            this.InitializeRoutesTab();
            this.InitializeIPTab();
            this.InitializeWireGuardTab();

            // tabControl
            this.tabControl.Controls.Add(this.tabInterfaces);
            this.tabControl.Controls.Add(this.tabWireless);
            this.tabControl.Controls.Add(this.tabBridges);
            this.tabControl.Controls.Add(this.tabSecurityProfiles);
            this.tabControl.Controls.Add(this.tabDHCP);
            this.tabControl.Controls.Add(this.tabDNS);
            this.tabControl.Controls.Add(this.tabRoutes);
            this.tabControl.Controls.Add(this.tabIP);
            this.tabControl.Controls.Add(this.tabWireGuard);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Size = new System.Drawing.Size(760, 537);
            this.tabControl.TabIndex = 0;

            // MainForm
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.Text = "Gestor de Mikrotik";

            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void InitializeInterfacesTab()
        {
            this.tabInterfaces.Text = "Interfaces";
            this.dgvInterfaces = new System.Windows.Forms.DataGridView();
            this.btnGetInterfaces = new System.Windows.Forms.Button();

            // Configurações do DataGridView
            this.dgvInterfaces.Location = new System.Drawing.Point(6, 50);
            this.dgvInterfaces.Size = new System.Drawing.Size(740, 420);
            this.dgvInterfaces.ColumnCount = 2;
            this.dgvInterfaces.Columns[0].Name = "Nome";
            this.dgvInterfaces.Columns[1].Name = "Tipo";

            // Configurações do botão
            this.btnGetInterfaces.Location = new System.Drawing.Point(6, 10);
            this.btnGetInterfaces.Size = new System.Drawing.Size(150, 30);
            this.btnGetInterfaces.Text = "Listar Interfaces";
            this.btnGetInterfaces.Click += new System.EventHandler(this.btnGetInterfaces_Click);

            // Adiciona controles à aba
            this.tabInterfaces.Controls.Add(this.btnGetInterfaces);
            this.tabInterfaces.Controls.Add(this.dgvInterfaces);
        }

        private void InitializeWirelessTab()
        {
            this.tabWireless.Text = "Wireless";
            this.dgvWireless = new System.Windows.Forms.DataGridView();
            this.btnGetWireless = new System.Windows.Forms.Button();
            this.btnToggleWireless = new System.Windows.Forms.Button();
            this.btnConfigurarWireless = new System.Windows.Forms.Button();

            // Configurações do DataGridView
            this.dgvWireless.Location = new System.Drawing.Point(6, 50);
            this.dgvWireless.Size = new System.Drawing.Size(740, 420);
            this.dgvWireless.ColumnCount = 3;
            this.dgvWireless.Columns[0].Name = "Nome";
            this.dgvWireless.Columns[1].Name = "Tipo";
            this.dgvWireless.Columns[2].Name = "Estado";

            // Configurações dos botões
            this.btnGetWireless.Location = new System.Drawing.Point(6, 10);
            this.btnGetWireless.Size = new System.Drawing.Size(150, 30);
            this.btnGetWireless.Text = "Listar Wireless";
            this.btnGetWireless.Click += new System.EventHandler(this.btnGetWireless_Click);

            this.btnToggleWireless.Location = new System.Drawing.Point(162, 10);
            this.btnToggleWireless.Size = new System.Drawing.Size(150, 30);
            this.btnToggleWireless.Text = "Ativar/Desativar";
            this.btnToggleWireless.Click += new System.EventHandler(this.btnToggleWireless_Click);

            this.btnConfigurarWireless.Location = new System.Drawing.Point(318, 10);
            this.btnConfigurarWireless.Size = new System.Drawing.Size(150, 30);
            this.btnConfigurarWireless.Text = "Configurar Wireless";
            this.btnConfigurarWireless.Click += new System.EventHandler(this.btnConfigurarWireless_Click);

            // Adiciona controles à aba
            this.tabWireless.Controls.Add(this.btnGetWireless);
            this.tabWireless.Controls.Add(this.btnToggleWireless);
            this.tabWireless.Controls.Add(this.btnConfigurarWireless);
            this.tabWireless.Controls.Add(this.dgvWireless);
        }

        private void InitializeBridgesTab()
        {
            this.tabBridges.Text = "Bridges";
            this.dgvBridges = new System.Windows.Forms.DataGridView();
            this.dgvBridgePorts = new System.Windows.Forms.DataGridView();
            this.btnGetBridges = new System.Windows.Forms.Button();
            this.btnAddBridge = new System.Windows.Forms.Button();
            this.btnDeleteBridge = new System.Windows.Forms.Button();
            this.btnEditBridge = new System.Windows.Forms.Button();
            this.btnGetBridgePorts = new System.Windows.Forms.Button();

            // Configurações do DataGridView
            this.dgvBridges.Location = new System.Drawing.Point(6, 50);
            this.dgvBridges.Size = new System.Drawing.Size(740, 200);
            this.dgvBridges.ColumnCount = 3;
            this.dgvBridges.Columns[0].Name = "ID";
            this.dgvBridges.Columns[1].Name = "Nome";
            this.dgvBridges.Columns[2].Name = "Tipo";

            this.dgvBridgePorts.Location = new System.Drawing.Point(6, 260);
            this.dgvBridgePorts.Size = new System.Drawing.Size(740, 210);
            this.dgvBridgePorts.ColumnCount = 2;
            this.dgvBridgePorts.Columns[0].Name = "Bridge";
            this.dgvBridgePorts.Columns[1].Name = "Interface";

            // Configurações dos botões
            this.btnGetBridges.Location = new System.Drawing.Point(6, 10);
            this.btnGetBridges.Size = new System.Drawing.Size(150, 30);
            this.btnGetBridges.Text = "Listar Bridges";
            this.btnGetBridges.Click += new System.EventHandler(this.btnGetBridges_Click);

            this.btnAddBridge.Location = new System.Drawing.Point(162, 10);
            this.btnAddBridge.Size = new System.Drawing.Size(150, 30);
            this.btnAddBridge.Text = "Criar Bridge";
            this.btnAddBridge.Click += new System.EventHandler(this.btnAddBridge_Click);

            this.btnDeleteBridge.Location = new System.Drawing.Point(318, 10);
            this.btnDeleteBridge.Size = new System.Drawing.Size(150, 30);
            this.btnDeleteBridge.Text = "Apagar Bridge";
            this.btnDeleteBridge.Click += new System.EventHandler(this.btnDeleteBridge_Click);

            this.btnEditBridge.Location = new System.Drawing.Point(474, 10);
            this.btnEditBridge.Size = new System.Drawing.Size(150, 30);
            this.btnEditBridge.Text = "Editar Bridge";
            this.btnEditBridge.Click += new System.EventHandler(this.btnEditBridge_Click);

            this.btnGetBridgePorts.Location = new System.Drawing.Point(630, 10);
            this.btnGetBridgePorts.Size = new System.Drawing.Size(150, 30);
            this.btnGetBridgePorts.Text = "Listar Portas";
            this.btnGetBridgePorts.Click += new System.EventHandler(this.btnGetBridgePorts_Click);

            // Adiciona controles à aba
            this.tabBridges.Controls.Add(this.btnGetBridges);
            this.tabBridges.Controls.Add(this.btnAddBridge);
            this.tabBridges.Controls.Add(this.btnDeleteBridge);
            this.tabBridges.Controls.Add(this.btnEditBridge);
            this.tabBridges.Controls.Add(this.btnGetBridgePorts);
            this.tabBridges.Controls.Add(this.dgvBridges);
            this.tabBridges.Controls.Add(this.dgvBridgePorts);
        }

        private void InitializeSecurityProfilesTab()
        {
            this.tabSecurityProfiles.Text = "Perfis de Segurança";
            this.dgvSecurityProfiles = new System.Windows.Forms.DataGridView();
            this.btnGetSecurityProfiles = new System.Windows.Forms.Button();
            this.btnAddSecurityProfile = new System.Windows.Forms.Button();
            this.btnEditSecurityProfile = new System.Windows.Forms.Button();
            this.btnDeleteSecurityProfile = new System.Windows.Forms.Button();

            // Configurações do DataGridView
            this.dgvSecurityProfiles.Location = new System.Drawing.Point(6, 50);
            this.dgvSecurityProfiles.Size = new System.Drawing.Size(740, 420);
            this.dgvSecurityProfiles.ColumnCount = 2;
            this.dgvSecurityProfiles.Columns[0].Name = "ID";
            this.dgvSecurityProfiles.Columns[1].Name = "Nome";

            // Configurações dos botões
            this.btnGetSecurityProfiles.Location = new System.Drawing.Point(6, 10);
            this.btnGetSecurityProfiles.Size = new System.Drawing.Size(150, 30);
            this.btnGetSecurityProfiles.Text = "Listar Perfis";
            this.btnGetSecurityProfiles.Click += new System.EventHandler(this.btnGetSecurityProfiles_Click);

            this.btnAddSecurityProfile.Location = new System.Drawing.Point(162, 10);
            this.btnAddSecurityProfile.Size = new System.Drawing.Size(150, 30);
            this.btnAddSecurityProfile.Text = "Criar Perfil";
            this.btnAddSecurityProfile.Click += new System.EventHandler(this.btnAddSecurityProfile_Click);

            this.btnEditSecurityProfile.Location = new System.Drawing.Point(318, 10);
            this.btnEditSecurityProfile.Size = new System.Drawing.Size(150, 30);
            this.btnEditSecurityProfile.Text = "Editar Perfil";
            this.btnEditSecurityProfile.Click += new System.EventHandler(this.btnEditSecurityProfile_Click);

            this.btnDeleteSecurityProfile.Location = new System.Drawing.Point(474, 10);
            this.btnDeleteSecurityProfile.Size = new System.Drawing.Size(150, 30);
            this.btnDeleteSecurityProfile.Text = "Apagar Perfil";
            this.btnDeleteSecurityProfile.Click += new System.EventHandler(this.btnDeleteSecurityProfile_Click);

            // Adiciona controles à aba
            this.tabSecurityProfiles.Controls.Add(this.btnGetSecurityProfiles);
            this.tabSecurityProfiles.Controls.Add(this.btnAddSecurityProfile);
            this.tabSecurityProfiles.Controls.Add(this.btnEditSecurityProfile);
            this.tabSecurityProfiles.Controls.Add(this.btnDeleteSecurityProfile);
            this.tabSecurityProfiles.Controls.Add(this.dgvSecurityProfiles);
        }

        private void InitializeDHCPTab()
        {
            this.tabDHCP.Text = "DHCP";
            this.dgvDHCP = new System.Windows.Forms.DataGridView();
            this.btnListarDHCP = new System.Windows.Forms.Button();
            this.btnAddDHCP = new System.Windows.Forms.Button();
            this.btnEditDHCP = new System.Windows.Forms.Button();
            this.btnDeleteDHCP = new System.Windows.Forms.Button();

            // Configurações do DataGridView
            this.dgvDHCP.Location = new System.Drawing.Point(6, 50);
            this.dgvDHCP.Size = new System.Drawing.Size(740, 420);
            this.dgvDHCP.ColumnCount = 4;
            this.dgvDHCP.Columns[0].Name = "ID";
            this.dgvDHCP.Columns[1].Name = "Nome";
            this.dgvDHCP.Columns[2].Name = "Interface";
            this.dgvDHCP.Columns[3].Name = "Pool";

            // Configurações dos botões
            this.btnListarDHCP.Location = new System.Drawing.Point(6, 10);
            this.btnListarDHCP.Size = new System.Drawing.Size(150, 30);
            this.btnListarDHCP.Text = "Listar DHCP";
            this.btnListarDHCP.Click += new System.EventHandler(this.btnListarDHCP_Click);

            this.btnAddDHCP.Location = new System.Drawing.Point(162, 10);
            this.btnAddDHCP.Size = new System.Drawing.Size(150, 30);
            this.btnAddDHCP.Text = "Criar DHCP";
            this.btnAddDHCP.Click += new System.EventHandler(this.btnAddDHCP_Click);

            this.btnEditDHCP.Location = new System.Drawing.Point(318, 10);
            this.btnEditDHCP.Size = new System.Drawing.Size(150, 30);
            this.btnEditDHCP.Text = "Editar DHCP";
            this.btnEditDHCP.Click += new System.EventHandler(this.btnEditDHCP_Click);

            this.btnDeleteDHCP.Location = new System.Drawing.Point(474, 10);
            this.btnDeleteDHCP.Size = new System.Drawing.Size(150, 30);
            this.btnDeleteDHCP.Text = "Apagar DHCP";
            this.btnDeleteDHCP.Click += new System.EventHandler(this.btnDeleteDHCP_Click);

            // Adiciona controles à aba
            this.tabDHCP.Controls.Add(this.btnListarDHCP);
            this.tabDHCP.Controls.Add(this.btnAddDHCP);
            this.tabDHCP.Controls.Add(this.btnEditDHCP);
            this.tabDHCP.Controls.Add(this.btnDeleteDHCP);
            this.tabDHCP.Controls.Add(this.dgvDHCP);
        }

        private void InitializeDNSTab()
        {
            this.tabDNS.Text = "DNS";
            this.btnConfigurarDNS = new System.Windows.Forms.Button();
            this.btnToggleDNS = new System.Windows.Forms.Button();

            // Configurações dos botões
            this.btnConfigurarDNS.Location = new System.Drawing.Point(6, 10);
            this.btnConfigurarDNS.Size = new System.Drawing.Size(180, 30);
            this.btnConfigurarDNS.Text = "Configurar DNS";
            this.btnConfigurarDNS.Click += new System.EventHandler(this.btnConfigurarDNS_Click);
            this.tabDNS.Controls.Add(this.btnConfigurarDNS);

            this.btnToggleDNS.Location = new System.Drawing.Point(200, 10);
            this.btnToggleDNS.Size = new System.Drawing.Size(180, 30);
            this.btnToggleDNS.Text = "Ativar/Desativar DNS";
            this.btnToggleDNS.Click += new System.EventHandler(this.btnToggleDNS_Click);
            this.tabDNS.Controls.Add(this.btnToggleDNS);
        }

        private void InitializeRoutesTab()
        {
            this.tabRoutes.Text = "Rotas";
            this.dgvRoutes = new System.Windows.Forms.DataGridView();
            this.btnListarRotas = new System.Windows.Forms.Button();
            this.btnCriarRota = new System.Windows.Forms.Button();
            this.btnEditarRota = new System.Windows.Forms.Button();
            this.btnApagarRota = new System.Windows.Forms.Button();

            // Configurações do DataGridView
            this.dgvRoutes.Location = new System.Drawing.Point(6, 50);
            this.dgvRoutes.Size = new System.Drawing.Size(740, 420);
            this.dgvRoutes.ColumnCount = 3;
            this.dgvRoutes.Columns[0].Name = "ID";
            this.dgvRoutes.Columns[1].Name = "Destino (dst-address)";
            this.dgvRoutes.Columns[2].Name = "Gateway";

            // Configurações dos botões
            this.btnListarRotas.Location = new System.Drawing.Point(6, 10);
            this.btnListarRotas.Size = new System.Drawing.Size(150, 30);
            this.btnListarRotas.Text = "Listar Rotas";
            this.btnListarRotas.Click += new System.EventHandler(this.btnListarRotas_Click);

            this.btnCriarRota.Location = new System.Drawing.Point(162, 10);
            this.btnCriarRota.Size = new System.Drawing.Size(150, 30);
            this.btnCriarRota.Text = "Criar Rota";
            this.btnCriarRota.Click += new System.EventHandler(this.btnCriarRota_Click);

            this.btnEditarRota.Location = new System.Drawing.Point(318, 10);
            this.btnEditarRota.Size = new System.Drawing.Size(150, 30);
            this.btnEditarRota.Text = "Editar Rota";
            this.btnEditarRota.Click += new System.EventHandler(this.btnEditarRota_Click);

            this.btnApagarRota.Location = new System.Drawing.Point(474, 10);
            this.btnApagarRota.Size = new System.Drawing.Size(150, 30);
            this.btnApagarRota.Text = "Apagar Rota";
            this.btnApagarRota.Click += new System.EventHandler(this.btnApagarRota_Click);

            // Adiciona controles à aba
            this.tabRoutes.Controls.Add(this.btnListarRotas);
            this.tabRoutes.Controls.Add(this.btnCriarRota);
            this.tabRoutes.Controls.Add(this.btnEditarRota);
            this.tabRoutes.Controls.Add(this.btnApagarRota);
            this.tabRoutes.Controls.Add(this.dgvRoutes);
        }

        private void InitializeIPTab()
        {
            this.tabIP.Text = "Endereços IP";
            this.dgvIPAddresses = new System.Windows.Forms.DataGridView();
            this.btnGetIP = new System.Windows.Forms.Button();
            this.btnAddIP = new System.Windows.Forms.Button();
            this.btnEditIP = new System.Windows.Forms.Button();
            this.btnDeleteIP = new System.Windows.Forms.Button();

            // Configurações do DataGridView
            this.dgvIPAddresses.Location = new System.Drawing.Point(6, 50);
            this.dgvIPAddresses.Size = new System.Drawing.Size(740, 420);
            this.dgvIPAddresses.ColumnCount = 3;
            this.dgvIPAddresses.Columns[0].Name = "ID";
            this.dgvIPAddresses.Columns[1].Name = "Endereço";
            this.dgvIPAddresses.Columns[2].Name = "Interface";

            // Configurações dos botões
            this.btnGetIP.Location = new System.Drawing.Point(6, 10);
            this.btnGetIP.Size = new System.Drawing.Size(150, 30);
            this.btnGetIP.Text = "Listar IPs";
            this.btnGetIP.Click += new System.EventHandler(this.btnGetIP_Click);

            this.btnAddIP.Location = new System.Drawing.Point(162, 10);
            this.btnAddIP.Size = new System.Drawing.Size(150, 30);
            this.btnAddIP.Text = "Adicionar IP";
            this.btnAddIP.Click += new System.EventHandler(this.btnAddIP_Click);

            this.btnEditIP.Location = new System.Drawing.Point(318, 10);
            this.btnEditIP.Size = new System.Drawing.Size(150, 30);
            this.btnEditIP.Text = "Editar IP";
            this.btnEditIP.Click += new System.EventHandler(this.btnEditIP_Click);

            this.btnDeleteIP.Location = new System.Drawing.Point(474, 10);
            this.btnDeleteIP.Size = new System.Drawing.Size(150, 30);
            this.btnDeleteIP.Text = "Apagar IP";
            this.btnDeleteIP.Click += new System.EventHandler(this.btnDeleteIP_Click);

            // Adiciona controles à aba
            this.tabIP.Controls.Add(this.btnGetIP);
            this.tabIP.Controls.Add(this.btnAddIP);
            this.tabIP.Controls.Add(this.btnEditIP);
            this.tabIP.Controls.Add(this.btnDeleteIP);
            this.tabIP.Controls.Add(this.dgvIPAddresses);
        }

        private void InitializeWireGuardTab()
        {
            this.tabWireGuard.Text = "WireGuard";
            this.dgvWireGuard = new System.Windows.Forms.DataGridView();
            this.btnGetWG = new System.Windows.Forms.Button();
            this.btnAddWG = new System.Windows.Forms.Button();
            this.btnDeleteWG = new System.Windows.Forms.Button();

            // Configurações do DataGridView
            this.dgvWireGuard.Location = new System.Drawing.Point(6, 50);
            this.dgvWireGuard.Size = new System.Drawing.Size(740, 420);
            this.dgvWireGuard.ColumnCount = 3;
            this.dgvWireGuard.Columns[0].Name = "ID";
            this.dgvWireGuard.Columns[1].Name = "Nome";
            this.dgvWireGuard.Columns[2].Name = "Endereço";

            // Configurações dos botões
            this.btnGetWG.Location = new System.Drawing.Point(6, 10);
            this.btnGetWG.Size = new System.Drawing.Size(150, 30);
            this.btnGetWG.Text = "Listar WG";
            this.btnGetWG.Click += new System.EventHandler(this.btnGetWG_Click);

            this.btnAddWG.Location = new System.Drawing.Point(162, 10);
            this.btnAddWG.Size = new System.Drawing.Size(150, 30);
            this.btnAddWG.Text = "Criar WG";
            this.btnAddWG.Click += new System.EventHandler(this.btnAddWG_Click);

            this.btnDeleteWG.Location = new System.Drawing.Point(318, 10);
            this.btnDeleteWG.Size = new System.Drawing.Size(150, 30);
            this.btnDeleteWG.Text = "Apagar WG";
            this.btnDeleteWG.Click += new System.EventHandler(this.btnDeleteWG_Click);

            this.btnAdicionarPeer = new System.Windows.Forms.Button();
            this.btnAdicionarPeer.Location = new System.Drawing.Point(474, 10);
            this.btnAdicionarPeer.Size = new System.Drawing.Size(150, 30);
            this.btnAdicionarPeer.Text = "Adicionar Peer";



            // Adiciona controles à aba
            this.tabWireGuard.Controls.Add(this.btnGetWG);
            this.tabWireGuard.Controls.Add(this.btnAddWG);
            this.tabWireGuard.Controls.Add(this.btnDeleteWG);
            this.tabWireGuard.Controls.Add(this.dgvWireGuard);
            this.tabWireGuard.Controls.Add(this.btnAdicionarPeer);

        }
    }
}