
using System.DirectoryServices;


namespace ITAssetsDatabase.HtmlHelpers
{

    public static class LDAPHelper
    {
        public static SearchResultCollection SearchUser(string searchstring)
        {            
            string path = "LDAP://ldap.forumsys.com/dc=example,dc=com";
                       
            //var username = "cn=read-only-admin,dc=example,dc=com";
            var username = "uid=boyle,dc=example,dc=com";
            var password = "password";
         
           DirectoryEntry rootEntry = new  DirectoryEntry(path, username, password, AuthenticationTypes.None);

            
            DirectorySearcher search = new DirectorySearcher(rootEntry)
            {
                Filter = "(&" + "(objectClass=inetOrgPerson)" + "(cn=*" + searchstring + "*" + "))"
                            };

            var searchlist = search.FindAll();
            
  

           return searchlist;
        

        }
    }
}


        //public static bool checkinAD(string searchstring)
        //{
        //    ITAssetsDatabaseDBContext db = new ITAssetsDatabaseDBContext();

        //    //Extract logon details from DB
        //    // 2 is UK1 ReadOnly

        //    var query = db.ADLogons.Find(2);

        //    var UK1ReadyOnlyUserID = (query.UserID).Trim();
        //    var UK1Password = (query.Password).Trim();

        //    // Creating the PrincipalContext 

        //    #region Variables

        //    string Domain = "uk1.group.internal";
        //    string Container = "DC=uk1,DC=group,DC=internal";
        //    //string sDefaultRootOU = "DC=ad,DC=xafinity,DC=com";
        //    string ServiceUser = @"uk1\" + UK1ReadyOnlyUserID;
        //    string ServicePassword = UK1Password;

        //    #endregion


        //    PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, Domain, Container,
        //                                                          ContextOptions.SimpleBind, ServiceUser, ServicePassword);

        //    //Create a "user object" in the context 
        //    ComputerPrincipal device = new ComputerPrincipal(domainContext);

        //    //Specify the search parameters 
        //    device.Name = searchstring + '*';


        //    //Create the searcher 
        //    //pass (our) user object 

        //    PrincipalSearcher pS = new PrincipalSearcher();
        //    pS.QueryFilter = device;

        //    //Perform the search 
        //    PrincipalSearchResult<Principal> results = pS.FindAll();

        //    var count = results.Count();


        //    bool retVal1 = false;

        //    if (count != 0)
        //    { retVal1 = true; }


        //    return retVal1;

        //}

        //public static bool checkinADXafinity(string searchstring)
        //{
        //    ITAssetsDatabaseDBContext db = new ITAssetsDatabaseDBContext();

        //    //Extract logon details from DB
        //    // 1 is Xafinity ReadOnly 

        //    var query = db.ADLogons.Find(1);

        //    var XafinityReadyOnlyUserID = (query.UserID).Trim();
        //    var XafinityPassword = (query.Password).Trim();

        //    // Creating the PrincipalContext 

        //    #region Variables

        //    string Domain = "ad.xafinity.com";
        //    string Container = "DC=ad,DC=xafinity,DC=com";
        //    string ServiceUser = @"Xafinity\" + XafinityReadyOnlyUserID;
        //    string ServicePassword = XafinityPassword;

        //    #endregion


        //    PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, Domain, Container,
        //                                                          ContextOptions.SimpleBind, ServiceUser, ServicePassword);

        //    //Create a "user object" in the context 
        //    ComputerPrincipal device = new ComputerPrincipal(domainContext);

        //    //Specify the search parameters 
        //    device.Name = searchstring + '*';


        //    //Create the searcher 
        //    //pass (our) user object 

        //    PrincipalSearcher pS = new PrincipalSearcher();
        //    pS.QueryFilter = device;

        //    //Perform the search 
        //    PrincipalSearchResult<Principal> results = pS.FindAll();

        //    var count = results.Count();


        //    bool retVal1 = false;

        //    if (count != 0)
        //    { retVal1 = true; }


        //    return retVal1;

        //}

    

     