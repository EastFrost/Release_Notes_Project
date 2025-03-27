using System.Security.Policy;

namespace ReleaseNotesProject
{
    public class Users
    {
        //set to be able to retreived freely but not written to
        public string email { get; private set; }
        public string password { get; private set; }
        public string name { get; private set; }
        //global currentUser Variable
        public static Users currentUser { get; set; } = new Users();



        //static lists to retrieve all existing notes
        private static List<Users> allUsers = Connector.LoadUsers();
        //retreives list without ability to tamper with contents
        public static IList<Users> AllUsers
        {
            get { return allUsers.AsReadOnly(); }
        }
        //constructor for retreiving user objects
        public Users(string email, string password, string name)
        {
            this.email = email;
            this.password = password;
            this.name = name;
     
        }
        public Users()
        {
            
        }


       


    }
}
