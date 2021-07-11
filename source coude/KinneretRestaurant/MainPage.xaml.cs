using System.Windows;
using System.Windows.Media;

namespace KinneretRestaurant
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {

        //current user the use this system 
        // firstName, surname, username, password, role
        public static string currentUser { get; set; } 


        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// open the the tables window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenTable_Click(object sender, RoutedEventArgs e)
        {
            new OpenTables().Show();
        }

        /// <summary>
        /// open the takeaway 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTakeAway_Click(object sender, RoutedEventArgs e)
        {
            if (!isWindowOpen("TakeAway"))
            {
                TakeAway takeAway = new TakeAway();
                takeAway.orderNumber = OpenTables.getOrderNumber();
                takeAway.Show();
            }
        }

        /// <summary>
        /// open the add user window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (!isWindowOpen("addUser")) { new addUser().Show(); }
        }

        /// <summary>
        /// ope the menu window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {        
            if (!isWindowOpen("Menu")) { new Menu().Show(); }        
        }


        /// <summary>
        /// ope add table winodow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTable_Click(object sender, RoutedEventArgs e)
        {
            if (!isWindowOpen("addTable")) { new addTable().Show(); }
        }

        /// <summary>
        /// open add products window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (!isWindowOpen("addProduct")) { new addProduct().Show(); }
        }

        /// <summary>
        /// open the oders window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            if (!isWindowOpen("Orders")) { new Orders().Show(); }
        }

        /// <summary>
        ///  this function check if the function with chosen title opened, for no need to open one again.
        /// </summary>
        /// <param name="name">string uwindow name or title</param>
        /// <returns>true/false if opened or not</returns>
        bool isWindowOpen(string name)
        {
            bool IsOpen = false;    
            foreach (Window win in Application.Current.Windows)
            {
                if (win.Title == name)
                {
                    IsOpen = true; break;
                }  
            }       
            return IsOpen;
        }

        /// <summary>
        /// grid load
        /// fetch all user info name, username, role.
        /// by the user role it will show the pictur on imageUser
        /// admin will view all the buttons.
        /// Waiter will view open table, takeaway' menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            string[] currentUserSplit = currentUser.Split(',');
            lblName.Content = " ! " + currentUserSplit[0];
            lbluserName.Content = currentUserSplit[2];
            lblRole.Content = currentUserSplit[4];
            if (currentUserSplit[4] == "מנהל")
            {
                imageUser.Source = new ImageSourceConverter().ConvertFromString(Constans.ASSETS + "Admin.png") as ImageSource;
   
            }
            else if (currentUserSplit[4] == "מלצר")
            {
                imageUser.Source = new ImageSourceConverter().ConvertFromString(Constans.ASSETS + "Waiter.png") as ImageSource;
                grid.Height = 510;
                Height = 510;
                btnAddEmployee.Visibility = btnAddTable.Visibility = btnAddProduct.Visibility = btnOrders.Visibility = Visibility.Hidden;
                label_Copy3.Visibility= label_Copy4.Visibility = label_Copy5.Visibility = label_Copy6.Visibility = Visibility.Hidden; 
            }
            else
            {
                imageUser.Source = new ImageSourceConverter().ConvertFromString(Constans.ASSETS + "Employee.png") as ImageSource;
                btnAddEmployee.Visibility = btnAddTable.Visibility = btnAddProduct.Visibility = btnOrders.Visibility = Visibility.Hidden;
                label_Copy3.Visibility = label_Copy4.Visibility = label_Copy5.Visibility = label_Copy6.Visibility = Visibility.Hidden;
                grid.Height = 510;
                Height = 510;
            }
        }
  
    }
}
