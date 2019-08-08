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
    public partial class Dashboard : Form
    {
        Client activeClient;

        public Dashboard(Client cln)
        {
            activeClient = cln;
            InitializeComponent();
        }

        internal Client ActiveClient { get => activeClient; set => activeClient = value; }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = activeClient.Fullname;
        }
    }
}
