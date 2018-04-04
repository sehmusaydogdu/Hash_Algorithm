namespace LoginApp
{
    partial class UyeGirisi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UyeGirisi));
            this.btnGirisYap = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.chkParola = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.groupKullaniciGirisi = new System.Windows.Forms.GroupBox();
            this.pictureUser = new System.Windows.Forms.PictureBox();
            this.btnHesapOlustur = new System.Windows.Forms.Button();
            this.groupKullaniciGirisi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUser)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGirisYap
            // 
            this.btnGirisYap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnGirisYap.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGirisYap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGirisYap.Location = new System.Drawing.Point(40, 328);
            this.btnGirisYap.Name = "btnGirisYap";
            this.btnGirisYap.Size = new System.Drawing.Size(252, 36);
            this.btnGirisYap.TabIndex = 5;
            this.btnGirisYap.Text = "Giriş Yap";
            this.btnGirisYap.UseVisualStyleBackColor = false;
            this.btnGirisYap.Click += new System.EventHandler(this.btnGirisYap_ClickAsync);
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUserName.ForeColor = System.Drawing.Color.Silver;
            this.txtUserName.Location = new System.Drawing.Point(40, 190);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(252, 24);
            this.txtUserName.TabIndex = 2;
            this.txtUserName.Text = "Kullanıcı Adı";
            this.txtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUserName.Enter += new System.EventHandler(this.txtUserName_Enter);
            this.txtUserName.Leave += new System.EventHandler(this.txtUserName_Leave);
            // 
            // chkParola
            // 
            this.chkParola.AutoSize = true;
            this.chkParola.Checked = true;
            this.chkParola.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkParola.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.chkParola.Location = new System.Drawing.Point(40, 281);
            this.chkParola.Name = "chkParola";
            this.chkParola.Size = new System.Drawing.Size(112, 19);
            this.chkParola.TabIndex = 1;
            this.chkParola.Text = "Şifreyi Göster";
            this.chkParola.UseVisualStyleBackColor = true;
            this.chkParola.CheckedChanged += new System.EventHandler(this.chkParola_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtPassword.ForeColor = System.Drawing.Color.Silver;
            this.txtPassword.Location = new System.Drawing.Point(40, 235);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(252, 24);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "Parola";
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // groupKullaniciGirisi
            // 
            this.groupKullaniciGirisi.Controls.Add(this.pictureUser);
            this.groupKullaniciGirisi.Controls.Add(this.btnHesapOlustur);
            this.groupKullaniciGirisi.Controls.Add(this.txtUserName);
            this.groupKullaniciGirisi.Controls.Add(this.txtPassword);
            this.groupKullaniciGirisi.Controls.Add(this.btnGirisYap);
            this.groupKullaniciGirisi.Controls.Add(this.chkParola);
            this.groupKullaniciGirisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupKullaniciGirisi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupKullaniciGirisi.Location = new System.Drawing.Point(30, 21);
            this.groupKullaniciGirisi.Name = "groupKullaniciGirisi";
            this.groupKullaniciGirisi.Size = new System.Drawing.Size(332, 442);
            this.groupKullaniciGirisi.TabIndex = 10;
            this.groupKullaniciGirisi.TabStop = false;
            this.groupKullaniciGirisi.Text = "Üye Giriş";
            // 
            // pictureUser
            // 
            this.pictureUser.Image = ((System.Drawing.Image)(resources.GetObject("pictureUser.Image")));
            this.pictureUser.Location = new System.Drawing.Point(114, 63);
            this.pictureUser.Name = "pictureUser";
            this.pictureUser.Size = new System.Drawing.Size(109, 95);
            this.pictureUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureUser.TabIndex = 6;
            this.pictureUser.TabStop = false;
            // 
            // btnHesapOlustur
            // 
            this.btnHesapOlustur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnHesapOlustur.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHesapOlustur.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnHesapOlustur.Location = new System.Drawing.Point(40, 380);
            this.btnHesapOlustur.Name = "btnHesapOlustur";
            this.btnHesapOlustur.Size = new System.Drawing.Size(252, 36);
            this.btnHesapOlustur.TabIndex = 6;
            this.btnHesapOlustur.Text = "Hesap Oluştur";
            this.btnHesapOlustur.UseVisualStyleBackColor = false;
            this.btnHesapOlustur.Click += new System.EventHandler(this.btnHesapOlustur_Click);
            // 
            // UyeGirisi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(393, 484);
            this.Controls.Add(this.groupKullaniciGirisi);
            this.Name = "UyeGirisi";
            this.Text = "Üye Giriş Sistemi";
            this.groupKullaniciGirisi.ResumeLayout(false);
            this.groupKullaniciGirisi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGirisYap;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.CheckBox chkParola;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.GroupBox groupKullaniciGirisi;
        private System.Windows.Forms.PictureBox pictureUser;
        private System.Windows.Forms.Button btnHesapOlustur;
    }
}

