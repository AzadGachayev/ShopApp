using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopApp
{
    public partial class Login : Form
    {
        ShopEntities db  = new ShopEntities();
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            if (Extensions.CheckInput(new string[] { email, password }, string.Empty))
            {
                Clients already = db.Clients.FirstOrDefault(clnt => clnt.Email == email);
                if (already != null)
                {
                    if (already.Password == password.PasswordHash())
                    {
                        if (already.Status == 1)
                        {
                            AdminPanel adm = new AdminPanel(already);
                            adm.ShowDialog();
                        }
                        else
                        {
                            if (ckRemember.Checked)
                            {
                                Properties.Settings.Default.Email = email;
                                Properties.Settings.Default.Password = password;
                                Properties.Settings.Default.Checked = true;
                                Properties.Settings.Default.Save();
                            }
                            else
                            {
                                Properties.Settings.Default.Email = "";
                                Properties.Settings.Default.Password = "";
                                Properties.Settings.Default.Checked = false;
                                Properties.Settings.Default.Save();
                            }
                            lblError.Visible = false;
                            Dashboard dsh = new Dashboard(already);
                            dsh.ShowDialog();
                        }
                    }
                    else
                    {
                        lblError.Text = "Shifre yanlisdir!";
                        lblError.Visible = true;
                    }
                    
                  

                }
                else
                {
                    lblError.Text = "Email yanlisdir.";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Xahis edirik xanalari tam doldurun :)";
                lblError.Visible = true;
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Checked) ;
            txtEmail.Text = Properties.Settings.Default.Email;
            txtPassword.Text = Properties.Settings.Default.Password;
            ckRemember.Checked = true;
        }
    }
}
