using LoginApp.DAL.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoginApp.Views
{
    public partial class UyeKayit
    {

        //Partial Class yazmamın sebebi: Kontrolleri başka class'lara yazarak kodun okunabilirliğini attırmaya çalıştım.

        /// <summary>
        /// Formu ilk açılışındaki (default) haline geri getiriyor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTemizle_Click(object sender, EventArgs e)
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
        }

        /// <summary>
        /// Kullanıcı kayıt olurken şifresini 2 defa alarak doğrulama yapıyorum.
        /// </summary>
        /// <param name="password">1. kez parolayı giriyor.</param>
        /// <param name="againPassword">2.kez aynı parolayı girmesi gerekiyor.</param>
        /// <returns></returns>
        private bool ValidationPassword(string password, string againPassword) => password == againPassword ? true : false;

        /// <summary>
        /// Girilen şifrenin 8 ile 16 karakter uzunluğunda olması gerekiyor.
        /// </summary>
        /// <param name="password">Girilen şifre</param>
        /// <returns></returns>
        private bool PassWordLenght(string password) => password.Length >= 8 && password.Length <= 16 ? true : false;

        /// <summary>
        /// Kullanıcının kayıt olurken alanların boş olmasının önüne geçiyorum.
        /// </summary>
        /// <param name="info">Information tablosu için gerekli olan bilgileri topluyor.</param>
        /// <param name="user">User tablosu için gerekli olan bilgileri topluyor.</param>
        /// <returns></returns>
        private bool TxtNullAble(Information info, Users user)
        {
            if (info.name.Trim() == "" || info.name.Equals("Adınızı Giriniz")) return false;
            else if (info.surname.Trim() == "" || info.surname.Equals("Soyadınızı Giriniz")) return false;
            else if (txtPasswordAgain.Text.Trim() == "" || txtPasswordAgain.Text.Equals("Şifrenizi (Tekrar) Giriniz")) return false;
            else if (user.password.Trim() == "" || user.password.Equals("Şifrenizi Giriniz")) return false;
            else if (user.username.Trim() == "" || user.username.Equals("Kullanıcı Adını Giriniz")) return false;

            return true;
        }

        private void chkParola_CheckedChanged(object sender, EventArgs e)
        {
            if (chkParola.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPasswordAgain.UseSystemPasswordChar = false;
            }

            else
            {
                txtPassword.UseSystemPasswordChar = true;
                txtPasswordAgain.UseSystemPasswordChar = true;
            }

        }
        private void txtName_Enter(object sender, EventArgs e)
        {
            if (txtName.Text == "Adınızı Giriniz")
            {
                txtName.Text = "";
                txtName.ForeColor = Color.Blue;
            }
        }
        private void txtLastName_Enter(object sender, EventArgs e)
        {
            if (txtLastName.Text == "Soyadınızı Giriniz")
            {
                txtLastName.Text = "";
                txtLastName.ForeColor = Color.Blue;
            }
        }
        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "Kullanıcı Adını Giriniz")
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Blue;
            }
        }
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Şifrenizi Giriniz")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Blue;
            }
        }
        private void txtPasswordAgain_Enter(object sender, EventArgs e)
        {
            if (txtPasswordAgain.Text == "Şifrenizi (Tekrar) Giriniz")
            {
                txtPasswordAgain.Text = "";
                txtPasswordAgain.ForeColor = Color.Blue;
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                txtName.Text = "Adınızı Giriniz";
                txtName.ForeColor = Color.Silver;
            }
        }
        private void txtLastName_Leave(object sender, EventArgs e)
        {
            if (txtLastName.Text == "")
            {
                txtLastName.Text = "Soyadınızı Giriniz";
                txtLastName.ForeColor = Color.Silver;
            }
        }
        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "Kullanıcı Adını Giriniz";
                txtUserName.ForeColor = Color.Silver;
            }
        }
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Şifrenizi Giriniz";
                txtPassword.ForeColor = Color.Silver;
            }
        }
        private void txtPasswordAgain_Leave(object sender, EventArgs e)
        {
            if (txtPasswordAgain.Text == "")
            {
                txtPasswordAgain.Text = "Şifrenizi (Tekrar) Giriniz";
                txtPasswordAgain.ForeColor = Color.Silver;
            }
        }

    }
}
