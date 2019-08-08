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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Welcome to Our Shop";
            pcImage.Image = new Bitmap(@"C:\Users\Lenovo\source\repos\ShopApp\shopping.png");
        }

        private void Label1_Click(object sender, EventArgs e)
        {
        
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            Register rs = new Register();

            rs.ShowDialog();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.ShowDialog();
        }
    }
}
