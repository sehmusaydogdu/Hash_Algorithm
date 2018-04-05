using LoginApp.DAL.Models;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginApp.Views
{
    public partial class UyeKayit : Form
    {
        public UyeKayit()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Bilgileri Veritabanına gönderdiğim metotdur.
        /// </summary>
        /// <param name="user">Information tablosu için gerekli olan bilgileri Server'a gönderiyorum</param>
        /// <param name="info">User tablosu için gerekli olan bilgileri Server'a gönderiyorum</param>
        /// <returns></returns>
        private async Task OnSaveInformation(Users user, Information info)
        {
            btnKayitOl.BackColor = Color.Honeydew;
            btnKayitOl.Text = "Gönderiliyor..";
            btnKayitOl.Enabled = false;
            
            
            using (PersonelEntities ctx = new PersonelEntities()) //Bağlantı nesnesi oluşturuyorum.
            {
                //Daha önceden aynı bilgilerle kayıt olmuş kullanıcı var mı? diye kontrol ediyorum.
                Users record = await ctx.Users.FirstOrDefaultAsync(k => k.username.Equals(user.username));

                //Kayıt bulunamadı ise (null) değeri döndürüyorum ve bilgileri veritabanına kaydediyorum.
                if (record == null)
                {
                    //Kullanıcının girdiği şifreyi kendi hazırladığım hash algoritmasına şifreliyorum.( Sonuç : 48-bit dönecektir.)
                    user.password = Cryptology.Controllers.HashCalculator.Cryptology(user.password,info.createTime);
                    ctx.Users.Add(user);
                    ctx.Information.Add(info);

                    await ctx.SaveChangesAsync(); //Değişiklikleri kaydediyorum.
                    MessageBox.Show("Kayıt başarılı bir şekilde gerçekleştirildi.");  
                }
                else
                    MessageBox.Show("Girdiğiniz kullanıcı adı mevcut. Başka bir kullanıcı adı giriniz.");
            }

        }

        private async void BtnKayitOl_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                Information info = new Information { name = txtName.Text.ToUpper(), surname = txtLastName.Text.ToUpper(), createTime = DateTime.Now };
                Users user = new Users { username = txtUserName.Text, password = txtPassword.Text };

                if (TxtNullAble(info, user))
                {
                    if (ValidationPassword(txtPassword.Text, txtPasswordAgain.Text))
                    {
                        if (PassWordLenght(user.password))
                            await OnSaveInformation(user, info);

                        else
                            MessageBox.Show("Girilen şifre 8 ile 16 karakter arasında olmalıdır.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Girilen 2 şifre birbiri ile uyumlu değildir.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                    MessageBox.Show("Lütfen alanları boş geçmeyiniz.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata :  {ex.Message}","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                btnKayitOl.Text = "Kayıt Ol";
                btnKayitOl.Enabled = true;
                btnKayitOl.BackColor = Color.FromArgb(255, 192, 192);
            }
        }


        /// <summary>
        /// Üye Giriş ekranına yönlendiriyorum.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUyeKayit_Click(object sender, EventArgs e)
        {
            UyeGirisi uyeGirisi = new UyeGirisi();
            this.Hide();
            uyeGirisi.Show();
        }
    }
}
