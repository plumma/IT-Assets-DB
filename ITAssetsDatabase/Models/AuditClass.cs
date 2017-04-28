using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models
{
    public class AuditClass
    {
            [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
            public DateTime CreatedDate { get; set; }
            public string HelpdeskRef { get; set; }
            public string RequesterFullName { get; set; }
            public string EndUserFullName { get; set; }
            public string SignOffSheetFileName { get; set; }
            public string AssetStatus { get; set; }
            public string Notes { get; set; }
            public string Staff { get; set; }

    }
}