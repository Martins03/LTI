namespace MikrotikManagerApp
{
    partial class ConfigurarWirelessForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbInterface;
        private System.Windows.Forms.TextBox txtSSID;
        private System.Windows.Forms.ComboBox cmbSecurityProfile;
        private System.Windows.Forms.ComboBox cmbBand;
        private System.Windows.Forms.Label lblInterface;
        private System.Windows.Forms.Label lblSSID;
        private System.Windows.Forms.Label lblSecurityProfile;
        private System.Windows.Forms.Label lblBand;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbInterface = new System.Windows.Forms.ComboBox();
            this.txtSSID = new System.Windows.Forms.TextBox();
            this.cmbSecurityProfile = new System.Windows.Forms.ComboBox();
            this.cmbBand = new System.Windows.Forms.ComboBox();
            this.lblInterface = new System.Windows.Forms.Label();
            this.lblSSID = new System.Windows.Forms.Label();
            this.lblSecurityProfile = new System.Windows.Forms.Label();
            this.lblBand = new System.Windows.Forms.Label();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblInterface
            this.lblInterface.AutoSize = true;
            this.lblInterface.Location = new System.Drawing.Point(12, 15);
            this.lblInterface.Text = "Interface:";

            // cmbInterface
            this.cmbInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterface.Location = new System.Drawing.Point(120, 12);
            this.cmbInterface.Size = new System.Drawing.Size(200, 23);

            // lblSSID
            this.lblSSID.AutoSize = true;
            this.lblSSID.Location = new System.Drawing.Point(12, 50);
            this.lblSSID.Text = "SSID:";

            // txtSSID
            this.txtSSID.Location = new System.Drawing.Point(120, 47);
            this.txtSSID.Size = new System.Drawing.Size(200, 23);

            // lblSecurityProfile
            this.lblSecurityProfile.AutoSize = true;
            this.lblSecurityProfile.Location = new System.Drawing.Point(12, 85);
            this.lblSecurityProfile.Text = "Security Profile:";

            // cmbSecurityProfile
            this.cmbSecurityProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSecurityProfile.Location = new System.Drawing.Point(120, 82);
            this.cmbSecurityProfile.Size = new System.Drawing.Size(200, 23);

            // lblBand
            this.lblBand.AutoSize = true;
            this.lblBand.Location = new System.Drawing.Point(12, 120);
            this.lblBand.Text = "Banda:";

            // cmbBand
            this.cmbBand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBand.Location = new System.Drawing.Point(120, 117);
            this.cmbBand.Size = new System.Drawing.Size(200, 23);

            // btnAplicar
            this.btnAplicar.Location = new System.Drawing.Point(164, 160);
            this.btnAplicar.Size = new System.Drawing.Size(75, 30);
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);

            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(245, 160);
            this.btnCancelar.Size = new System.Drawing.Size(75, 30);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // ConfigurarWirelessForm
            this.ClientSize = new System.Drawing.Size(340, 210);
            this.Controls.Add(this.lblInterface);
            this.Controls.Add(this.cmbInterface);
            this.Controls.Add(this.lblSSID);
            this.Controls.Add(this.txtSSID);
            this.Controls.Add(this.lblSecurityProfile);
            this.Controls.Add(this.cmbSecurityProfile);
            this.Controls.Add(this.lblBand);
            this.Controls.Add(this.cmbBand);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configurar Wireless";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
