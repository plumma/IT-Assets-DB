using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models
{
    public class Employee

    {

           public Employee()
        {
            this.Assets = new HashSet<Asset>();
            this.AssetSignoff = new HashSet<AssetSignoff>();     
        }  
         


        public int Id { get; set; }
        public string SID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string Extension { get; set; }
        public string Domain { get; set; }
        public string DomainLogon { get; set; }
        public int? BusinessAreaId { get; set; }

        public virtual BusinessArea BusinessArea { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return SecondName + ", " + FirstName; }

        }      


        public virtual ICollection<AssetAssignee> Requester { get; set; }
        public virtual ICollection<AssetAssignee> Assignee { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<AssetSignoff> AssetSignoff { get; set; }  
    }
} 