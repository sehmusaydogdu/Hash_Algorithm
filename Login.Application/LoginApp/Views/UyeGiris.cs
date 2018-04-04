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

        /// <summary>
        ///  Kullanıcı (şifreyi göster) seçeneğini işaretlerse şifreyi görebilecek ve seçeneği kaldırırsa şifre gizlenecek.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkParola_CheckedChanged(object sender, EventArgs e)
        {
            if (chkParola.Checked)
                txtPassword.UseSystemPasswordChar = false;
            else
                txtPassword.UseSystemPasswordChar = true;
        }

        /// <summary>
        /// Başlangıçta kullanıcıya yol göstermesi için veri giriş alanından yönlendirme yapıyorum
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text.Equals("Kullanıcı Adı"))
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Blue;
            }
        }

        /// <summary>
        /// Kullanıcı veri giriş alanına hiç bir değer girmez ise ipucunu tekrar gösteriyorum.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtUserName_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "Kullanıcı Adı";
                txtUserName.ForeColor = Color.Silver;
            }
        }

        /// <summary>
        ///  Başlangıçta kullanıcıya yol göstermesi için veri giriş alanından yönlendirme yapıyorum
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals("Parola"))
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Blue;
            }
        }

        /// <summary>
        /// Kullanıcı veri giriş alanına hiç bir değer girmez ise ipucunu tekrar gösteriyorum.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Parola";
                txtPassword.ForeColor = Color.Silver;
            }
        }


        /// <summary>
        /// Üye Kayıt ekranına yönlendiriyorum.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHesapOlustur_Click(object sender, EventArgs e)
        {
            UyeKayit uyeKayit = new UyeKayit();
            this.Hide();
            uyeKayit.Show();
        }


        /// <summary>
        /// Bilgileri Veritabanına gönderdiğim metotdur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnGirisYap_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                //Kullanıcının veritabanına ipucu olarak gösterdiğim verileri göndermesini engelliyorum.
                if (txtUserName.Text.Equals("Kullanıcı Adı") || txtPassword.Text.Equals("Parola"))
                    MessageBox.Show("Kullanıcı Adı ve Parola boş geçilemez.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                else
                {
                    btnGirisYap.BackColor = Color.Honeydew;
                    btnGirisYap.Text = "Gönderiliyor..";
                    btnGirisYap.Enabled = false;

                    using (PersonelEntities ctx = new PersonelEntities())  //Bağlantı nesnesi oluşturuyorum.
                    {
                        //Daha önceden aynı bilgilerle kayıt olmuş kullanıcı var mı? diye kontrol ediyorum.
                        //Böyle bir kullanıcı var ise (Users) tablosundan (userId,username,password)
                        Users user = await ctx.Users.FirstOrDefaultAsync(k => k.username.Equals(txtUserName.Text));

                        //(null) ise böyle bir kayıt bulunamadı.
                        if (user == null)
                            MessageBox.Show("Böyle bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        else
                        {
                            //Kayıt var ise (Information) tablosuna gidip bilgileri alıyorum. (userId,name,surname,creatime)
                            Information info = await ctx.Information.FirstOrDefaultAsync(k => k.userId == user.userId);

                            if (info == null)
                                MessageBox.Show("Kayıt Bulunamadı.");

                            else
                            {
                                //Kullanıcı login olurken aldğım şifreyi ilk giriş yaptığı tarih(private key) ile şifreliyorum.
                                string hashSifre = Cryptology.Controllers.HashCalculator.Cryptology(txtPassword.Text, info.createTime);

                                //şifreler doğru ise kullanıcıyı sisteme alıyorum
                                if (hashSifre.Equals(user.password))
                                    MessageBox.Show("Tebrikler. Kullanıcı adı ve Şifreyi doğru girdiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
