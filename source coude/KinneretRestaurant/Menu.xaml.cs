
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace KinneretRestaurant
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }
        /// <summary>
        /// grid load fetch and view the menu data on the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            List<Product> products = Product.getProducts().OrderBy(order => order.category).ToList();
            dataGridView.ItemsSource = products;
        }
    }
}
