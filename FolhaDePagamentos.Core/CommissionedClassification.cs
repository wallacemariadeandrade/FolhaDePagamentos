using System;
using System.Collections.Generic;
using System.Linq;

namespace FolhaDePagamentos.Core
{
    // Exercício proposto
    public class CommissionedClassification : PaymentClassification
    {
        private readonly List<SalesReceipt> salesReceipts = new List<SalesReceipt>();
        public CommissionedClassification(double salary, double commissionRate)
        {
            Salary = salary;
            CommissionRate = commissionRate;
        }

        public double Salary { get; }
        public double CommissionRate { get; }

        public void AddSalesReceipt(SalesReceipt salesReceipt) => salesReceipts.Add(salesReceipt);
        public SalesReceipt GetSalesReceipt(DateTime dateTime) => salesReceipts.FirstOrDefault(x => x.Date.Date == dateTime.Date);
    }
}