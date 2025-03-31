namespace ReleaseNotesProject
{
    public class SQLApps
    {

        public string appName { get; private set; }
        public string appId { get; private set; }

        //static lists to retrieve all existing apps
        private static List<SQLApps> allApps = Connector.LoadApps();
        //retrieves list without ability to tamper with contents
        public static IList<SQLApps> AllApps
        {
            get { return allApps.AsReadOnly(); }
        }



        public SQLApps(string appId, string appName)
        {

            this.appId = appId;
            this.appName = appName;

        }

        public SQLApps(string appName)
        {

            this.appName = appName;

        }

        public SQLApps()
        {


        }

    }
}
