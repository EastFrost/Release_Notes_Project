using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ReleaseNotesProject
{


    
    public class Connector
    {

        //set connection values for test db
        private static string Server { get; set; } = "localhost";
        private static string DatabaseName { get; set; } = "tcschema";
        private static string UserName { get; set; } = "root";
        private static string Password { get; set; } = "root";
        private static string connstring { get; set; } = string.Format("Server={0};Initial Catalog={1};User ID={2};Password={3};SslMode=None", Server, DatabaseName, UserName, Password);


        //set private default constructor as making connector objects is not neccassary
        private Connector()
        {
  
        }

        //construct insert sql command for new note creation
        public static string InsertNewNoteCommandText(string GUID, string appName, string noteContents, string creator, string dateCreated)
        {

            string command = $"INSERT INTO notes VALUES (\"{GUID}\",\"{appName}\",\"{noteContents}\",\"{creator}\",\"{dateCreated}\");";
            return command;

        }

        //construct insert sql command for new app creation
        public static string InsertNewAppCommandText(string appName)
        {
            
            string command = $"INSERT INTO apps (appname) VALUES (\"{appName}\");";
            return command;

        }

        //construct insert sql command for new user creation
        public static string InsertNewUserCommandText(string email, string password, string name)
        {

            string command = $"INSERT INTO users (username, password, name) VALUES (\"{email}\",\"{password}\",\"{name}\");";
            return command;

        }




        public static void InsertNewNote(string GUID, string appName, string noteContents, string creator, string dateCreated)
        {
            //using statements for disposal of connection and command objects
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    cmd.CommandText = InsertNewNoteCommandText(GUID,appName,noteContents,creator,dateCreated);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }

        }

        //
        public static void InsertNewApp(string appName)
        {
            //using statements for disposal of connection and command objects
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    cmd.CommandText = InsertNewAppCommandText(appName);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }

        }

        //
        public static void InsertNewUser(string email, string password, string name)
        {
            //using statements for disposal of connection and command objects
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    cmd.CommandText = InsertNewUserCommandText(email, password, name);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }

        }




        //read existing users from db and load them into a list for later assignment
        public static List<Users> LoadUsers()
        {

            List<Users> allUsers = new List<Users>();
            //using statements for disposal of connection and reader objects
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                string cmdString = $"select * from users";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = cmdString;

                conn.Open();
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read()) { 
                    
                       allUsers.Add(new Users(rdr["username"].ToString(), rdr["password"].ToString(), rdr["name"].ToString(), int.Parse(rdr["isAdmin"].ToString())));
                        Console.WriteLine($"(...) user:{allUsers.Last().email}<>pass:{allUsers.Last().password}");


                    }

                    conn.Close();
                }
            }



            return allUsers;

        }



        //read existing notes from db and load them into a list for later use
        public static List<ReleaseNote> LoadNotes()
        {


            List<ReleaseNote> releaseNotes = new List<ReleaseNote>();



            //using statements for disposal of connection and reader objects
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                string cmdString = $"select * from notes";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = cmdString;

                conn.Open();
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    
                    while (rdr.Read())
                    {
                        releaseNotes.Add(new ReleaseNote(rdr["GUID"].ToString(),
                            rdr["App Name"].ToString(),
                            rdr["Note Contents"].ToString(),
                            rdr["Creator"].ToString(),
                            rdr["Date Created"].ToString()
                            ));
                    }

                    conn.Close();
                }
            }


            return releaseNotes;

        }

        //

        //read existing apps from db and load them into a list for later use
        public static List<SQLApps> LoadApps()
        {


            List<SQLApps> Apps = new List<SQLApps>();



            //using statements for disposal of connection and reader objects
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                string cmdString = $"select * from apps";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = cmdString;

                conn.Open();
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {

                    while (rdr.Read())
                    {
                        Apps.Add(new SQLApps(rdr["idapps"].ToString(), rdr["appname"].ToString()));
                    }

                    conn.Close();
                }
            }


            return Apps;

        }


    }
}
