using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace KinneretRestaurant
{
    /// <summary>
    /// Interaction logic for TakeAway.xaml
    /// </summary>
    public partial class TakeAway : Window
    {
        public int orderNumber; //order number
        int total;//total to pay 
        public TakeAway()
        {
            InitializeComponent();
        }

        /// <summary>
        /// grid load
        /// get menu data, fill the chairs number, get new order number, view order number/
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridView1.Columns.Add(new DataGridTextColumn() { Header = "product", Binding = new Binding("product") }); ;
            dataGridView1.Columns.Add(new DataGridTextColumn() { Header = "price", Binding = new Binding("price") });
            dataGridView1.Columns.Add(new DataGridTextColumn() { Header = "quantity", Binding = new Binding("quantity") });

            fillComboBox();
            filMenu();
            lblOrderNumber.Content = orderNumber + "";
          
        }

        /// <summary>
        /// fill the comboBoxQuantity with numbers
        /// </summary>
        void fillComboBox()
        {
            comboBoxQuantity.Items.Clear();
            for (int index = 1; index <= 5; index++)
                comboBoxQuantity.Items.Add(index);
        }

        /// <summary>
        ///  fill the menu with all products
        /// </summary>
        void filMenu()
        {
            dataGridView.ItemsSource = Product.getProducts();
            dataGridView.Columns.RemoveAt(1);
            dataGridView.Columns[0].SortDirection = ListSortDirection.Ascending;
        }


        /// <summary>
        /// button pay click handler, this will save the order in orders file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            string paymentMethod = radioButtonCard.IsChecked == true ? "אשראי" : "מזומן";
            OpenTables.orderDone(createOrder(paymentMethod, "שולם"));
            clean();
            MessageBox.Show("שולם");
        }

        /// <summary>
        /// button left click hadler, move product from menu to left dataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxQuantity.Text == "")
                return;

            if (dataGridView.SelectedItems.Count > 0)
            {
                foreach (var obj in dataGridView.SelectedItems)
                {
                    Product product = obj as Product;

                    if (!sendToLeftWithExist(product.product, comboBoxQuantity.Text))
                        dataGridView1.Items.Add(new DataGridItem() { product = product.product, price = product.price, quantity = comboBoxQuantity.Text });
                }

            }
            dataGridView.SelectedItems.Clear();
        }

        /// <summary>
        /// 
        /// this function add quantity for product already in the left side, product already choosen not duplicated just changed the quantity
        /// </summary>
        /// <param name="pproduct">string product name</param>
        /// <param name="pquantity">string product quantity</param>
        /// <returns>true/false if succed</returns>
        bool sendToLeftWithExist(string pproduct, string pquantity)
        {
            int index = 0;
            foreach (var obj in dataGridView1.Items)
            {
                DataGridItem dataGridItem = obj as DataGridItem;

                if (dataGridItem.product == pproduct)
                {
                    dataGridItem.quantity = (int.Parse(dataGridItem.quantity) + int.Parse(pquantity)) + "";

                    dataGridView1.Items[index] = dataGridItem;
                    dataGridView1.Items.Refresh();

                    return true;
                }
                index++;
            }

            return false;
        }

        /// <summary>
        /// delete botton click handler, delete chosen product from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView1.SelectedItems.Count > 0)
                dataGridView1.Items.Remove(dataGridView1.SelectedItem);

            dataGridView1.SelectedItems.Clear();

        }

        /// <summary>
        /// button calc click handler, this will calculate total to pay
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            total = calcTotal();
            lbltotal.Content = " ₪ " + total + "";
        }

        /// <summary>
        /// its calculate total 
        /// </summary>
        /// <returns> numeric number (total)</returns>
        int calcTotal()
        {
            int total = 0;

            foreach (var obj in dataGridView1.Items)
            {
                DataGridItem dataGridItem = obj as DataGridItem;
                if (dataGridItem.price != "" && dataGridItem.quantity != "")
                    total += int.Parse(dataGridItem.price) * int.Parse(dataGridItem.quantity);

            }

            return total;
        }


        /// <summary>
        /// create order string[] (order as string arry)
        /// </summary>
        /// <param name="payMethod">string paymethod</param>
        /// <param name="pStatus"></param>
        /// <returns>order as string arry</returns>
        string[] createOrder(string payMethod, string pStatus)
        {    
            string number = orderNumber + "";
            string createdBy = MainPage.currentUser.Split(',')[0] + "  " + MainPage.currentUser.Split(',')[1]; 
            string time = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            string name = txtBoxName.Text;
            string numofPeople = textBoxPeople.Text;
            string products = readProducts();
            string tableOr = Constans.TAKE_AWAY;
            string paymentMethod = payMethod;
            string status = pStatus;
            return string.Format(number + "," + createdBy + "," + time + "," + name + "," + numofPeople + "," + products + "," + tableOr + "," + paymentMethod + "," + status).Split(',');
        }

        /// <summary>
        /// read the products and return it in format [product-price-quantity;product-price-quantity ...]
        /// </summary>
        /// <returns>products as string</returns>
        string readProducts()
        {
            string products = "";
            foreach (var obj in dataGridView1.Items)
            {

                DataGridItem dataGridItem = obj as DataGridItem;
                if (dataGridItem.price != "" && dataGridItem.quantity != "")
                    products += dataGridItem.product + "-" + dataGridItem.price + "-" + dataGridItem.quantity + ";";

            }
            return products;
        }

        /// <summary>
        /// clear all the controls for new order
        /// </summary>
        void clean()
        {
            orderNumber = OpenTables.getOrderNumber();
            lblOrderNumber.Content = orderNumber + "";
            txtBoxName.Text = "";
            lbltotal.Content = "";
            textBoxPeople.Text = "";     
            dataGridView1.Items.Clear();

        }

    }
}
