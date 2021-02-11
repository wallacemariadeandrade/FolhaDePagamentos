using System;

namespace FolhaDePagamentos.Core
{
    public class SalesReceiptTransaction : Transaction
    {
        private readonly DateTime date;
        private readonly double amount;
        private readonly int empId;

        public SalesReceiptTransaction(DateTime date, double amount, int empId)
        {
            this.date = date;
            this.amount = amount;
            this.empId = empId;
        }

        public void Execute()
        {
            Employee e = PayrollDatabase.GetEmployee(empId);

            if(e != null)
            {
                CommissionedClassification cc = e.Classification as CommissionedClassification;

                if(cc != null)
                    cc.AddSalesReceipt(new SalesReceipt(date, amount));
                else
                    throw new InvalidOperationException("Tried to add sales receipt to non-commissioned employee");
            }
            else
                throw new InvalidOperationException("No such employee");
        }
    }
}