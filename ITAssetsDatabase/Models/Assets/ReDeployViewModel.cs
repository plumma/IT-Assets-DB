using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;    // SelectList
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models.Assets
{
    public class ReDeployViewModel
    {

        // Display Details of existing device

        public int id { get; set; }
        public string AssetStatus { get; set; }
        public string ComputerDomain { get; set; }
        public string DeviceDetails { get; set; }
        public string Hostname { get; set; }        
        public string AssignedTo { get; set; }
           
        
        // Re-deploy asset to a different user

        
        // Checkbox for redeploy        
        public bool RedeployCheckBox { get; set; }
         
        public string HelpdeskRef { get; set; }

        public int DomainId { get; set; }
        public SelectList Domain { get; set; }

        [Required(ErrorMessage = "Please choose a Requester.")]
        public string LookupRequester { get; set; }
        public string RequesterSID { get; set; }
        public string RequesterFirstName { get; set; }
        public string RequesterMiddleName { get; set; }
        public string RequesterSurname { get; set; }
        public string RequesterEmail { get; set; }
        public string RequesterDomain { get; set; }
        public string RequesterDomainLogon { get; set; }
        public string RequesterContactNo { get; set; }

        [Required(ErrorMessage = "Please choose an End User.")]
        public string LookupEndUser { get; set; }
        public string AssigneeSID { get; set; }
        public string AssigneeFirstName { get; set; }
        public string AssigneeMiddleName { get; set; }
        public string AssigneeSurname { get; set; }
        public string AssigneeEmail { get; set; }
        public string AssigneeDomain { get; set; }
        public string AssigneeDomainLogon { get; set; }
        public string AssignedContactNo { get; set; }

        // To Pass Parameters of user device most recently assigned to

        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }

        // Change Hostname/Build


        // Re-deploy asset to a different user
        public bool ChangeBuildHostnameCheckBox { get; set; }

        [Required(ErrorMessage = "Please choose a location")]
        public string LookupLocation { get; set; }
        public int LocationId { get; set; }


        [Required(ErrorMessage = "Please choose Device")]        
        public int? DeviceId { get; set; }

        
        public string LookupBuild { get; set; }
        public int BuildId { get; set; }
        public string BuildDomain { get; set; }       

        [Required(ErrorMessage = "Please fetch a Hostname")]        
        public int HostnameId { get; set; }


        public string Notes { get; set; }

        public int StaffId { get; set; }

         
    }
}