using System.Windows;

namespace KinneretRestaurant
{
    /// <summary>
    /// Interaction logic for addTable.xaml
    /// </summary>
    public partial class addTable : Window
    {
        public addTable()
        {
            InitializeComponent();
        }

        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            updateTablesData();
        }

        /// <summary>
        /// 
        /// button delete event handler, will delete selected item from the list and data file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count > 0)
            {
                Table table = dataGridView.SelectedItem as Table;
                if (Table.deleteTable(table.number))
                    MessageBox.Show("  נמחק  שולחן " + table.number);

                dataGridView.SelectedItems.Clear();
                updateTablesData();
            }
        }

        /// <summary>
        /// 
        ///button add event handler, will add new product to the list + data file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            //need to check if exist 
            if (isTableExist(textBoxTableNumber.Text))
            {
                MessageBox.Show("!!! מספר שולחן קיים  ");
                return;
            }
            Table.addTable(textBoxTableNumber.Text, textBoxChaires.Text);
            textBoxTableNumber.Text = textBoxChaires.Text = "";
            updateTablesData();
        }

        /// <summary>
        /// fetch and view all the data and view on dataGridView
        /// </summary>
        void updateTablesData()
        {      
            dataGridView.ItemsSource = Table.getTables();
            dataGridView.Columns.RemoveAt(2);
        }

        /// <summary>
        /// check if table name exist in data file
        /// </summary>
        /// <param name="table"> string table name</param>
        /// <returns>truefalse if the table exist</returns>
        bool isTableExist(string table)
        {
            return Table.getTable(table).number == "null" ? false : true;
        }

    }
}
