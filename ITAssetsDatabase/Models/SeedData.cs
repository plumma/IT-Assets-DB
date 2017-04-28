using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ITAssetsDatabase.Models
{
    public class SeedData : DropCreateDatabaseIfModelChanges<ITAssetsDatabaseDBContext>
    {

        protected override void Seed(ITAssetsDatabaseDBContext context)
        {
            //base.Seed(context);

            var Assets = new List<Asset>
            {
                new Asset { PRRef ="11111", PORef ="11111" },
                new Asset { PRRef = "11111", PORef = "11111" }

            };

            foreach (var eachrecord in Assets)
            {
                context.Assets.Add(eachrecord);
            }

            context.SaveChanges();
    }



    }
}