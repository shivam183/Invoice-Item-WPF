using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EF_LabExercise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Invoice_ItemEntities db = new Invoice_ItemEntities();
        public MainWindow()
        {
            InitializeComponent();
            //cboInvoiceID.ItemsSource = db.Invoices.Select(c => c.InvId).ToList();
            cboInvoiceID.ItemsSource = (from c in db.Invoices select c).ToList();
            cboInvoiceID_info.ItemsSource = db.Invoices.Select(c => c.InvId).ToList();
            cboInvoiceID.Items.Refresh();
        }

        private void RefreshInvoice(object sender, RoutedEventArgs e)
        {
            //dgrdInvoice.ItemsSource = db.Invoices.Select(c => new { c.InvId, c.Name, c.Address }).ToList();
            dgrdInvoice.ItemsSource = (from a in db.Invoices select new { a.InvId, a.Name, a.Address }).ToList();
        }

        private void RefreshItems(object sender, RoutedEventArgs e)
        {
            dgrgItem.ItemsSource = db.Items.Select(c => new { c.Id, c.InvoiceID, c.Name, c.Quantity, c.Price }).ToList();
            //dgrgItem.ItemsSource = (from a in db.Items select new {a.Id, a.InvoiceID, a.Name, a.Quantity, a.Price }).ToList();
        }

        private void AddInvoiceHandler(object sender, RoutedEventArgs e)
        {
           
                Invoice invoice = new Invoice
                {
                    InvId = int.Parse(txtInvoiceID.Text),
                    Name = txtName.Text,
                    Address = txtAddress.Text
                };
                db.Invoices.Add(invoice);
                db.SaveChanges();
                MessageBox.Show(String.Format("Sucessfully Added {0}", invoice.Name), "Add Invoice");
           
            
        }

        private void AddItemsHandler(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            if (cboInvoiceID.SelectedItem != null)
            {
                int.TryParse(Convert.ToString(cboInvoiceID.SelectedValue), out int IDvalue);
                item.InvoiceID = IDvalue;
                item.Name = txtItemName.Text;
                item.Quantity = int.Parse(txtQuantity.Text);
                item.Price = decimal.Parse(txtPrice.Text);
                db.Items.Add(item);
                db.SaveChanges();
                MessageBox.Show(String.Format("Sucessfully Added {0}", item.Name), "Add Item");
            }
            else
            {
                MessageBox.Show("Select InvoiceID please", "Error!");
                
            }
          
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnGetInfo(object sender, RoutedEventArgs e)
        {
            Invoice invoice = new Invoice();
            Item item = new Item();
            if (cboInvoiceID_info.SelectedValue != null)
            {
              
                txtNameOfPerson.Text = (from r in db.Invoices select r.Name).ToString();
                txtAddressInfo.Text = invoice.Address;
                txtNameOfFruit.Text = item.Name;
                txtQuantityInfo.Text = item.Quantity.ToString();
                txtPriceInfo.Text = item.Price.ToString();
            }
            else
            {
                MessageBox.Show("Select InvoiceID please", "Error!");
            }
        }
    }
    partial class Invoice
    {
        public override string ToString() => $"{InvId}";
      
    }
}
