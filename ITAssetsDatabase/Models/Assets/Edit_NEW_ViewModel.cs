using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;    // SelectList

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models.Assets
{
    public class Edit_NEW_ViewModel
    {
        public int ID { get; set; }


        [Display(Name = "Helpdesk Ticket Reference")]
        [Required(ErrorMessage = "The Helpdesk Reference field is required.")]
        [StringLength(10, ErrorMessage = "Invalid Ticket Reference")]       
        public string HelpdeskRef { get; set; }

        // User Domain combo boxes

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
               
         
        // Asset Details     

        [Required(ErrorMessage = "The Purchase Reference field is required.")]
        public string PRRef { get; set; }
        
        [Required(ErrorMessage = "The Purchase Order Reference field is required.")]
        public string PORef { get; set; }

        [Required(ErrorMessage = "The Asset Number field is required.")]
        public string AssetNo { get; set; }

        [Required(ErrorMessage = "The Serial Number field is required.")]
        public string SerialNo { get; set; }

        [Required(ErrorMessage = "The MAC address field is required.")]
        public string MAC_Address { get; set; }


        [Required(ErrorMessage = "Please choose a location")]
        public string LookupLocation { get; set; }
        public int LocationId { get; set; }
        
        
        [Required(ErrorMessage = "Please choose Device")]
        public string LookupDeviceModel { get; set; }
        public int DeviceId { get; set; }


        // Asset Build Details

        [Required(ErrorMessage = "Please choose Build")]
        public string LookupBuild { get; set; }
        public int BuildId { get; set; }
        public string BuildDomain { get; set; }

        [Required(ErrorMessage = "Please fetch a Hostname")]
        public string Hostname { get; set; }  // Only Hostname ID is registered but this is used to create field 
        public int HostnameId { get; set; }

        
        public string Notes { get; set; }

        // Creator

        public int StaffId { get; set; }

    }
}