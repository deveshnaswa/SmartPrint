using System;
using System.Collections.Generic;

namespace SmartPrint.ViewModels
{
    public class UserTransactionListViewModel
    {
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TransationType { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<UserTransactionViewModel> Results { get; set; }
    }
}