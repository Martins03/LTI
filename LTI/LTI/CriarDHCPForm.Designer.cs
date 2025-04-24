namespace MikrotikManagerApp
{
    partial class CriarDHCPForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblInterface;
        private System.Windows.Forms.ComboBox cmbInterface;
        private System.Windows.Forms.Label lblPool;
        private System.Windows.Forms.ComboBox cmbPool;
        private System.Windows.Forms.Label lblLease;
        private System.Windows.Forms.TextBox txtLeaseTime;
        private System.Windows.Forms.Button btnCriar;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblInterface = new System.Windows.Forms.Label();
            this.cmbInterface = new System.Windows.Forms.ComboBox();
            this.lblPool = new System.Windows.Forms.Label();
            this.cmbPool = new System.Windows.Forms.ComboBox();
            this.lblLease = new System.Windows.Forms.Label();
            this.txtLeaseTime = new System.Windows.Forms.TextBox();
            this.btnCriar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblNome
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(12, 15);
            this.lblNome.Text = "Nome:";

            // txtNome
            this.txtNome.Location = new System.Drawing.Point(120, 12);
            this.txtNome.Size = new System.Drawing.Size(200, 23);

            // lblInterface
            this.lblInterface.AutoSize = true;
            this.lblInterface.Location = new System.Drawing.Point(12, 50);
            this.lblInterface.Text = "Interface:";

            // cmbInterface
            this.cmbInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterface.Location = new System.Drawing.Point(120, 47);
            this.cmbInterface.Size = new System.Drawing.Size(200, 23);
            this.cmbInterface.SelectedIndexChanged += new System.EventHandler(this.cmbInterface_SelectedIndexChanged);

            // lblPool
            this.lblPool.AutoSize = true;
            this.lblPool.Location = new System.Drawing.Point(12, 85);
            this.lblPool.Text = "Address Pool:";

            // cmbPool
            this.cmbPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPool.Location = new System.Drawing.Point(120, 82);
            this.cmbPool.Size = new System.Drawing.Size(200, 23);

            // lblLease
            this.lblLease.AutoSize = true;
            this.lblLease.Location = new System.Drawing.Point(12, 120);
            this.lblLease.Text = "Lease Time:";

            // txtLeaseTime
            this.txtLeaseTime.Location = new System.Drawing.Point(120, 117);
            this.txtLeaseTime.Size = new System.Drawing.Size(200, 23);

            // btnCriar
            this.btnCriar.Location = new System.Drawing.Point(164, 160);
            this.btnCriar.Size = new System.Drawing.Size(75, 30);
            this.btnCriar.Text = "Criar";
            this.btnCriar.Click += new System.EventHandler(this.btnCriar_Click);

            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(245, 160);
            this.btnCancelar.Size = new System.Drawing.Size(75, 30);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // CriarDHCPForm
            this.ClientSize = new System.Drawing.Size(340, 210);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblInterface);
            this.Controls.Add(this.cmbInterface);
            this.Controls.Add(this.lblPool);
            this.Controls.Add(this.cmbPool);
            this.Controls.Add(this.lblLease);
            this.Controls.Add(this.txtLeaseTime);
            this.Controls.Add(this.btnCriar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Criar Servidor DHCP";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
