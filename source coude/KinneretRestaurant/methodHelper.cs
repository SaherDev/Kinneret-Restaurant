
using System.Collections.Generic;
using System.IO;


namespace KinneretRestaurant
{
    class methodHelper
    {

        /// <summary>
        /// Service function for saving string to (txt) file. 
        /// 
        /// </summary>
        /// <param name="path">string file path</param>
        /// <param name="lines">list of string lines need to save</param>
        public static void saveData(string path, List<string> lines)
        {

            if (!Directory.Exists(Constans.DIRECTORY))
            {
                Directory.CreateDirectory(Constans.DIRECTORY);
            }

            using (StreamWriter writer = new StreamWriter(Constans.DIRECTORY + path, true))
            {
                foreach (string line in lines)
                    writer.WriteLine(line);
                writer.Close();
            }
        }

        /// <summary>
        /// service function that read from (txt) file all the lines and return it as list of string
        /// </summary>
        /// <param name="path">strinf file path</param>
        /// <returns>list of strings</returns>
        public static List<string> getData(string path)
        {
            List<string> lines = new List<string>();
            if(!File.Exists(Constans.DIRECTORY + path))
                using (StreamWriter w = File.AppendText(Constans.DIRECTORY + path)) { w.Close(); }

            using (StreamReader file = new StreamReader(Constans.DIRECTORY + path))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                    lines.Add(ln);
 
                file.Close();
            }
            return lines;
        }

        /// <summary>
        /// serveice function that delete lines from (txt) file that match prop[index]   
        /// line = [prop[0],prop[2]...]
        /// </summary>
        /// <param name="path">string file path</param>
        /// <param name="chosenTxt">string chosentxt that need to find</param>
        /// <param name="index">index</param>
        /// <returns></returns>
        public static bool deleteLine(string path, string chosenTxt, int index)
        {
            string line = null;
            bool isDeleted = false;
            List<string> lines = new List<string>();

            if (!File.Exists(Constans.DIRECTORY + path))
                using (StreamWriter w = File.AppendText(Constans.DIRECTORY + path)) { w.Close(); }

            using (StreamReader reader = new StreamReader(Constans.DIRECTORY + path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] lineSplit = line.Split(',');
                    lines.Add(line);
                    if (lineSplit.Length > index + 1) {
                        if (lineSplit[index] == chosenTxt)  {   lines.RemoveAt(lines.Count - 1); isDeleted = true; }}
                }

            }

            StreamWriter sw = new StreamWriter(Constans.DIRECTORY + path);
            foreach(string ln in lines)
                sw.WriteLine(ln);
            sw.Close();
            return isDeleted;
        }


    }
}
