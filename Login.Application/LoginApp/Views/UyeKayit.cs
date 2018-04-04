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

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtName.Text = "Adınızı Giriniz"; 
            txtLastName.Text = "Soyadınızı Giriniz"; 
            txtUserName.Text = "Kullanıcı Adını Giriniz"; 
            txtPassword.Text = "Şifrenizi Giriniz"; 
            txtPasswordAgain.Text = "Şifrenizi (Tekrar) Giriniz";

            txtName.ForeColor = Color.Silver;
            txtLastName.ForeColor = Color.Silver;
            txtUserName.ForeColor = Color.Silver;
            txtName.ForeColor = Color.Silver;
            txtPassword.ForeColor = Color.Silver;
            txtPasswordAgain.ForeColor = Color.Silver;
            chkParola.Checked = false;
            MessageBox.Show("Temizlendi.");
        }

        private bool ValidationPassword(string password, string againPassword) => password == againPassword ? true : false;
        private bool PassWordLenght(string password) => password.Length >= 8 && password.Length <= 16 ? true : false;
        private bool TxtNullAble(Information info, Users user)
        {
            if (info.name.Trim() == "" || info.name == "Adınızı Giriniz") return false;
            else if (info.surname.Trim() == "" || info.surname == "Soyadınızı Giriniz") return false;
            else if (txtPasswordAgain.Text.Trim() == "" || txtPasswordAgain.Text == "Şifrenizi (Tekrar) Giriniz") return false;
            else if (user.password.Trim() == "" || user.password == "Şifrenizi Giriniz") return false;
            else if (user.username.Trim() == "" || user.username == "Kullanıcı Adını Giriniz") return false;

            return true;
        }
        private async Task OnSaveInformation(Users user, Information info)
        {
            btnKayitOl.BackColor = Color.Honeydew;
            btnKayitOl.Text = "Gönderiliyor..";
            btnKayitOl.Enabled = false;
            
            using (PersonelEntities ctx = new PersonelEntities())
            {
                Users record = await ctx.Users.FirstOrDefaultAsync(k => k.username.Equals(user.username));

                if (record == null)
                {
                    user.password = Cryptology.Controllers.HashCalculator.Cryptology(user.password,info.createTime);
                    ctx.Users.Add(user);
                    ctx.Information.Add(info);
                    await ctx.SaveChangesAsync();
                    MessageBox.Show("Kayıt başarılı bir şekilde gerçekleştirildi.");  
                }
                else
                    MessageBox.Show("Girdiğiniz kullanıcı adı mevcut. Başka bir kullanıcı adı giriniz.");
            }

        }

        private async void btnKayitOl_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                Information info = new Information { name = txtName.Text, surname = txtLastName.Text, createTime = DateTime.Now };
                Users user = new Users { username = txtUserName.Text, password = txtPassword.Text };

                if (TxtNullAble(info, user))
                {
                    if (ValidationPassword(txtPassword.Text, txtPasswordAgain.Text))
                    {
                        if (PassWordLenght(user.password))
                        {
                            await OnSaveInformation(user, info);
                            btnKayitOl.Text = "Kayıt Ol";
                            btnKayitOl.Enabled = true;
                            btnKayitOl.BackColor = Color.FromArgb(255, 192, 192);
                        }
                        else
                            MessageBox.Show("Girilen şifre 8 ile 16 karakter arasında olmalıdır.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Girilen 2 şifre birbiri ile uyumlu değildir.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                    MessageBox.Show("Lütfen alanları boş geçmeyiniz.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(System.Data.Entity.Core.EntityException)
            {
                MessageBox.Show("Hata :  İnternet Bağlantınızı kontrol ediniz","Bağlantı Hatası",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata :  {ex.Message}","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        


        private void btnUyeKayit_Click(object sender, EventArgs e)
        {
            UyeGirisi uyeGirisi = new UyeGirisi();
            this.Hide();
            uyeGirisi.Show();
        }
    }
}
