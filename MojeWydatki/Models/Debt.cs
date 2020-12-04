using MojeWydatki.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojeWydatki.Models
{
    public class Debt : SModel
    {
        public String Person { get; set; }
        public String Description { get; set; }
        public Double CurrentValue { get; set; }
        public Double DebtValue { get; set; }
        public Double Progress { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public bool AmILender { get; set; }
    }
}
