namespace MikrotikManagerApp
{
    partial class EditarRotaForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblDst;
        private System.Windows.Forms.Label lblGateway;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.TextBox txtDst;
        private System.Windows.Forms.TextBox txtGateway;
        private System.Windows.Forms.TextBox txtDistance;
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
            this.lblDst = new System.Windows.Forms.Label();
            this.lblGateway = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.txtDst = new System.Windows.Forms.TextBox();
            this.txtGateway = new System.Windows.Forms.TextBox();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblDst
            this.lblDst.AutoSize = true;
            this.lblDst.Location = new System.Drawing.Point(12, 15);
            this.lblDst.Name = "lblDst";
            this.lblDst.Size = new System.Drawing.Size(76, 15);
            this.lblDst.Text = "Dst Address:";

            // txtDst
            this.txtDst.Location = new System.Drawing.Point(120, 12);
            this.txtDst.Size = new System.Drawing.Size(200, 23);

            // lblGateway
            this.lblGateway.AutoSize = true;
            this.lblGateway.Location = new System.Drawing.Point(12, 50);
            this.lblGateway.Name = "lblGateway";
            this.lblGateway.Size = new System.Drawing.Size(57, 15);
            this.lblGateway.Text = "Gateway:";

            // txtGateway
            this.txtGateway.Location = new System.Drawing.Point(120, 47);
            this.txtGateway.Size = new System.Drawing.Size(200, 23);

            // lblDistance
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(12, 85);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(56, 15);
            this.lblDistance.Text = "Distance:";

            // txtDistance
            this.txtDistance.Location = new System.Drawing.Point(120, 82);
            this.txtDistance.Size = new System.Drawing.Size(200, 23);

            // btnAplicar
            this.btnAplicar.Location = new System.Drawing.Point(164, 120);
            this.btnAplicar.Size = new System.Drawing.Size(75, 30);
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);

            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(245, 120);
            this.btnCancelar.Size = new System.Drawing.Size(75, 30);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // EditarRotaForm
            this.ClientSize = new System.Drawing.Size(340, 170);
            this.Controls.Add(this.lblDst);
            this.Controls.Add(this.txtDst);
            this.Controls.Add(this.lblGateway);
            this.Controls.Add(this.txtGateway);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.txtDistance);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editar Rota Estática";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
