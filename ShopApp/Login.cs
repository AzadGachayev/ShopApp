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
            if(Extensions.CheckInput(new string[] { email, password}, string.Empty))
            {
                Clients already = db.Clients.FirstOrDefault(clnt => clnt.Email == email);
                if(already != null)
                {
                    if(already.Password == password.PasswordHash())
                    {
                        lblError.Visible = false;
                        Dashboard dsh = new Dashboard();
                        dsh.ShowDialog();
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
    }
}
