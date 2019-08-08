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
    public partial class Register : Form
    {
        ShopEntities db = new ShopEntities();

        public Register()
        {
            InitializeComponent();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string fullname = txtFullname.Text;
            string email = textBox2.Text;
            string phone = txtPhone.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string[] CheckedInput = new string[] {
                fullname, email, phone, password, confirmPassword};

            if (Extensions.CheckInput(CheckedInput,string.Empty))
            {
                lblError.Visible = false;
                if(phone.Length < 15)
                {
                    if(password.Length > 7)
                    {
                        if(password == confirmPassword)
                        {
                            Clients cls = db.Clients.FirstOrDefault(c=>c.Email == email);
                            if(cls == null)
                            {
                                db.Clients.Add(new Clients()
                                {
                                    Fullname = fullname,
                                    Email = email,
                                    Phone = phone,
                                    Password = password.PasswordHash(),

                                });
                                db.SaveChanges();
                                MessageBox.Show("Yeni istifadeci elave olundu.", "Emeliyyat Ugurlu! :)", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                lblError.Text = "Bele bir emailden artiq istifade olunub.";
                                lblError.Visible = true;
                            }


                        }
                        else
                        {
                            lblError.Text = "Evvelki sifre ile sonraki sifrede uygunsuzluq var.";
                            lblError.Visible = true;
                        }
                    }
                    else
                    {

                        lblError.Text = "Shifre en azi 8 reqemli olmalidir";
                        lblError.Visible = true;
                    }
                }
                else
                {

                    lblError.Text = "O boyda telefon nomresi gormusen? Adam kimi duz emelli yig!";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Xahis edirik xanalari tam doldurun :)";
                lblError.Visible = true;
            }
    
         }

        private void TxtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar !=8) 
            {
                e.Handled = true;
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
