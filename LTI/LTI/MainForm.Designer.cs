namespace MikrotikManagerApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabInterfaces;
        private System.Windows.Forms.TabPage tabWireless;
        private System.Windows.Forms.TabPage tabBridges;

        private System.Windows.Forms.DataGridView dgvInterfaces;
        private System.Windows.Forms.DataGridView dgvWireless;
        private System.Windows.Forms.DataGridView dgvBridges;
        private System.Windows.Forms.DataGridView dgvBridgePorts;

        private System.Windows.Forms.Button btnGetInterfaces;
        private System.Windows.Forms.Button btnGetWireless;
        private System.Windows.Forms.Button btnGetBridges;
        private System.Windows.Forms.Button btnAddBridge;
        private System.Windows.Forms.Button btnDeleteBridge;
        private System.Windows.Forms.Button btnEditBridge;
        private System.Windows.Forms.Button btnGetBridgePorts;

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

            this.dgvInterfaces = new System.Windows.Forms.DataGridView();
            this.dgvWireless = new System.Windows.Forms.DataGridView();
            this.dgvBridges = new System.Windows.Forms.DataGridView();
            this.dgvBridgePorts = new System.Windows.Forms.DataGridView();

            this.btnGetInterfaces = new System.Windows.Forms.Button();
            this.btnGetWireless = new System.Windows.Forms.Button();
            this.btnGetBridges = new System.Windows.Forms.Button();
            this.btnAddBridge = new System.Windows.Forms.Button();
            this.btnDeleteBridge = new System.Windows.Forms.Button();
            this.btnEditBridge = new System.Windows.Forms.Button();
            this.btnGetBridgePorts = new System.Windows.Forms.Button();

            this.tabControl.SuspendLayout();
            this.tabInterfaces.SuspendLayout();
            this.tabWireless.SuspendLayout();
            this.tabBridges.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterfaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWireless)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBridges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBridgePorts)).BeginInit();
            this.SuspendLayout();

            // tabControl
            this.tabControl.Controls.Add(this.tabInterfaces);
            this.tabControl.Controls.Add(this.tabWireless);
            this.tabControl.Controls.Add(this.tabBridges);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Size = new System.Drawing.Size(760, 537);
            this.tabControl.TabIndex = 0;

            // tabInterfaces
            this.tabInterfaces.Controls.Add(this.btnGetInterfaces);
            this.tabInterfaces.Controls.Add(this.dgvInterfaces);
            this.tabInterfaces.Text = "Interfaces";

            // tabWireless
            this.tabWireless.Controls.Add(this.btnGetWireless);
            this.tabWireless.Controls.Add(this.dgvWireless);
            this.tabWireless.Text = "Wireless";

            // tabBridges
            this.tabBridges.Controls.Add(this.btnGetBridges);
            this.tabBridges.Controls.Add(this.btnAddBridge);
            this.tabBridges.Controls.Add(this.btnDeleteBridge);
            this.tabBridges.Controls.Add(this.btnEditBridge);
            this.tabBridges.Controls.Add(this.btnGetBridgePorts);
            this.tabBridges.Controls.Add(this.dgvBridges);
            this.tabBridges.Controls.Add(this.dgvBridgePorts);
            this.tabBridges.Text = "Bridges";

            // dgvInterfaces
            this.dgvInterfaces.Location = new System.Drawing.Point(6, 50);
            this.dgvInterfaces.Size = new System.Drawing.Size(740, 420);
            this.dgvInterfaces.ColumnCount = 2;
            this.dgvInterfaces.Columns[0].Name = "Nome";
            this.dgvInterfaces.Columns[1].Name = "Tipo";

            // dgvWireless
            this.dgvWireless.Location = new System.Drawing.Point(6, 50);
            this.dgvWireless.Size = new System.Drawing.Size(740, 420);
            this.dgvWireless.ColumnCount = 2;
            this.dgvWireless.Columns[0].Name = "Nome";
            this.dgvWireless.Columns[1].Name = "Tipo";

            // dgvBridges
            this.dgvBridges.Location = new System.Drawing.Point(6, 50);
            this.dgvBridges.Size = new System.Drawing.Size(740, 200);
            this.dgvBridges.ColumnCount = 3;
            this.dgvBridges.Columns[0].Name = "ID";
            this.dgvBridges.Columns[1].Name = "Nome";
            this.dgvBridges.Columns[2].Name = "Tipo";

            // dgvBridgePorts
            this.dgvBridgePorts.Location = new System.Drawing.Point(6, 260);
            this.dgvBridgePorts.Size = new System.Drawing.Size(740, 210);
            this.dgvBridgePorts.ColumnCount = 2;
            this.dgvBridgePorts.Columns[0].Name = "Bridge";
            this.dgvBridgePorts.Columns[1].Name = "Interface";

            // btnGetInterfaces
            this.btnGetInterfaces.Location = new System.Drawing.Point(6, 10);
            this.btnGetInterfaces.Size = new System.Drawing.Size(150, 30);
            this.btnGetInterfaces.Text = "Listar Interfaces";
            this.btnGetInterfaces.Click += new System.EventHandler(this.btnGetInterfaces_Click);

            // btnGetWireless
            this.btnGetWireless.Location = new System.Drawing.Point(6, 10);
            this.btnGetWireless.Size = new System.Drawing.Size(150, 30);
            this.btnGetWireless.Text = "Listar Wireless";
            this.btnGetWireless.Click += new System.EventHandler(this.btnGetWireless_Click);

            // btnGetBridges
            this.btnGetBridges.Location = new System.Drawing.Point(6, 10);
            this.btnGetBridges.Size = new System.Drawing.Size(150, 30);
            this.btnGetBridges.Text = "Listar Bridges";
            this.btnGetBridges.Click += new System.EventHandler(this.btnGetBridges_Click);

            // btnAddBridge
            this.btnAddBridge.Location = new System.Drawing.Point(162, 10);
            this.btnAddBridge.Size = new System.Drawing.Size(150, 30);
            this.btnAddBridge.Text = "Criar Bridge";
            this.btnAddBridge.Click += new System.EventHandler(this.btnAddBridge_Click);

            // btnDeleteBridge
            this.btnDeleteBridge.Location = new System.Drawing.Point(318, 10);
            this.btnDeleteBridge.Size = new System.Drawing.Size(150, 30);
            this.btnDeleteBridge.Text = "Apagar Bridge";
            this.btnDeleteBridge.Click += new System.EventHandler(this.btnDeleteBridge_Click);

            // btnEditBridge
            this.btnEditBridge.Location = new System.Drawing.Point(474, 10);
            this.btnEditBridge.Size = new System.Drawing.Size(150, 30);
            this.btnEditBridge.Text = "Editar Bridge";
            this.btnEditBridge.Click += new System.EventHandler(this.btnEditBridge_Click);

            // btnGetBridgePorts
            this.btnGetBridgePorts.Location = new System.Drawing.Point(630, 10);
            this.btnGetBridgePorts.Size = new System.Drawing.Size(150, 30);
            this.btnGetBridgePorts.Text = "Listar Portas";
            this.btnGetBridgePorts.Click += new System.EventHandler(this.btnGetBridgePorts_Click);

            // MainForm
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.Text = "Gestor de Mikrotik";

            this.tabControl.ResumeLayout(false);
            this.tabInterfaces.ResumeLayout(false);
            this.tabWireless.ResumeLayout(false);
            this.tabBridges.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterfaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWireless)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBridges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBridgePorts)).EndInit();
            this.ResumeLayout(false);
        }
    }
}