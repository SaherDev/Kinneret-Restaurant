
using System.Collections.Generic;


namespace KinneretRestaurant
{
    class Product
    {
        public string product { get; set; }
        public string category { get; set; }
        public string price { get; set; }

        
        public Product(string product, string category, string price)
        {
            this.product = product;
            this.category = category;
            this.price = price;
        }



        public static void addProduct(string product, string category, string price)
        {
            List<string> products = new List<string>();
            products.Add(product + "," + category + "," + price);

            methodHelper.saveData(Constans.PRODUCT_PATH, products);
        }

        public static List<Product> getProducts()
        {
            List<string> products = methodHelper.getData(Constans.PRODUCT_PATH);
            List<Product> productList = new List<Product>();

            foreach (string line in products)
            {
                string[] lineSplit = line.Split(',');
                if (lineSplit.Length != 3) continue;
                productList.Add(new Product(lineSplit[0], lineSplit[1], lineSplit[2]));

            }

            return productList;
        }

        public static Product getProduct(string prod)
        {
            List<string> linnes = methodHelper.getData(Constans.PRODUCT_PATH);
            foreach (string line in linnes)
            {
                string[] lineSplit = line.Split(',');
                if (lineSplit.Length != 3) continue;
                if (lineSplit[0] == prod) { return new Product(lineSplit[0], lineSplit[1], lineSplit[2]); }

            }

            return new Product("null", "null", "null");
        }

        public static bool deleteProdyct(string product)
        {
            return methodHelper.deleteLine(Constans.PRODUCT_PATH, product, 0);
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", product, price);
        }

    }
}
