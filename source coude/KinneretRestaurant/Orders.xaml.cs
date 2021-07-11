using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace KinneretRestaurant
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        public Orders()
        {
            InitializeComponent();
        }

        /// <summary>
        /// grid load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            updateUrdersData();
            gridSide.Visibility = Visibility.Hidden;
        }
        
        /// <summary>
        /// fetch order data and view on then screen
        /// </summary>
        void updateUrdersData()
        {
            dataGridView.ItemsSource = Order.getOrders();
            dataGridView.Columns.RemoveAt(5);
        
        }

        /// <summary>
        /// view order products on the right side of the screen 
        /// </summary>
        /// <param name="product">string[] product</param>
        /// <param name="y">margin top </param>
        void viewOrderProdduct(string[] product, int y)
        {
            var Label = new Label() { 
                Margin= new Thickness(label4.Margin.Left, label4.Margin.Top + y, label4.Margin.Right, label4.Margin.Bottom),
                FontSize = label4.FontSize,
                Width = label4.Width,
                Height =label4.Height,
                Content = "         " + product[1] + "              " + product[2] + "           " + product[0],
                FontFamily = label4.FontFamily,

            };
            gridSide.Children.Add(Label);

        }

        /// <summary>
        /// show the order products when chose any item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count > 0)
            {
                gridSide.Children.Clear();//.Controls.Clear();
                gridSide.Visibility = Visibility.Visible;
                gridSide.Children.Add(label1);
                gridSide.Children.Add(label2);
                gridSide.Children.Add(label3);

                Order order = dataGridView.SelectedItem as Order;
                string[] products = order.products.Split(';');

                int y = 0;

                foreach (string prod in products)
                {
                    string[] productSplit = prod.Split('-');
                    if (productSplit.Length != 3)
                        continue;
                    viewOrderProdduct(productSplit, y);
                    y += 25;
                }
                dataGridView.SelectedItems.Clear();
            }
        }
    }
}
