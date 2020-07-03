using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Model
{
    public partial class StudentFee
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string AdmissionNo { get; set; }
        public string Name { get; set; }
        public string Father { get; set; }
        public string Months { get; set; }
        public string Class { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public string TotalAmount { get; set; }
        public string TransportFee { get; set; }
        public string PreviousDue { get; set; }
        public string OldBalanced { get; set; }
        public string Fine { get; set; }
        public string Concession { get; set; }
        public string AdmissionFee { get; set; }
        public Nullable<decimal> GrandTotal { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public string Balance { get; set; }
        public string ReciptNo { get; set; }
        public string Remark { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public string BankName { get; set; }
        public string CardPaymentRecieptNo { get; set; }
        public string Session { get; set; }
        public string BalancedShow { get; set; }
        public string HobbyFee { get; set; }
    }
}
