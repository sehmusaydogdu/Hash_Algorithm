using LoginApp.DAL.Models;
using LoginApp.Views;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Windows.Forms;

namespace LoginApp
{
    public partial class UyeGirisi : Form
    {
        public UyeGirisi()
        {
            InitializeComponent();
        }

        //Kullanıcı (şifreyi göster) seçeneğini işaretlerse şifreyi görebilecek ve seçeneği kaldırırsa şifre gizlenecek.
        private void chkParola_CheckedChanged(object sender, EventArgs e)
        {
            if (chkParola.Checked)
                txtPassword.UseSystemPasswordChar = false;
            else
                txtPassword.UseSystemPasswordChar = true;
        }

        
        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text.Equals("Kullanıcı Adı"))
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Blue;
            }
        }
        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "Kullanıcı Adı";
                txtUserName.ForeColor = Color.Silver;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals("Parola"))
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Blue;
            }
        }
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Parola";
                txtPassword.ForeColor = Color.Silver;
            }
        }

        private void btnHesapOlustur_Click(object sender, EventArgs e)
        {
            UyeKayit uyeKayit = new UyeKayit();
            this.Hide();
            uyeKayit.Show();
        }

        private async void btnGirisYap_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text.Equals("Kullanıcı Adı") || txtPassword.Text.Equals("Parola"))
                    MessageBox.Show("Kullanıcı Adı ve Parola boş geçilemez.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                else
                {
                    btnGirisYap.BackColor = Color.Honeydew;
                    btnGirisYap.Text = "Gönderiliyor..";
                    btnGirisYap.Enabled = false;

                    using (PersonelEntities ctx = new PersonelEntities())
                    {
                        Users user = await ctx.Users.FirstOrDefaultAsync(k => k.username.Equals(txtUserName.Text));

                        if (user == null)
                            MessageBox.Show("Böyle bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        else
                        {
                            Information info = await ctx.Information.FirstOrDefaultAsync(k => k.userId == user.userId);
                            if (info == null)
                                MessageBox.Show("Kayıt Bulunamadı.");

                            else
                            {
                                string hashSifre = Cryptology.Controllers.HashCalculator.Cryptology(txtPassword.Text, info.createTime);

                                if (hashSifre.Equals(user.password))
                                    MessageBox.Show("Tebrikler şifreyi doğru girdiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("Hatalı giriş yaptınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            }

                        }
                    }
                }
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                MessageBox.Show("Hata :  İnternet Bağlantınızı kontrol ediniz", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata :  {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGirisYap.Text = "Giriş Yap";
                btnGirisYap.Enabled = true;
                btnGirisYap.BackColor = Color.FromArgb(255, 192, 192);
            }
        }
    }
}
