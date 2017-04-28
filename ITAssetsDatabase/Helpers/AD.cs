using System.Collections.Generic;
using System.Linq;
using System.DirectoryServices.AccountManagement;
using ITAssetsDatabase.Models;


namespace ITAssetsDatabase.HtmlHelpers
{

    public static class AD
    {
             

        public static IEnumerable<UserPrincipal> SearchUser(string searchstring, int DomainId)
        {
            ITAssetsDatabaseDBContext db = new ITAssetsDatabaseDBContext();

            string Domain =null;
            string Container =null;
            string ServiceUser = null;
            string ServicePassword = null;


            if (DomainId == 1)

            { 
                // Setup PLUMDOM1 Connections

                var query = db.ADLogons.Find(1);

                var ReadyOnlyUserID = (query.UserID).Trim();
                var Password = (query.Password).Trim();

                // Creating the PrincipalContext

                #region Variables

                Domain = "PLUMDOM1";
                Container = "OU=Users,OU=Plumdom,DC=ad,DC=plumdom1,DC=com";
                //string sDefaultRootOU = "DC=ad,DC=plumdom,DC=com";
                ServiceUser = @"plumdom1\" + ReadyOnlyUserID;
                //string ServicePassword = "London98";
                ServicePassword = Password;

                #endregion
            
            }

            else if (DomainId == 2)
            {

                // Setup PLUMDOM2 Connections

                var query = db.ADLogons.Find(2);

                var ReadyOnlyUserID = (query.UserID).Trim();
                var Password = (query.Password).Trim();
                 
                // Creating the PrincipalContext

                #region Variables

                Domain = "plumdom2.com";
                Container = "OU=People,DC=plumdom2,DC=group,DC=internal";
                //string sDefaultRootOU = "DC=ad,DC=plumdom2,DC=com";
                ServiceUser = @"plumdom2\" + ReadyOnlyUserID;
                //string ServicePassword = "London98";
                ServicePassword = Password;

                #endregion

            }

            
            
            PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, Domain, Container,
                                                                  ContextOptions.SimpleBind, ServiceUser, ServicePassword );

            //Create a "user object" in the context 
            UserPrincipal user = new UserPrincipal(domainContext);

            //Specify the search parameters 
            user.Surname = searchstring + '*';

            //Create the searcher 
            //pass (our) user object 

            PrincipalSearcher pS = new PrincipalSearcher();
            pS.QueryFilter = user;

            //Perform the search 
            PrincipalSearchResult<Principal> results = pS.FindAll();

            List<UserPrincipal> retVal = null;

            //retVal = (from principal in results select principal as UserPrincipal).ToList();

            retVal = (from principal in results
                      select principal as UserPrincipal).ToList();

            return retVal;

        }


        public static bool checkinAD(string searchstring)
        {
            ITAssetsDatabaseDBContext db = new ITAssetsDatabaseDBContext();

            //Extract logon details from DB
            // 2 is UK1 ReadOnly

            var query = db.ADLogons.Find(2);

            var ReadyOnlyUserID = (query.UserID).Trim();
            var Password = (query.Password).Trim();

            // Creating the PrincipalContext 

            #region Variables

            string Domain = "plumdom2.com";
            string Container = "OU=People,DC=plumdom2,DC=group,DC=internal";
            //string sDefaultRootOU = "DC=ad,DC=plumdom2,DC=com";
            string ServiceUser = @"plumdom2\" + ReadyOnlyUserID;
            //string ServicePassword = "London98";
            string ServicePassword = Password;


            #endregion


            PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, Domain, Container,
                                                                  ContextOptions.SimpleBind, ServiceUser, ServicePassword);
                 
            //Create a "user object" in the context 
            ComputerPrincipal device = new ComputerPrincipal(domainContext);

            //Specify the search parameters 
            device.Name = searchstring + '*';
         

            //Create the searcher 
            //pass (our) user object 

            PrincipalSearcher pS = new PrincipalSearcher();
            pS.QueryFilter = device;

            //Perform the search 
            PrincipalSearchResult<Principal> results = pS.FindAll();

            var count = results.Count();


            bool retVal1 = false;

            if (count != 0 )
            { retVal1 = true; }
            

            return retVal1;

        }

          
    }

}

