using SmartPrint.Common.Enums;
using SmartPrint.Helpers;
using SmartPrint.Helpers.User;
using SmartPrint.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartPrint.ViewModels
{
    public class UserTransactionViewModel
    {
        public UserTransactionViewModel()
        {

        }
        public UserTransactionViewModel(UserTxns data, UserHelper userHelper,Dictionary<int,string> transactionTypeDetails)
        {
            TransactionId = data.TxnId;
            UserId = data.UserId;
            TransactionTypeId = data.TxnTypeId;
            TransactionAmount = data.TxnAmount;
            TransactionBalance = data.TxnBalance;
            TransactionDateTime = data.TxnDateTime;
            TransactionReferenceJobId = data.TxnRefJobId;
            TransactionStatus = data.TxnStatusId;
            NameOfUser = userHelper.GetNameOfUser(UserId);
            TransactionTypeName = transactionTypeDetails[TransactionTypeId];
            
        }

        public UserTxns GetDbObjectToCreate(int userId)
        {
            var now = DateTimeHelper.GetTimeStamp();
            var dataToReturn = new UserTxns()
            {
                TxnId = 0,
                UserId = UserId,
                TxnTypeId = TransactionTypeId,
                TxnAmount = TransactionAmount,
                TxnDateTime = now,
                TxnRefJobId = TransactionReferenceJobId,
                TxnStatusId = TransactionStatus,
                TxnBalance = TransactionBalance,
                AddedBy = userId,
                EditedBy = userId,
                AddedOn = now,
                EditedOn = now,
                StatusId = (int)RecordStatus.Active
            };
            return dataToReturn;
        }
        [Key]
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "Please Select a user")]
        [DisplayName("User")]
        public int UserId { get; set; }

        [Required]
        [DisplayName("Credit/Debit")]
        public int TransactionTypeId { get; set; }

        [Required(ErrorMessage = "Please specify the amount")]
        [DisplayName("Amount")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal TransactionAmount { get; set; }

        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",ApplyFormatInEditMode = false)]
        public DateTime TransactionDateTime { get; set; }
        
        [DisplayName("Balance")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal TransactionBalance { get; set; }

        [DisplayName("Print Job Details")]
        public int TransactionReferenceJobId { get; set; }
        
        [DisplayName("Transaction Status")]
        public int TransactionStatus { get; set; }
        [DisplayName("User Name")]
        public string NameOfUser { get; set; }
        public string TransactionTypeName { get; set; }

        internal void ChangeDbObjectForUpdate(UserTxns userTransactionToEdit, int loggedInUserId,DateTime updatedOn)
        {
            userTransactionToEdit.UserId = UserId;
            var amountDifference = TransactionAmount - userTransactionToEdit.TxnAmount;
            userTransactionToEdit.TxnAmount = TransactionAmount;
            userTransactionToEdit.TxnBalance = userTransactionToEdit.TxnBalance + amountDifference;
            userTransactionToEdit.EditedOn = updatedOn;
            userTransactionToEdit.EditedBy = loggedInUserId;
        }
    }
}