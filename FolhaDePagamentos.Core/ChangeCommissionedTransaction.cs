namespace FolhaDePagamentos.Core
{
    public class ChangeCommissionedTransaction : ChangeClassificationTransaction
    {
        private readonly double newSalary;
        private readonly double newCommissionRate;
        public ChangeCommissionedTransaction(int empId, double newSalary, double newCommissionRate) : base(empId)
        {
            this.newSalary = newSalary;
            this.newCommissionRate = newCommissionRate;
        }

        protected override PaymentClassification Classification => new CommissionedClassification(newSalary,  newCommissionRate);

        protected override PaymentSchedule Schedule => new BiweeklySchedule();
    }
}