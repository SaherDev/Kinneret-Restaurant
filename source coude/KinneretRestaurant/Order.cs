
using System.Collections.Generic;
namespace KinneretRestaurant
{
    class Order
    {
        public string number { get; set; } //order number
        public string createdBy { get; set; } // user who create the order
        public string time { get; set; } //time create

        public string name { get; set; } //clirnt name
        public string numofPeople { get; set; }

        public string products { get; set; } 

        public string tableOr { get; set; } // table or takeaway

        public string paymentMethod { get; set; }
        public string status { get; set; }

        //cons
        public Order(string number, string createdBy, string time, string name, string numofPeople, string products, string tableOr, string paymentMethod, string status)
        {
            this.number = number;
            this.createdBy = createdBy;
            this.time = time;
            this.name = name;
            this.numofPeople = numofPeople;
            this.products = products;
            this.tableOr = tableOr;
            this.paymentMethod = paymentMethod;
            this.status = status;
        }

        /// <summary>
        /// save new order to Users.txt file in format [prop1,prop2,pro3...]
        /// </summary>
        /// <param name="number">order number</param>
        /// <param name="createdBy">user name</param>
        /// <param name="time">create time</param>
        /// <param name="name">customer name</param>
        /// <param name="numberOfPeople">number of cusyomers</param>
        /// <param name="products">product list</param>
        /// <param name="tableOr">table or takeaway</param>
        /// <param name="paymentMethod">paymentMethod(cash, card)</param>
        /// <param name="status">order status</param>
        public static void addOrder(string number, string createdBy, string time, string name, string numberOfPeople, string products, string tableOr, string paymentMethod, string status)
        {
            List<string> orders = new List<string>();
            orders.Add(number + "," + createdBy + "," + time + "," + name + "," + numberOfPeople + "," + products + "," + tableOr + "," + paymentMethod + "," + status);

            methodHelper.saveData(Constans.ORDER_PATH, orders);
        }

        /// <summary>
        /// fetch and find order by order number from Users.txt file
        /// </summary>
        /// <param name="number">string order number</param>
        /// <returns>order/ null</returns>
        public static Order getOrder(string number)
        {
            List<string> orders = methodHelper.getData(Constans.ORDER_PATH);
            foreach (string line in orders)
            {
                string[] lineSplit = line.Split(',');
                if (lineSplit.Length != 9) continue;
                if (lineSplit[0] == number)
                {
                    return new Order(lineSplit[0], lineSplit[1], lineSplit[2], lineSplit[3], lineSplit[4], lineSplit[5], lineSplit[6], lineSplit[7], lineSplit[8]);
                }

            }

            return new Order("null", "null", "null", "null", "null", "null", "null", "null", "null");
        }

        /// <summary>
        /// fetch all the orders from Orders,txt file
        /// </summary>
        /// <returns>listo of orders</returns>
        public static List<Order> getOrders()
        {
            List<string> orders = methodHelper.getData(Constans.ORDER_PATH);
            List<Order> ordersList = new List<Order>();

            foreach (string line in orders)
            {
                string[] lineSplit = line.Split(',');
                if (lineSplit.Length != 9) continue;
                ordersList.Add(new Order(lineSplit[0], lineSplit[1], lineSplit[2], lineSplit[3], lineSplit[4], lineSplit[5], lineSplit[6], lineSplit[7], lineSplit[8]));

            }

            return ordersList;
        }

        /// <summary>
        /// get the max + 1 order number
        /// 
        /// </summary>
        /// <returns>int new order number</returns>
        public static int getnewOrderNumber()
        {
            List<Order> ordersList = getOrders();
            int orderNumber = 1;
            foreach (Order order in ordersList)
            {
                if (int.Parse(order.number) > orderNumber)
                    orderNumber = int.Parse(order.number);
            }
            return ++orderNumber;
        }

        /// <summary>
        /// ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format(number + "," + createdBy + "," + time + "," + name + "," + numofPeople + "," + products + "," + tableOr + "," + paymentMethod + "," + status);
        }
    }
}
