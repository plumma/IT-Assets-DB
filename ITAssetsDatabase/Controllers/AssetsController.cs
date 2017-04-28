using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITAssetsDatabase.Models;
using ITAssetsDatabase.HtmlHelpers;
using ITAssetsDatabase.Models.Assets;
using System.IO;   // Allows Path.Combine(path, fileName); to work


using ITAssetsDatabase.Helpers;


using System.DirectoryServices;                     // Active directory chats 
using System.DirectoryServices.AccountManagement;  // Active directory chats 


namespace ITAssetsDatabase.Controllers
{
    public class AssetsController : Controller
    {
        private ITAssetsDatabaseDBContext db = new ITAssetsDatabaseDBContext();

        string CurrentlyLoggedin = System.Web.HttpContext.Current.User.Identity.Name.ToLower();

        public ActionResult Index()
        {

            var model = new List<AssetsViewModel>();

            var query = db.Assets.Include(x => x.AssetAssigned).ToList();


            string EndUserName = null;
            string HelpdeskRef = null;


            foreach (var Assets in query)
            {
               
                var assetquery = Assets.AssetAssigned.OrderByDescending(d => d.CreateDate).FirstOrDefault();
                var Status = assetquery.AssetStatus.Id;
                var StatusLabel = assetquery.AssetStatus.State;


                if (Status == 1 || Status == 2)   // NEW or ASSIGNED
                {
                    EndUserName = assetquery.AssetAssignee.Assignee.FirstName;
                    HelpdeskRef = assetquery.AssetAssignee.HelpdeskRef;
                }
                else if (Status == 3)  // IN STOCK
                {
                    EndUserName = "NOT ASSIGNED";
                    HelpdeskRef = "n/a";
                }

                else if (Status == 4)  // FAULTY
                {
                    EndUserName = "IN REPAIR";
                    HelpdeskRef = "n/a";
                }
                else if (Status == 5)  // RETIRED
                {
                    EndUserName = "n/a";
                    HelpdeskRef = "n/a";
                }

                var viewModel = new AssetsViewModel

                {
                    Id = Assets.Id,
                    CreateDate = Assets.CreateDate,
                    Type = Assets.Device.Type,
                    Make = Assets.Device.Make,
                    Model = Assets.Device.Model,
                    Status = StatusLabel,
                    EndUserName = EndUserName,
                    HelpdeskRef = HelpdeskRef
                };

                model.Add(viewModel);

            }

            return View(model);
        }


        public ActionResult Register()
        {

            var model = new RegisterAssetViewModel();

            // This snippet of code was originally used within a domain infrastructure environment and would look at who was logged on and update a new asset record using their Staff ID
            // for the purposes of placing the asset register in a public setting I will use one guest id to register asset additions
            //model.StaffId = db.Staff.Select(o => new { StaffFullName = o.Domain + @"\" + o.DomainLogon, o.Id }).Where(r => r.StaffFullName == CurrentlyLoggedin).Select(t => t.Id).First();

            // StaffID of 1 is set as a guest Id
            model.StaffId = 1;

            model.Domain = new SelectList(db.Domains, "Id", "DomainName");
            
              
            return View(model); 
        }

      

        [HttpPost]
        public ActionResult Register(RegisterAssetViewModel Asset)
        {

            // Add End User to AssetAssignee entity

            var newAsset = new Asset();

            newAsset.PRRef = Asset.PRRef;
            newAsset.PORef = Asset.PORef;
            newAsset.AssetNo = Asset.AssetNo;
            newAsset.SerialNo = Asset.SerialNo;
            newAsset.MAC_Address = Asset.MAC_Address;
            newAsset.DomainNameId = Asset.DomainId;
            newAsset.HostnameId = Asset.HostnameId;            
            newAsset.BuildId = Asset.BuildId;
            newAsset.DeviceId = Asset.DeviceId;
            newAsset.StaffId = Asset.StaffId;

            db.Assets.Add(newAsset);
            db.SaveChanges();

            // Update AssetAssignee entity

            var newAssetAssignee = new AssetAssignee();

            newAssetAssignee.HelpdeskRef = Asset.HelpdeskRef;


            // Update Assignee Details

            // Check if user already in Database

            var SIDLookup = db.Employees.Where(e => e.SID == Asset.AssigneeSID).Select(u => u.Id).FirstOrDefault();

            if (SIDLookup != 0)
            {
                newAssetAssignee.AssigneeId = SIDLookup;
            }
            else
            {

                // Update Employee entity with newly aquired SID details

                var employee = new Employee();

                employee.FirstName = Asset.AssigneeFirstName;
                employee.MiddleName = Asset.AssigneeMiddleName;
                employee.SecondName = Asset.AssigneeSurname;
                employee.Email = Asset.AssigneeEmail;
                employee.Domain = Asset.AssigneeDomain;
                employee.DomainLogon = Asset.AssigneeDomainLogon;
                employee.SID = Asset.AssigneeSID;
                db.Employees.Add(employee);
                db.SaveChanges();

                newAssetAssignee.AssigneeId = employee.Id;

            }
            // Update Requester Details

            SIDLookup = db.Employees.Where(e => e.SID == Asset.RequesterSID).Select(u => u.Id).FirstOrDefault();

            if (SIDLookup != 0)
            {
                newAssetAssignee.RequesterId = SIDLookup;
            }
            else
            {
                // Update Employee entity with newly aquired SID details

                var employee = new Employee();

                employee.FirstName = Asset.RequesterFirstName;
                employee.MiddleName = Asset.RequesterMiddleName;
                employee.SecondName = Asset.RequesterSurname;
                employee.Email = Asset.RequesterEmail;
                employee.Domain = Asset.RequesterDomain;
                employee.DomainLogon = Asset.RequesterDomainLogon;
                employee.SID = Asset.RequesterSID;
                db.Employees.Add(employee);
                db.SaveChanges();
                newAssetAssignee.RequesterId = employee.Id;

            }


            db.AssetAssignees.Add(newAssetAssignee);
            db.SaveChanges();




            // Update AssetAssigned entity

            var newAssetAssigned = new AssetAssigned();

            newAssetAssigned.AssetStatusID = 1;   // To represent NEW status
            newAssetAssigned.AssetId = newAsset.Id;  // Adds newly created ID from newAsset record to AssetAssigned entity
            newAssetAssigned.LocationId = Asset.LocationId;
            newAssetAssigned.StaffId = Asset.StaffId;
            newAssetAssigned.Notes = Asset.Notes;
            newAssetAssigned.AssetAssigneeID = newAssetAssignee.Id;
            db.AssetAssigned.Add(newAssetAssigned);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var model = db.Assets.Find(id);


            ViewBag.Hostname = model.Hostname.FullHostname;        


            ViewBag.status = model.AssetAssigned.Where(x => x.AssetId == id).OrderByDescending(x => x.CreateDate).Select(x => x.AssetStatus.State).First();

            var newmodel = new DetailsViewModel();

            newmodel.Asset = model;

           var dbrecordset = db.AssetAssigned.Where(x => x.AssetId == id);
            
           var ListOfObjects = new List<AuditClass>();
            
            foreach (var Item in dbrecordset)
            {

                var eachAudit = new AuditClass();
                {
                    eachAudit.CreatedDate = Item.CreateDate;

                    if (Item.AssetAssigneeID != null)
                    {
                        eachAudit.HelpdeskRef = Item.AssetAssignee.HelpdeskRef;
                        eachAudit.RequesterFullName = Item.AssetAssignee.Requester.FirstName;
                        eachAudit.EndUserFullName = Item.AssetAssignee.Assignee.FirstName;
                        eachAudit.SignOffSheetFileName = Item.AssetAssignee.SignOffSheetFileName;
                    }
                    else
                    {
                        eachAudit.HelpdeskRef = "n/a";
                        eachAudit.RequesterFullName = "n/a";
                        eachAudit.EndUserFullName = "n/a";
                        eachAudit.SignOffSheetFileName = "";
                    }

                    
                    eachAudit.AssetStatus = Item.AssetStatus.State;
                    eachAudit.Notes = Item.Notes;
                    eachAudit.Staff = Item.Staff.SecondName;

                    ListOfObjects.Add(eachAudit);
       
                }
                
            }

            newmodel.Audit = ListOfObjects;


            return View(newmodel);

        }



         public JsonResult ConfirmDelete(int id)
        {
            // Remove records from AssetAssigned table to clean up database
            var assetassignedtoremove = db.AssetAssigned.Where(a => a.AssetId == id).ToList();
            foreach (var record in assetassignedtoremove)
                db.AssetAssigned.Remove(record);
            db.SaveChanges();

            // Remove main asset record.

            var asset = db.Assets.Find(id);

            db.Assets.Remove(asset);
            db.SaveChanges();
            
            return null;

        }



        //public PartialViewResult SignoffSheetUpload(int AssetId)

        //{

        //    var model = new SignoffSheetUploadViewModel();

        //    //var query1 = db.AssetAssigned.Where(x => x.AssetId == id).Where(s => s.AssetStatusID == 2).Select(y => new { Id = y.AssetAssignee.Assignee.Id, Fullname = y.AssetAssignee.Assignee.SecondName + ", " + y.AssetAssignee.Assignee.FirstName});
           
        //    var newestassignedenduser = db.AssetAssigned.Where(x => x.AssetId == AssetId && x.AssetStatusID == 2)
        //                   .OrderByDescending(s => s.CreateDate).FirstOrDefault();

        //    model.EmployeeID = newestassignedenduser.AssetAssignee.Assignee.Id;
        //    model.EmployeeFullName = newestassignedenduser.AssetAssignee.Assignee.FirstName;
        //    model.AssetId = AssetId;
        //    model.Name = "";


        //    return this.PartialView(model);
        //}


        

        //// No longer required
        //public PartialViewResult PrintSignoff(int id)
        //{

        //    var model = new SignoffSheetUploadViewModel();

        //    //var query1 = db.AssetAssigned.Where(x => x.AssetId == id).Where(s => s.AssetStatusID == 2).Select(y => new { Id = y.AssetAssignee.Assignee.Id, Fullname = y.AssetAssignee.Assignee.SecondName + ", " + y.AssetAssignee.Assignee.FirstName });
        //    var query1 = db.AssetAssigned.Where(x => x.AssetId == id).Where(s => s.AssetStatusID == 2).Select(y => new { Id = y.AssetAssignee.Assignee.Id, Fullname = y.AssetAssignee.Assignee.FirstName });


        //    model.Endusers = new SelectList(query1, "Id", "Fullname");
        //    model.AssetId = id;
        //    model.Name = "";


        //    return this.PartialView(model);
        //}


        public ActionResult PrintSignoff_display(int AssetId)
        {

            var model = new PrintSignoff();

            // Find newest assigned end user

            //var newestassignedenduser = db.AssetAssigned.Where(x => x.AssetId == AssetId && x.AssetStatusID == 2)
            //                .OrderByDescending(s => s.CreateDate)
            //                .Select(y => new { Fullname = y.AssetAssignee.Assignee.FirstName + " " + y.AssetAssignee.Assignee.SecondName }).FirstOrDefault();

            var newestassignedenduser = db.AssetAssigned.Where(x => x.AssetId == AssetId && x.AssetStatusID == 2)
                           .OrderByDescending(s => s.CreateDate).FirstOrDefault();

            model.EndUser = newestassignedenduser.AssetAssignee.Assignee.FirstName;
            model.Email = newestassignedenduser.AssetAssignee.Assignee.Email;
            model.Number = newestassignedenduser.AssetAssignee.Assignee.PhoneNum;
            model.UserDomain = newestassignedenduser.AssetAssignee.Assignee.Domain;

            var query = db.Assets.Find(AssetId);

            model.HelpdeskRef = newestassignedenduser.AssetAssignee.HelpdeskRef;
            model.PORef = query.PORef;
            model.PRRef = query.PRRef;

            model.Type = query.Device.Type;
            model.Make = query.Device.Make;
            model.Model = query.Device.Model;
            model.SerialNo = query.SerialNo;
            model.Asset = query.AssetNo;
            model.MACAddress = query.MAC_Address;
           
            model.Hostname = query.Hostname.FullHostname;
            model.Build = query.Build.BuildName;
            model.DeviceDomain = query.DomainName.DomainName;

            var staffid = 1;

            //var staffid = db.Staff.Select(o => new { StaffFullName = o.Domain + @"\" + o.DomainLogon, o.Id }).Where(r => r.StaffFullName == CurrentlyLoggedin).Select(t => t.Id).First();

            model.Engineer = db.Staff.Find(staffid).FullName;
                         
            return View(model);
        
        }
   

        public FilePathResult GetFileFromDisk(int id)
        { 
            string filename = db.AssetSignoffs.Where(x => x.ID == id).Select(x => x.Uploadpath).FirstOrDefault();

            string path = AppDomain.CurrentDomain.BaseDirectory + "SignOffSheets/";

            return File(path + filename, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", filename);
        }



        public ActionResult Edit(int id)
        {

            var query = db.Assets.Find(id);

            string hostname = "";


            hostname = query.Hostname.FullHostname;
         
            int status = 0;            
            
            if (query != null)
                        { 
                            status = db.AssetAssigned.Where( x => x.AssetId == id).OrderByDescending( x => x.CreateDate).Select( x => x.AssetStatus.Id).First();       
                            }

            if (status == 1)  // Status NEW

            {                                
                return View("Edit_NEW", query);            
            }

            var model = new AssetActionViewModel();
            model.Assets = query;
            model.Hostname = hostname;
            

            if (status == 2)   // Status ASSIGNED
            {   
                model.State = "ASSIGNED";                
                model.AssetActions = new SelectList(db.AssetActions, "ID", "Action", 1);
                return View("Action", model);

            }

            if (status == 3)  // Status IN STOCK
            {   
                model.State = "IN STOCK";
                // Remove the Return to Stock Option
                var ammendedlist = db.AssetActions.Where(x => x.ID != 1);
                model.AssetActions = new SelectList(ammendedlist, "ID", "Action", 2);                

                return View("Action", model);
            }

            if (status == 4)  // Status FAULTY
            {
                
                model.State = "FAULTY";

                // Remove the DEVICE IN REPAIR option
                var ammendedlist = db.AssetActions.Where(x => x.ID != 4);

                model.AssetActions = new SelectList(ammendedlist, "ID", "Action", 2);
               
                return View("Action", model);
            }

            if (status == 5)  // Status RETIRED
            {
                
                model.State = "RETIRED";

                // Remove the DEVICE IN REPAIR option
                var ammendedlist = db.AssetActions.Where(x => x.ID != 3);

                model.AssetActions = new SelectList(ammendedlist, "ID", "Action", 2);                
                return View("Action", model);
            }





            return View(query);
        }

        
        [HttpPost]
        public ActionResult Edit_NEW(int id, int AssetAssigneeId) 
        {
            //db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            //var StaffId = db.Staff.Select(o => new { StaffFullName = o.Domain + @"\" + o.DomainLogon, o.Id }).Where(r => r.StaffFullName == CurrentlyLoggedin).Select(t => t.Id).First();
            var StaffId = 1;
                      

            var model = new AssetAssigned();

            model.StaffId = StaffId;
            model.AssetStatusID = 2;
            model.AssetAssigneeID = AssetAssigneeId;
            model.AssetId = id;

            db.AssetAssigned.Add(model);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Action(string submitbuttonflag,int id, int Action, List<HttpPostedFileBase> fileupload_object) 
        {

            if (submitbuttonflag == "action")
            {

                // For the purposes of this public website I've set the Staff ID to 1 which represents myself only
                var StaffId = 1;
                // db.Staff.Select(o => new { StaffFullName = o.Domain + @"\" + o.DomainLogon, o.Id }).Where(r => r.StaffFullName == CurrentlyLoggedin).Select(t => t.Id).First();


                var model = new AssetAssigned();
                model.StaffId = StaffId;
                model.AssetId = id;
                model.AssetAssigneeID = null;

                if (Action == 1)  // Return to stock

                {
                    model.AssetStatusID = 3;
                }

                if (Action == 2)  // Re-deploy to different user
                {
                    return RedirectToAction("ReDeploy", new { id = id });

                }

                if (Action == 3)  // Re-tire the device
                {
                    model.AssetStatusID = 5;
                }

                if (Action == 4)  // Mark device as in repair
                {
                    model.AssetStatusID = 4;
                }


                db.AssetAssigned.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            else if (submitbuttonflag == "upload") {



                // Determine Latest User.

                var newestassignedenduser = db.AssetAssigned.Where(x => x.AssetId == id && x.AssetStatusID == 2).OrderByDescending(s => s.CreateDate).FirstOrDefault();

                var EmployeeID = newestassignedenduser.AssetAssignee.Assignee.Id;
                var EmployeeFullName = newestassignedenduser.AssetAssignee.Assignee.FirstName;



                string newfilename = "";


                //   Save attached files to disk

                if (fileupload_object.Count > 0)
                {
                    foreach (HttpPostedFileBase file in fileupload_object)
                    {
                        string pathFile = string.Empty;
                        if (file != null)
                        {
                            string serverFilepath = string.Empty;
                            string fileName = string.Empty;
                            string fullPath = string.Empty;

                            serverFilepath = AppDomain.CurrentDomain.BaseDirectory + "SignOffSheets"; //here give the directory where you want to save your file

                            if (!System.IO.Directory.Exists(serverFilepath))  //if path do not exit
                            {
                                System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "SignOffSheets");//if given directory dont exist, it creates with give directory name
                            }


                            string date = DateTime.Now.ToString("dd-MM-yy");

                            fileName = Path.GetFileName(file.FileName);   // IE Fix to ensure path is stripped from uploaded file

                            //string name = Path.GetFileNameWithoutExtension(fileName);

                            string[] name = EmployeeFullName.Split(' ');


                            string extension = Path.GetExtension(fileName);

                            newfilename = name[1].Trim() + "_" + name[0].Trim() + "_" + date + extension;

                            fullPath = Path.Combine(serverFilepath, newfilename);

                            if (!System.IO.File.Exists(fullPath))
                            {

                                if (fileName != null && fileName.Trim().Length > 0)
                                {
                                    file.SaveAs(fullPath);
                                }
                            }

                            var model = new AssetSignoff();

                            model.AssetId = id;
                            model.EmployeeId = EmployeeID;
                            model.Uploadpath = newfilename;

                            db.AssetSignoffs.Add(model);
                            db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Edit", new { id = id });


            }

            return null;
        }


        
        //public ActionResult SignOffSheetsUpload(int AssetId)
        //{

        //    // Determine Latest User.

        //    var newestassignedenduser = db.AssetAssigned.Where(x => x.AssetId == AssetId && x.AssetStatusID == 2).OrderByDescending(s => s.CreateDate).FirstOrDefault();

        //    var EmployeeID = newestassignedenduser.AssetAssignee.Assignee.Id;
        //    var EmployeeFullName = newestassignedenduser.AssetAssignee.Assignee.FirstName;

        

        //    string newfilename = "";


        //    //   Save attached files to disk

        //    if (fileupload_object.Count > 0)
        //    {
        //        foreach (HttpPostedFileBase file in fileupload_object)
        //        {
        //            string pathFile = string.Empty;
        //            if (file != null)
        //            {
        //                string serverFilepath = string.Empty;
        //                string fileName = string.Empty;
        //                string fullPath = string.Empty;

        //                serverFilepath = AppDomain.CurrentDomain.BaseDirectory + "SignOffSheets"; //here give the directory where you want to save your file

        //                if (!System.IO.Directory.Exists(serverFilepath))  //if path do not exit
        //                {
        //                    System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "SignOffSheets");//if given directory dont exist, it creates with give directory name
        //                }


        //                string date = DateTime.Now.ToString("dd-MM-yy");

        //                fileName = Path.GetFileName(file.FileName);   // IE Fix to ensure path is stripped from uploaded file

        //                //string name = Path.GetFileNameWithoutExtension(fileName);

        //                string[] name = EmployeeFullName.Split(' ');


        //                string extension = Path.GetExtension(fileName);

        //                newfilename = name[1].Trim() + "_" + name[0].Trim() + "_" + date + extension;

        //                fullPath = Path.Combine(serverFilepath, newfilename);

        //                if (!System.IO.File.Exists(fullPath))
        //                {

        //                    if (fileName != null && fileName.Trim().Length > 0)
        //                    {
        //                        file.SaveAs(fullPath);
        //                    }
        //                }

        //                var model = new AssetSignoff();

        //                model.AssetId = AssetId;
        //                model.EmployeeId = EmployeeID;
        //                model.Uploadpath = newfilename;

        //                db.AssetSignoffs.Add(model);
        //                db.SaveChanges();
        //            }
        //        }
        //    }

        //    return RedirectToAction("Edit", new { id = AssetId });
        //}



        public ActionResult ReDeploy( int? id)

        {

            var model = db.Assets.Find(id);

            var status = model.AssetAssigned.Where(x => x.AssetId == id).OrderByDescending(x => x.CreateDate).First();


            // var StaffId = db.Staff.Select(o => new { StaffFullName = o.Domain + @"\" + o.DomainLogon, o.Id }).Where(r => r.StaffFullName == CurrentlyLoggedin).Select(t => t.Id).First();
            var StaffId = 1;
            
            var ReDeployViewModel = new ReDeployViewModel();

            ReDeployViewModel.id = model.Id;

            ReDeployViewModel.StaffId = StaffId;

            ReDeployViewModel.AssetStatus = status.AssetStatus.State;

            ReDeployViewModel.DeviceDetails = model.Device.Type + "-> " + model.Device.Make + "-> " + model.Device.Model;

            ReDeployViewModel.DeviceId = model.DeviceId;

            ReDeployViewModel.ComputerDomain = model.DomainName.DomainName;

            ReDeployViewModel.Hostname = model.Hostname.FullHostname;  
          

            if (status.AssetAssigneeID != null)
            {

                ReDeployViewModel.AssignedTo = status.AssetAssignee.Requester.FullName;

            }

            else {

                ReDeployViewModel.AssignedTo = "Currently Not Assigned";
            }

            ReDeployViewModel.RedeployCheckBox = true;
            ReDeployViewModel.Domain = new SelectList(db.Domains, "Id", "DomainName");

            
            return View(ReDeployViewModel);

        }


        [HttpPost]
        public ActionResult ReDeploy(ReDeployViewModel model)
        {
            if (model.RedeployCheckBox == true)        // Redeploy asset to new user
            {
                
                // Update AssetAssignee entity

                var newAssetAssignee = new AssetAssignee();

                newAssetAssignee.HelpdeskRef = model.HelpdeskRef;


                // Update Assignee Details

                var SIDLookup = db.Employees.Where(e => e.SID == model.AssigneeSID).Select(u => u.Id).FirstOrDefault();

                if (SIDLookup != 0)
                {
                    newAssetAssignee.AssigneeId = SIDLookup;
                }
                else
                {

                    // Update Employee entity with newly aquired SID details

                    var employee = new Employee();

                    employee.FirstName = model.AssigneeFirstName;
                    employee.MiddleName = model.AssigneeMiddleName;
                    employee.SecondName = model.AssigneeSurname;
                    employee.Email = model.AssigneeEmail;
                    employee.Domain = model.AssigneeDomain;
                    employee.DomainLogon = model.AssigneeDomainLogon;
                    employee.SID = model.AssigneeSID;
                    db.Employees.Add(employee);
                    db.SaveChanges();

                    newAssetAssignee.AssigneeId = employee.Id;

                }
                // Update Requester Details

                SIDLookup = db.Employees.Where(e => e.SID == model.RequesterSID).Select(u => u.Id).FirstOrDefault();

                if (SIDLookup != 0)
                {
                    newAssetAssignee.RequesterId = SIDLookup;
                }
                else
                {
                    // Update Employee entity with newly aquired SID details

                    var employee = new Employee();

                    employee.FirstName = model.RequesterFirstName;
                    employee.MiddleName = model.RequesterMiddleName;
                    employee.SecondName = model.RequesterSurname;
                    employee.Email = model.RequesterEmail;
                    employee.Domain = model.RequesterDomain;
                    employee.DomainLogon = model.RequesterDomainLogon;
                    employee.SID = model.RequesterSID;
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    newAssetAssignee.RequesterId = employee.Id;

                }


                db.AssetAssignees.Add(newAssetAssignee);
                db.SaveChanges();
                

                // Update AssetAssigned entity

                var newAssetAssigned = new AssetAssigned();

                newAssetAssigned.AssetStatusID = 2 ;   // Assigned to user
                newAssetAssigned.AssetId = model.id;  // Adds newly created ID from newAsset record to AssetAssigned entity
                newAssetAssigned.AssetAssigneeID = newAssetAssignee.Id;
                //newAssetAssigned.LocationId = model.LocationId;
                newAssetAssigned.StaffId = model.StaffId;
                newAssetAssigned.Notes = model.Notes;
                newAssetAssigned.AssetAssigneeID = newAssetAssignee.Id;
                db.AssetAssigned.Add(newAssetAssigned);
                db.SaveChanges();
            }


            if (model.ChangeBuildHostnameCheckBox == true)        // Redeploy asset to new user
            {

                // Update Asset with new hostname/build

                var assetquery = db.Assets.Find(model.id);

                assetquery.HostnameId = model.HostnameId;
                assetquery.DomainNameId = 1;
          
                assetquery.BuildId = model.BuildId;
               
                db.SaveChanges();
                    
            }


            return RedirectToAction("Index");

        }


        // Lookup Person using AD which uses a helper I created called AD.SearchUser

        //public JsonResult LookupPersonAutocomplete(string term, int DomainId)
       
        //{
        //    if (term != string.Empty)
        //    {

        //        var listmodel = new List<EndUserViewModel>();
                  

        //       IEnumerable<UserPrincipal> ADUsers = AD.SearchUser(term, DomainId);
             

        //        foreach (UserPrincipal item in ADUsers)

        //        {
        //            var model = new EndUserViewModel();
              
        //            var domain = (item.Context.Name).ToLower();

        //            if (domain == "uk1.group.internal")

        //            { domain = "UK1"; };

        //            var logon = item.UserPrincipalName;


        //            if (logon != null)
        //            {
        //                logon = logon.Remove(logon.IndexOf("@"));
        //            }
                    
        //            model.label = item.DisplayName + " (" + domain + @"\" + logon + ")";                    
        //            model.value = item.Sid.Value;

        //            model.FirstName = item.GivenName;
        //            model.MiddleName = item.MiddleName;
        //            model.Surname = item.Surname;
        //            model.Email = item.EmailAddress;
        //            model.Domain = domain;
        //            model.Logon = logon;

        //            listmodel.Add(model);

        //        }

        //        return Json(listmodel, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return null;
        //    }
           
        //}






        // Lookup Person using LDAP

        public JsonResult LookupPersonAutocomplete(string term, int DomainId)

        {
            if (term != string.Empty)
            {
                
                var search = LDAPHelper.SearchUser(term);

                var listmodel = new List<EndUserViewModel>();

                foreach (SearchResult result in search)
                {
                    var model = new EndUserViewModel();

                    DirectoryEntry user = result.GetDirectoryEntry();
                    if (user != null && user.Name.Contains("uid"))
                    {

                        model.value = Convert.ToString(user.Properties["uid"].Value);
                        model.label = user.Properties["cn"].Value.ToString();
                        model.FirstName = user.Properties["cn"].Value.ToString();
                        model.Surname = user.Properties["sn"].Value.ToString();
                        model.Logon = user.Properties["uid"].Value.ToString();
                        model.Email = Convert.ToString(user.Properties["mail"].Value);
                        model.Domain = user.Path;

                        listmodel.Add(model);

                    }
                }


                return Json(listmodel, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }

        //  LookupLocation

        public JsonResult LookupLocationAutocomplete(string term)
        {
            if (term != string.Empty)
            {

                var locations = db.Locations.Where(l => l.BuildingName.Contains(term) || l.Town.Contains(term)).Select(o => new { value = o.Id, label = o.BuildingName + ", " + o.Town }).ToList();

                                
                return Json(locations, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }


        //  Lookup Device Model

        public JsonResult LookupDeviceModelAutocomplete(string term)
        {
            if (term != string.Empty)
            {
             
                var devices = db.Devices.Where(l => l.Make.Contains(term) || l.Model.Contains(term) || l.Type.Contains(term)).Select(o => new { value = o.Id, label = o.Type + "->" + o.Make + "->" + o.Model}).ToList();

                return Json(devices, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }


        //  Lookup Build

        public JsonResult LookupBuildAutocomplete(string term, int filter)
        {
            if (term != string.Empty)
            {

                var builds = db.Builds.Where(l => l.BuildName.Contains(term) || l.Domain.Contains(term)).Select(o => new { value = o.Id, label = o.Domain + "->" + o.BuildName}).ToList();
                 
                return Json(builds, JsonRequestBehavior.AllowGet);
                         
            } 
            else
            {
                return null;
            }

        }



        //  Fetch Hostname

        public JsonResult FetchHostname(int LocationId, int DeviceId, int StaffId, int BuildId)
        {

            var Domain = db.Builds.Find(BuildId).Domain;
            var Type = db.Devices.Find(DeviceId).Type;

            var Hostname = FetchHostnameHelper2.Fetch(LocationId, Type, StaffId, Domain);

            return Json(Hostname, JsonRequestBehavior.AllowGet);

        }
        
    }
}

    