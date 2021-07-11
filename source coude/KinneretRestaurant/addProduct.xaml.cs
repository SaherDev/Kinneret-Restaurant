using System;
using System.Windows;


namespace KinneretRestaurant
{
    /// <summary>
    /// Interaction logic for addProduct.xaml
    /// </summary>
    public partial class addProduct : Window
    {
        public addProduct()
        {
            InitializeComponent();
        }

        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            updateProductsData();
        }

        /// <summary>
        /// 
        ///   fetch and view the all the products on dataGridView
        /// </summary>
        void updateProductsData()
        {
            dataGridView.ItemsSource = Product.getProducts();
        }


        /// <summary>
        /// 
        ///check if product name exist in the Products file
        /// </summary>
        /// <param name="prod">the product namr</param>
        /// <returns>true/false if the the fie exist</returns>
        bool isProductExist(string prod)
        {
            return Product.getProduct(prod).product == "null" ? false : true;
        }

        /// <summary>
        /// 
        /// button delete event that deleted choosen item from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedItems.Count > 0)
            {
                    Product product = dataGridView.SelectedItem as Product;
                    if (Product.deleteProdyct(product.product))
                        MessageBox.Show("  נמחק  " + product.product);

                dataGridView.SelectedItems.Clear();
                updateProductsData();
            }
        }


        /// <summary>
        /// 
        /// button add click event to save the product to product txt file
        /// if it exist its will show error message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnadd_Click(object sender, EventArgs e)
        {
            //need to check if exist 
            if (isProductExist(textBoxProduct.Text))
            {
                MessageBox.Show("!!!  מוצר קיים ");
                return;
            }
            Product.addProduct(textBoxProduct.Text, textBoxCategory.Text, textBoxPrice.Text);
            textBoxProduct.Text = textBoxCategory.Text = textBoxPrice.Text = "";
            updateProductsData();
        }

    }
}
