using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KinneretRestaurant
{
    public partial class OpenTables : Form
    {
        static List<Table> tablesList = new List<Table>();
        static List<Button> buttons = new List<Button>();
        static List<Order> orders = new List<Order>();
        public OpenTables()
        {
            InitializeComponent();

        }


        /// <summary>
        /// this function view all tables as buttons, view it dynamically acroding to (window size, saved tables)
        /// 
        /// </summary>
        void addTables()
        {

            int height = this.Size.Height;
            int width = this.Size.Width;
            int widthOffset = 60;
            int heightOffset = 80;
            int btnWidth = 130;  // Button Widht
            int btnHeight = 130;  // Button Height
            int gap = 40;
            for (int i = 0; i < tablesList.Count; ++i)
            {
                if ((widthOffset + btnWidth) >= width)
                {
                    widthOffset = 60;
                    heightOffset = heightOffset + btnHeight + gap;

                    var button = new Button();
                    button.Size = new Size(btnWidth, btnHeight);
                    button.Name = "bntTable" + i + "";
                    button.Text = "" + tablesList[i].number;
                    button.ForeColor = Color.Black;
                    button.Font = new Font("David", 13F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    button.BackgroundImage = getImageByStatus(tablesList[i].status);
                    button.BackgroundImageLayout = ImageLayout.Zoom;
                    button.Click += btn_Click; // Button Click Event
                    button.Location = new Point(widthOffset, heightOffset);
                    buttons.Add(button);
                    Controls.Add(button);
                    widthOffset = widthOffset + (btnWidth) + gap;
                }

                else
                {
                    var button = new Button();
                    button.Size = new Size(btnWidth, btnHeight);
                    button.Name = "bntTable" + i + "";
                    button.Text = "" + tablesList[i].number;
                    button.ForeColor = Color.Black;
                    button.Font = new Font("David", 13F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    button.BackgroundImage = getImageByStatus(tablesList[i].status);
                    button.BackgroundImageLayout = ImageLayout.Zoom;
                    button.Click += btn_Click; // Button Click Event
                    button.Location = new Point(widthOffset, heightOffset);
                    buttons.Add(button);
                    Controls.Add(button);

                    widthOffset = widthOffset + (btnWidth) + gap;
                }
            }
        }

        /// <summary>
        ///   fetch all the saved tables and add them to the window dynamically, if there is no tables will close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTable_Load(object sender, EventArgs e)
        {
            tablesList = Table.getTables();
            if (tablesList.Count == 0)
            {
                MessageBox.Show(" !! אין שולחנות ");
                Close();
            }

            addTables();

            foreach(Button button in buttons)
            {
                if (findOrder(button.Text))
                    changeTableStatus(button.Text, "תפוס");
            }
            
        }


        /// <summary>
        /// generate new order number
        /// </summary>
        /// <returns>int new order number</returns>
        public static int getOrderNumber()
        {
            return orders.Count == 0 ? Order.getnewOrderNumber() : (int.Parse(orders[orders.Count - 1].number) + 1);
        }

        /// <summary>
        /// table button click handler, open new order or existed order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            NewOrder newOrder = new NewOrder();

            //check if order exist for this table
            if (findOrder(button.Text))
                newOrder.updateOrder(orders.Find(obj => obj.tableOr == button.Text).ToString().Split(','));
            else
                newOrder.orderNumber = orders.Count == 0 ? Order.getnewOrderNumber() : (int.Parse(orders[orders.Count - 1].number) + 1);

            newOrder.tableNumber = button.Text;
            newOrder.chairs = tablesList.Find(x => x.number == button.Text).chairs;
            newOrder.Show();
        }

        /// <summary>
        /// find if the order contain table number
        /// </summary>
        /// <param name="tableNumber">string table number</param>
        /// <returns>true/false</returns>
        static bool findOrder(string tableNumber)
        {
            var item = orders.Find(obj => obj.tableOr == tableNumber);

            return item == null ? false : true;
        }

        /// <summary>
        /// add new order to waiting queue, change the table status and color
        /// </summary>
        /// <param name="orderSplit">string[] order</param>
        public static void addOrder(string[] orderSplit)
        {
            // MessageBox.Show("bntTable" + orderSplit[6]);
            if (findOrder(orderSplit[6]))
                orders.RemoveAt(orders.FindIndex(obj => obj.tableOr == orderSplit[6]));

            //change table status to red
            orders.Add(new Order(orderSplit[0], orderSplit[1], orderSplit[2], orderSplit[3], orderSplit[4], orderSplit[5], orderSplit[6], orderSplit[7], orderSplit[8]));
            changeTableStatus(orderSplit[6], "תפוס");
        }

        /// <summary>
        /// delete order from waiting queue and save it to orders file, 
        /// change the table status and color
        /// </summary>
        /// <param name="orderSplit">string[] order</param>
        public static void orderDone(string[] orderSplit)
        {
            //delete order from list
            //save to file
            //change table status to green
            if (findOrder(orderSplit[6]))
                orders.RemoveAt(orders.FindIndex(obj => obj.tableOr == orderSplit[6]));
            Order.addOrder(orderSplit[0], orderSplit[1], orderSplit[2], orderSplit[3], orderSplit[4], orderSplit[5], orderSplit[6], orderSplit[7], orderSplit[8]);
            changeTableStatus(orderSplit[6], "ריק");

        }

        /// <summary>
        /// change table and button status
        /// </summary>
        /// <param name="table">string table number</param>
        /// <param name="status">string status</param>
        static void changeTableStatus(string table, string status)
        {

            foreach (Button btn in buttons)
            {
                if (btn.Text == table) 
                     btn.BackgroundImage = getImageByStatus(status);   
            
            }

            foreach(Table tbl in tablesList)
            {
                if (tbl.number == table)
                        tbl.status = status;
            }
        }

        
        /// <summary>
        ///  images bu status
        ///  
        /// </summary>
        /// <param name="status">string status</param>
        /// <returns>image</returns>
        static Image getImageByStatus(string status)
        {
            Image tableImage;
            if (status == "ריק")
                tableImage = Image.FromFile(Constans.ASSETS + "TableGreen.png");
            else if (status == "תפוס")
                tableImage = Image.FromFile(Constans.ASSETS + "TableRed.png");
            else
                tableImage = Image.FromFile(Constans.ASSETS + "Table.png");

            return tableImage;

        }
    }
}
