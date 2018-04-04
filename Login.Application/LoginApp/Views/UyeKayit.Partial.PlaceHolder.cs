using LoginApp.DAL.Models;
using System;
using System.Drawing;

namespace LoginApp.Views
{
    public partial class UyeKayit
    {

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
