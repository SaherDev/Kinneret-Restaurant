
using System.Collections.Generic;


namespace KinneretRestaurant
{
    class User
    {

        public string firstName { get; set; } 
        public string surname { get; set; }
        public string username { get; set; } //username to enter the system
        public string password { get; set; }
        public string role { get; set; }


        //cons
        public User(string firstName, string surname, string username, string password, string role)
        {
            this.firstName = firstName;
            this.surname = surname;
            this.username = username;
            this.password = password;
            this.role = role;
        }

        public User(User user)
        {
            this.firstName = user.firstName;
            this.surname = user.surname;
            this.username = user.username;
            this.password = user.password;
            this.role = user.role;
        }

        /// <summary>
        /// save new user to Users.txt in format [prop1,prop2,pro3...]
        /// </summary>
        /// <param name="firstName">string firstName</param>
        /// <param name="surname">string surname/last name</param>
        /// <param name="username">string username</param>
        /// <param name="password">string password</param>
        /// <param name="role">string role</param>
        public static void addUser(string firstName, string surname, string username, string password, string role)
        {
            List<string> users = new List<string>();
            users.Add(firstName + "," + surname + "," + username + "," + password + "," + role);

            methodHelper.saveData(Constans.USER_PATH, users);
        }


        /// <summary>
        /// fetch and find user bu user name from Users.txt
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>user/null</returns>
        public static User getUser(string userName)
        {
            List<string> users = methodHelper.getData(Constans.USER_PATH);
            foreach (string line in users)
            {
                string[] lineSplit = line.Split(',');
                if (lineSplit.Length != 5) continue;
                if (lineSplit[2] == userName) { return new User(lineSplit[0], lineSplit[1], lineSplit[2], lineSplit[3], lineSplit[4]); }

            }

            return new User("null", "null", "null", "null", "null");
        }

        /// <summary>
        /// fetch all the users from Users.txt
        /// </summary>
        /// <returns>list  of users</returns>
        public static List<User> getUsers()
        {
            List<string> users = methodHelper.getData(Constans.USER_PATH);
            List<User> userslist = new List<User>();

            foreach (string line in users)
            {
                string[] lineSplit = line.Split(',');
                if (lineSplit.Length != 5) continue;
                userslist.Add(new User(lineSplit[0], lineSplit[1], lineSplit[2], lineSplit[3], lineSplit[4]));

            }

            return userslist;
        }

        /// <summary>
        /// delete user from Users.txt
        /// </summary>
        /// <param name="userName">string username</param>
        /// <returns>true/false if succed</returns>
        public static bool deleteUser(string userName)
        {
            return methodHelper.deleteLine(Constans.USER_PATH, userName, 2);
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4}", firstName, surname, username, password, role);
        }

    }
}
