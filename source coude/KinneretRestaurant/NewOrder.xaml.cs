using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace KinneretRestaurant
{


    public class DataGridItem
    {
  
        public string product { set; get; }
        public string price { set; get; }
        public string quantity { set; get; }


    }
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    /// 
    public partial class NewOrder : Window
    {
        public int orderNumber; //the order number
        public string tableNumber { get; set; } //in which table
        public string chairs { get; set; } //how many chairs
        int total;
        Order order = null;
        public NewOrder()
        {
            InitializeComponent();
        }
        public void updateOrder(string[] orderSplit)
        {
            order = new Order(orderSplit[0], orderSplit[1], orderSplit[2], orderSplit[3], orderSplit[4], orderSplit[5], orderSplit[6], orderSplit[7], orderSplit[8]);
        }

        /// <summary>
        /// grid load
        /// get menu data, fill the comboBox accroding to chairs number, get new order number, view table + order number/
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
  
            dataGridView1.Columns.Add(new DataGridTextColumn() { Header = "product", Binding = new Binding("product")}); ;
            dataGridView1.Columns.Add(new DataGridTextColumn() { Header = "price", Binding = new Binding("price") });
            dataGridView1.Columns.Add(new DataGridTextColumn() { Header = "quantity", Binding = new Binding("quantity") });
           
            if (order != null)
                fillOrderDat();

            fillComboBox();
            filMenu();
            lblOrderNumber.Content = orderNumber + "";
            lblTable.Content = tableNumber;
        }

        /// <summary>
        /// this function fills the order data when open order laready in waiting (table with red color)
        /// show all the order data include the products/
        /// </summary>
        void fillOrderDat()
        {
            orderNumber = int.Parse(order.number);
            tableNumber = order.tableOr;
            txtBoxName.Text = order.name;
            comboBoxPeople.Text = order.numofPeople;
            fillProductsToGV1(order.products);
            total = calcTotal();
            lbltotal.Content = " ₪ " + total + "";

        }

        /// <summary>
        /// 
        /// fill the comboBoxPeople with numbers
        /// fill the comboBoxQuantity with numbers
        /// </summary>
        void fillComboBox()
        {
            comboBoxPeople.Items.Clear();
            comboBoxQuantity.Items.Clear();
            
            for (int index = 1; index <= int.Parse(chairs); index++)
                comboBoxPeople.Items.Add(index);
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
        ///  read all the products and return itin format [product-price-quantity;product-price-quantity ...]
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
        /// add products to left dataGridView (order products)
        /// </summary>
        /// <param name="prod">string product</param>
        void fillProductsToGV1(string prod)
        {
            dataGridView1.ItemsSource = null;
            string[] productsList = prod.Split(';');
            foreach (string p in productsList)
            {
                string[] pSplit = p.Split('-');
                if (pSplit.Length != 3)
                    continue;
                dataGridView1.Items.Add(new DataGridItem() { product = pSplit[0], price = pSplit[1], quantity = pSplit[2] });       
            }
        }
        /// <summary>
        /// button left click hadler, move product from menu to left dataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (comboBoxQuantity.Text == "")
                return;

            if (dataGridView.SelectedItems.Count > 0)
            {             
                foreach (var obj in dataGridView.SelectedItems) {
                    Product product = obj as Product;

                    if (!sendToLeftWithExist(product.product, comboBoxQuantity.Text))
                        dataGridView1.Items.Add(new DataGridItem() { product =product.product, price = product.price, quantity = comboBoxQuantity.Text });
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
                    dataGridItem.quantity =( int.Parse(dataGridItem.quantity) + int.Parse(pquantity))+"";
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedItems.Count > 0)
                dataGridView1.Items.Remove(dataGridView1.SelectedItem);

            dataGridView1.SelectedItems.Clear();
        }

        /// <summary>
        /// button save click handler, this will add the order to the queue
        /// changes the table color to red
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            total = calcTotal();
            lbltotal.Content = " ₪ " + total + "";
            OpenTables.addOrder(createOrder("", "לא שולם"));
        }

        /// <summary>
        /// button pay click handler, this will save the orders in order file
        /// changes the table to red, show message and clear to new order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPay_Click(object sender, EventArgs e)
        {  
            string paymentMethod = radioButtonCard.IsChecked == true ? "אשראי" : "מזומן";
            OpenTables.orderDone(createOrder(paymentMethod, "שולם"));
            clean();
            MessageBox.Show("שולם");
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
            string numofPeople = comboBoxPeople.Text;
            string products = readProducts();
            string tableOr = tableNumber;
            string paymentMethod = payMethod;
            string status = pStatus;   
            return string.Format(number + "," + createdBy + "," + time + "," + name + "," + numofPeople + "," + products + "," + tableOr + "," + paymentMethod + "," + status).Split(',');
        }

        /// <summary>
        /// clear all the controls for new order
        /// </summary>
        void clean()
        {
            orderNumber = OpenTables.getOrderNumber();
            lblOrderNumber.Content = orderNumber + "";
            txtBoxName.Text =   "";
            lbltotal.Content = "";
            comboBoxPeople.SelectedIndex = -1;
            order = null;
            dataGridView1.Items.Clear();

        }
    }
}
