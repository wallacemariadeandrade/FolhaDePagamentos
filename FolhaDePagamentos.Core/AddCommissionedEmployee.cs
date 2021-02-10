namespace FolhaDePagamentos.Core
{
    public class AddCommissionedEmployee : AddEmployeeTransaction
    {
        private readonly double salary;
        private readonly double commissionRate;
        public AddCommissionedEmployee(int empId, string name, string address, double salary, double commissionRate) : base(empId, name, address)
        {
            this.salary = salary;
            this.commissionRate = commissionRate;
        }

        protected override PaymentClassification MakeClassification() => new CommissionedClassification(salary, commissionRate);

        protected override PaymentSchedule MakeSchedule() => new BiweeklySchedule();
    }
}