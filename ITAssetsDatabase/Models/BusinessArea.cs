using System;
using System.Collections.Generic;

namespace ITAssetsDatabase.Models
{
    
    public class BusinessArea
    {
        public BusinessArea()
        {
            this.EndUsers = new HashSet<EndUser>();
            this.RaisedBy = new HashSet<RaisedBy>();
            this.Staff = new HashSet<Staff>();
        }
    
        public int Id { get; set; }
        public string Department { get; set; }
        public string Floor { get; set; }
        public string Build { get; set; }
        public string Costcentre { get; set; }
        public string ProjectCode { get; set; }
        public int? AddressId { get; set; }
        public int? orderitemid { get; set; }
    
        public virtual ICollection<EndUser> EndUsers { get; set; }
        public virtual ICollection<RaisedBy> RaisedBy { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
        public virtual Address Address { get; set; }
    }
}
