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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KinneretRestaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// login  button click handler, check if the username + password are correct
        /// incorrect username or password show error message
        /// open the main page, send the user info to view in mai page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            User user = User.getUser(txtBoxUser.Text);
            if (user.password == textBoxPassword.Text)
            {
                this.Hide();
                MainPage mainPage = new MainPage();
                MainPage.currentUser = user.ToString();
                mainPage.Closed += (s, args) => this.Close();
                mainPage.Show();
            }
            else MessageBox.Show("שם משתמש או סיסמה שגויים");
        }
    }
}
