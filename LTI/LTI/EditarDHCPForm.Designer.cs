namespace MikrotikManagerApp
{
    partial class EditarDHCPForm
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
        private System.Windows.Forms.Button btnGuardar;
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
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblNome
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(12, 15);
            this.lblNome.Size = new System.Drawing.Size(96, 15);
            this.lblNome.Text = "Nome do Servidor:";

            // txtNome
            this.txtNome.Location = new System.Drawing.Point(130, 12);
            this.txtNome.Size = new System.Drawing.Size(200, 23);

            // lblInterface
            this.lblInterface.AutoSize = true;
            this.lblInterface.Location = new System.Drawing.Point(12, 50);
            this.lblInterface.Size = new System.Drawing.Size(57, 15);
            this.lblInterface.Text = "Interface:";

            // cmbInterface
            this.cmbInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterface.Location = new System.Drawing.Point(130, 47);
            this.cmbInterface.Size = new System.Drawing.Size(200, 23);

            // lblPool
            this.lblPool.AutoSize = true;
            this.lblPool.Location = new System.Drawing.Point(12, 85);
            this.lblPool.Size = new System.Drawing.Size(83, 15);
            this.lblPool.Text = "Address Pool:";

            // cmbPool
            this.cmbPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPool.Location = new System.Drawing.Point(130, 82);
            this.cmbPool.Size = new System.Drawing.Size(200, 23);

            // lblLease
            this.lblLease.AutoSize = true;
            this.lblLease.Location = new System.Drawing.Point(12, 120);
            this.lblLease.Size = new System.Drawing.Size(73, 15);
            this.lblLease.Text = "Lease Time:";

            // txtLeaseTime
            this.txtLeaseTime.Location = new System.Drawing.Point(130, 117);
            this.txtLeaseTime.Size = new System.Drawing.Size(200, 23);

            // btnGuardar
            this.btnGuardar.Location = new System.Drawing.Point(174, 160);
            this.btnGuardar.Size = new System.Drawing.Size(75, 30);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(255, 160);
            this.btnCancelar.Size = new System.Drawing.Size(75, 30);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(350, 210);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblInterface);
            this.Controls.Add(this.cmbInterface);
            this.Controls.Add(this.lblPool);
            this.Controls.Add(this.cmbPool);
            this.Controls.Add(this.lblLease);
            this.Controls.Add(this.txtLeaseTime);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editar Servidor DHCP";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
