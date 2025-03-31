using System.Globalization;

namespace ReleaseNotesProject
{
    public class ReleaseNote
    {

        //set to be able to retreived freely but not written to
        public string guid { get; private set; }
        public string appName { get; private set; }
        public string releaseNote { get; private set; }
        public string createdBy { get; private set; }
        public string createdDate { get; private set; }


        //datetime and guid objects for new note creation
        private static Guid newGuid { get; set; }
        private static DateTime now { get; set; }


        //static lists to retrieve all existing notes
        private static List<ReleaseNote> allNotes = Connector.LoadNotes();
        //retreives list without ability to tamper with contents
        public static IList<ReleaseNote> AllNotes
        {
            
            get { return allNotes.AsReadOnly(); }
        }




        //constructor for new note creation
        public ReleaseNote(string appName, string releaseNote, string createdBy){
            this.appName = appName;
            this.releaseNote = releaseNote;
            this.createdBy = createdBy;
            //generate new Guid on creation
            guid = Guid.NewGuid().ToString();
            //get current date on creation
            now=DateTime.Now;
            createdDate = now.ToString();
            }

        //constructor for existent note reading

        public ReleaseNote(string guid, string appName, string releaseNote, string createdBy, string createdDate)
        {
            this.guid = guid;
            this.appName = appName;
            this.releaseNote = releaseNote;
            this.createdBy = createdBy;
            this.createdDate = createdDate;
            
        }

        public ReleaseNote() { 
        }






    }
}
