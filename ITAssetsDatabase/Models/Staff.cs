using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models
{

     [Table("Staff")] 	
    public class Staff
    {
        public Staff()
        {
            
            this.Uploads = new HashSet<Upload>();
            this.Articles = new HashSet<Article>();
            this.Assets = new HashSet<Asset>();
        }
          
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string Extension { get; set; }
        public string Domain { get; set; }
        public string DomainLogon { get; set; }
        public string Rights { get; set; }
       
        public int? BusinessAreaId { get; set; }
    
        
        public virtual ICollection<Upload> Uploads { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }  

        public virtual BusinessArea BusinessArea { get; set; }

        [NotMapped]
        public string FullName      { get { return SecondName + ", " + FirstName; }}

        [NotMapped]
        public string LoggedInName { get { return Domain + @"\" + DomainLogon; } }     
    }
}
