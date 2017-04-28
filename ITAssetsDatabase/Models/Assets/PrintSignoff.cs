using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITAssetsDatabase.Models.Assets
{
    public class PrintSignoff
    {
        public string EndUser { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string UserDomain { get; set; }
        public string Hostname { get; set; }
        public string Build { get; set; }
        public string HelpdeskRef  { get; set; }
        public string PORef { get; set; }
        public string PRRef { get; set; }
        public string Type { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Asset { get; set; }
        public string SerialNo { get; set; }
        public string MACAddress { get; set; }
        public string DeviceDomain { get; set; }
        public string Engineer { get; set; }

    }
}