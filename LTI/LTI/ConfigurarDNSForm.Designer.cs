namespace MikrotikManagerApp
{
    partial class ConfigurarDNSForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblServers;
        private System.Windows.Forms.TextBox txtServers;
        private System.Windows.Forms.CheckBox chkAllowRemote;
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
            this.lblServers = new System.Windows.Forms.Label();
            this.txtServers = new System.Windows.Forms.TextBox();
            this.chkAllowRemote = new System.Windows.Forms.CheckBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblServers
            // 
            this.lblServers.AutoSize = true;
            this.lblServers.Location = new System.Drawing.Point(12, 20);
            this.lblServers.Name = "lblServers";
            this.lblServers.Size = new System.Drawing.Size(134, 15);
            this.lblServers.Text = "Servidores DNS (IP/IP):";
            // 
            // txtServers
            // 
            this.txtServers.Location = new System.Drawing.Point(15, 40);
            this.txtServers.Name = "txtServers";
            this.txtServers.Size = new System.Drawing.Size(300, 23);
            // 
            // chkAllowRemote
            // 
            this.chkAllowRemote.AutoSize = true;
            this.chkAllowRemote.Location = new System.Drawing.Point(15, 75);
            this.chkAllowRemote.Name = "chkAllowRemote";
            this.chkAllowRemote.Size = new System.Drawing.Size(166, 19);
            this.chkAllowRemote.Text = "Permitir requisições remotas";
            this.chkAllowRemote.UseVisualStyleBackColor = true;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(159, 110);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(75, 30);
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(240, 110);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 30);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // ConfigurarDNSForm
            // 
            this.ClientSize = new System.Drawing.Size(330, 160);
            this.Controls.Add(this.lblServers);
            this.Controls.Add(this.txtServers);
            this.Controls.Add(this.chkAllowRemote);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfigurarDNSForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configurar Servidor DNS";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
