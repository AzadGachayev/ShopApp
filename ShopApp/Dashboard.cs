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
    public partial class Dashboard : Form
    {
        ShopEntities db = new ShopEntities();
        Clients activeClient;

        public Dashboard(Clients clnt)
        {
            activeClient = clnt;
            InitializeComponent();
        }
        private void FillOrderList()
        {
            dtgOrderList.DataSource = db.Orders.Where(o => o.ClientsID == activeClient.ID).Select(ord => new
            {
                ord.Products.Name,
                ord.Products.Price,
                ord.Amount,
                ord.BuyDate

            }).ToList();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Welcome " + activeClient.Fullname;
            FillOrderList();
            FillCategoryCombo();  
        }
        private void FillCategoryCombo()
        {
            cmbCategory.Items.AddRange(db.Category.Select(c => c.Name).ToArray());
        }

        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProductCombo();

        }
        private void FillProductCombo()
        {
            string ctgName = cmbCategory.Text;
            int ctgID = db.Category.FirstOrDefault(ctg => ctg.Name == ctgName).ID;
            cmbProduct.Items.Clear();
            cmbProduct.Items.AddRange(db.Products.Where(pr => pr.CategoryID == ctgID).Select(pr => pr.Name).ToArray());
        }

        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ProductName = cmbProduct.Text;
            if(ProductName!="")
            {
                Products selectProduct = db.Products.FirstOrDefault(pr => pr.Name == ProductName);
                if (selectProduct.Count == 0)
                {
                    lblStok.Text = "Stokda bu mehsuldan qalmayib!";
                    lblStok.Visible = true;
                }
                else
                {
                    lblPrice.Text = ((double)nmCount.Value * selectProduct.Price + "AZN").ToString();
                    lblStok.Text = string.Format("Stokda bu mehsuldan {0} eded qalib", selectProduct.Count);
                    lblStok.Visible = true;
                    lblPrice.Visible = true;
                    btnAddProduct.Enabled = true;
                    nmCount.Visible = true;
                }
            }
            else
            {
                nmCount.Visible = false;
            }
            
        }

        private void NmCount_ValueChanged(object sender, EventArgs e)
        {
            string productname = cmbProduct.Text;
            Products selectProduct = db.Products.FirstOrDefault(pr => pr.Name == ProductName);
            lblPrice.Text = ((double)nmCount.Value * selectProduct.Price + "AZN").ToString();
            lblPrice.Visible = true;
        }
        private void clearAll()
        {
            cmbCategory.Text = "";
            cmbProduct.Text = "";
            lblStok.Visible = false;
            lblPrice.Visible = false;
            nmCount.Visible = false;
         
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            string productName = cmbProduct.Text;
            string categoryName = cmbCategory.Text;

            int count = (int)nmCount.Value;

            if (Extensions.CheckInput(new string[] {
                productName,categoryName
            },string.Empty))
            {   
                Products selectedPro = db.Products.FirstOrDefault(pr => pr.Name == productName);
                if(nmCount.Value <= selectedPro.Count)
                {
                    Orders ord = new Orders();
                    ord.ClientsID = activeClient.ID;
                    ord.ProductID = selectedPro.ID;
                    ord.Price = (decimal)selectedPro.Price * count;
                    ord.BuyDate = DateTime.Now;
                    db.Orders.Add(ord);
                    db.SaveChanges();
                    selectedPro.Count = selectedPro.Count - count;
                    MessageBox.Show("Product succesfully buy!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearAll();
                    FillOrderList();
                }
                else
                {
                    lblError.Text = "Istediyiniz qeder mehsul qalmayib!";
                    lblError.Visible = true;
                }
       
            }
            else
            {
                lblError.Text = "Xanalari bosh buraxmayin!";
                lblError.Visible = true;
            }
           
        }
    }
}
