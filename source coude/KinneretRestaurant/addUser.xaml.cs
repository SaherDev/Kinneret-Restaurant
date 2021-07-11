using System.Windows;

namespace KinneretRestaurant
{
    /// <summary>
    /// Interaction logic for addUser.xaml
    /// </summary>
    public partial class addUser : Window
    {
        public addUser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// button delte click handler, will delete selected user rom the list + users. file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count > 0)
            {
                User user = dataGridView.SelectedItem as User;

                if (User.deleteUser(user.username))
                    MessageBox.Show(" נמחק " + user.username);

                dataGridView.SelectedItems.Clear();
                updateUsersData();

            }
           
        }

        /// <summary>
        /// 
        ///  grid loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            updateUsersData();

        }

        /// <summary>
        /// fetch user data and view on the screen
        /// </summary>
        void updateUsersData()
        {          
            dataGridView.ItemsSource = User.getUsers();
        }

        /// <summary>
        ///  button add handler' will add new user to the list + users file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            //need to check if exist 
            if (isUserExist(textBoxUserName.Text))
            {
                MessageBox.Show("!!! שם משתמש קיים ");
                return;
            }
            User.addUser(txtBoxfirstName.Text, textBoxlast.Text, textBoxUserName.Text, textBoxpassword.Text, textBoxRole.Text);
            txtBoxfirstName.Text = textBoxlast.Text = textBoxUserName.Text = textBoxpassword.Text = textBoxRole.Text = "";
            updateUsersData();
        }

        /// <summary>
        ///   check if usernmae if exist in users data
        /// </summary>
        /// <param name="userName">username</param>
        /// <returns>true/false if the username alreadu exist</returns>
        bool isUserExist(string userName)
        {
            return User.getUser(userName).firstName == "null" ? false : true;
        }
    }
}
