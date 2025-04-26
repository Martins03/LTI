namespace MikrotikManagerApp
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblBaseUrl;
        private System.Windows.Forms.TextBox txtBaseUrl;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ComboBox cmbProtocol;
        private System.Windows.Forms.Label lblProtocol;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblProtocol = new System.Windows.Forms.Label();
            this.cmbProtocol = new System.Windows.Forms.ComboBox();
            this.lblBaseUrl = new System.Windows.Forms.Label();
            this.txtBaseUrl = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Form
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 240);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "LoginForm";
            this.Text = "Login Mikrotik";

            // Font padrão
            var defaultFont = new System.Drawing.Font("Segoe UI", 10F);

            // lblProtocol
            this.lblProtocol.AutoSize = true;
            this.lblProtocol.Location = new System.Drawing.Point(15, 20);
            this.lblProtocol.Font = defaultFont;
            this.lblProtocol.Text = "Protocolo:";

            // cmbProtocol
            this.cmbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProtocol.Items.AddRange(new object[] { "http", "https" });
            this.cmbProtocol.Location = new System.Drawing.Point(100, 17);
            this.cmbProtocol.Size = new System.Drawing.Size(75, 25);
            this.cmbProtocol.Font = defaultFont;

            // lblBaseUrl
            this.lblBaseUrl.AutoSize = true;
            this.lblBaseUrl.Location = new System.Drawing.Point(15, 60);
            this.lblBaseUrl.Font = defaultFont;
            this.lblBaseUrl.Text = "Endereço IP:";

            // txtBaseUrl
            this.txtBaseUrl.Location = new System.Drawing.Point(130, 57);
            this.txtBaseUrl.Size = new System.Drawing.Size(200, 25);
            this.txtBaseUrl.Font = defaultFont;
            this.txtBaseUrl.Text = "192.169.79.1";

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(15, 100);
            this.lblUsername.Font = defaultFont;
            this.lblUsername.Text = "Utilizador:";

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(130, 97);
            this.txtUsername.Size = new System.Drawing.Size(200, 25);
            this.txtUsername.Font = defaultFont;

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(15, 140);
            this.lblPassword.Font = defaultFont;
            this.lblPassword.Text = "Senha:";

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(130, 137);
            this.txtPassword.Size = new System.Drawing.Size(200, 25);
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Font = defaultFont;

            // btnLogin
            this.btnLogin.Location = new System.Drawing.Point(15, 180);
            this.btnLogin.Size = new System.Drawing.Size(315, 35);
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Text = "Iniciar Sessão";
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // Adicionar controles ao formulário
            this.Controls.Add(this.lblProtocol);
            this.Controls.Add(this.cmbProtocol);
            this.Controls.Add(this.lblBaseUrl);
            this.Controls.Add(this.txtBaseUrl);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}