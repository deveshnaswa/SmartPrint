using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using SmartPrint.Models;

namespace SmartPrint
{
    public class MainDbContext : DbContext
    {


        public MainDbContext()
            : base("name=DefaultConnection")
        {
        }



        public DbSet<Users> Users { get; set; }

        public System.Data.Entity.DbSet<SmartPrint.Models.UserTypes> UserTypes { get; set; }

        public override int SaveChanges()
        {
            var auditable = ChangeTracker.Entries<ITrackable>().ToList();
            if (!auditable.Any()) return base.SaveChanges();

            foreach (var dbEntry in auditable)
            {
                switch (dbEntry.State)
                {
                    case System.Data.Entity.EntityState.Added:
                        dbEntry.Entity.AddedOn = DateTime.Now;
                        dbEntry.Entity.AddedBy = int.Parse( HttpContext.Current.User.Identity.GetUserId());
                        dbEntry.Entity.EditedOn = DateTime.Now;
                        dbEntry.Entity.EditedBy = int.Parse(HttpContext.Current.User.Identity.GetUserId());
                        break;
                    case System.Data.Entity.EntityState.Modified:
                        if (String.IsNullOrEmpty(dbEntry.Entity.AddedBy.ToString()))
                        {
                            
                            dbEntry.Entity.AddedOn= DateTime.Now;
                            dbEntry.Entity.AddedBy= int.Parse(HttpContext.Current.User.Identity.GetUserId());
                        }
                        
                        dbEntry.Entity.EditedOn= DateTime.Now;
                        dbEntry.Entity.EditedBy= int.Parse(HttpContext.Current.User.Identity.GetUserId());
                        break;
                }
            }

            return base.SaveChanges();
        }

        public System.Data.Entity.DbSet<SmartPrint.Models.DocTypes> DocTypes { get; set; }

        public System.Data.Entity.DbSet<SmartPrint.Models.UserDocs> UserDocs { get; set; }

        public System.Data.Entity.DbSet<SmartPrint.Models.UserTxns> UserTxns { get; set; }

        public System.Data.Entity.DbSet<SmartPrint.Models.PrintCosts> PrintCosts { get; set; }

        public System.Data.Entity.DbSet<SmartPrint.Models.PrintJobs> PrintJobs { get; set; }
    }
}