using System.Collections.Generic;
namespace KinneretRestaurant
{
    class Table
    {
        public string number { get; set; }
        public string chairs { get; set; }
        public string status { get; set; }

        //cons
        public Table(string number, string chairs, string status)
        {
            this.number = number;
            this.chairs = chairs;
            this.status = status;
        }


        /// <summary>
        /// save new Table to Tables.txt file in format [prop1,prop2,pro3...]
        /// </summary>
        /// <param name="number">string tables number</param>
        /// <param name="chairs">string number of chairs</param>
        public static void addTable(string number, string chairs)
        {
            List<string> tables = new List<string>();
            tables.Add(number + "," + chairs);
            methodHelper.saveData(Constans.TABLE_PATH, tables);
        }

        /// <summary>
        /// delete table from Tables.txt file
        /// </summary>
        /// <param name="table">string table number</param>
        /// <returns>true/false if succed</returns>
        public static bool deleteTable(string table)
        {
            return methodHelper.deleteLine(Constans.TABLE_PATH, table, 0);
        }

        /// <summary>
        /// fetch all the tables from Tables.tx file
        /// </summary>
        /// <returns>list of tables</returns>
        public static List<Table> getTables()
        {
            List<string> lines = methodHelper.getData(Constans.TABLE_PATH);
            List<Table> tables = new List<Table>();

            foreach (string ln in lines)
            {
                string[] lnSplit = ln.Split(',');
                if (lnSplit.Length != 2) continue;
                tables.Add(new Table(lnSplit[0], lnSplit[1], "ריק"));

            }

            return tables;
        }

        /// <summary>
        /// ftech and find table by table number
        /// </summary>
        /// <param name="tbl">string table number</param>
        /// <returns>table/null</returns>
        public static Table getTable(string tbl)
        {
            List<string> linnes = methodHelper.getData(Constans.TABLE_PATH);
            foreach (string line in linnes)
            {
                string[] lineSplit = line.Split(',');
                if (lineSplit.Length != 2) continue;
                if (lineSplit[0] == tbl) { return new Table(lineSplit[0], lineSplit[1], "ריק"); }

            }
            return new Table("null", "null", "null");
        }

    }



}
