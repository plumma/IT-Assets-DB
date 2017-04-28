using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsDatabase.Models
{
    public class UploadedFile
    {
        public int id { get; set; }
        public string FileName { get; set; }

        [ForeignKey("Upload")]
        public int UploadId { get; set; }
        public virtual Upload Upload { get; set; }


    }
}