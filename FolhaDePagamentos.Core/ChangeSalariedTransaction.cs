namespace FolhaDePagamentos.Core
{
    public class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
        private readonly double newSalary;
        public ChangeSalariedTransaction(int empId, double newSalary) : base(empId)
        {
            this.newSalary = newSalary;
        }

        protected override PaymentClassification Classification => new SalariedClassification(newSalary);

        protected override PaymentSchedule Schedule => new MonthlySchedule();
    }
}