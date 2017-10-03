using System;
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

        public System.Data.Entity.DbSet<SmartPrint.Models.DocTypes> DocTypes { get; set; }

        public System.Data.Entity.DbSet<SmartPrint.Models.UserDocs> UserDocs { get; set; }

        public System.Data.Entity.DbSet<SmartPrint.Models.UserTxns> UserTxns { get; set; }
                
        public System.Data.Entity.DbSet<SmartPrint.Models.PrintCosts> PrintCosts { get; set; }

        public System.Data.Entity.DbSet<SmartPrint.Models.PrintJobs> PrintJobs { get; set; }

        public System.Data.Entity.DbSet<SmartPrint.Models.Configurations> Configurations { get; set; }

        public System.Data.Entity.DbSet<SmartPrint.ViewModels.UserTransactionViewModel> UserTransactionViewModels { get; set; }

        public System.Data.Entity.DbSet<SmartPrint.ViewModels.UserDocsViewModel> UserDocsViewModels { get; set; }

        public System.Data.Entity.DbSet<SmartPrint.ViewModels.PrintJobsViewModel> PrintJobsViewModels { get; set; }
    }
}