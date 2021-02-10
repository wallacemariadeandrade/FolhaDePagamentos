namespace FolhaDePagamentos.Core
{
    public class AddSalariedEmployee : AddEmployeeTransaction
    {
        private readonly double salary;

        public AddSalariedEmployee(int empId, string name, string address, double salary) : base(empId, name, address)
        {
            this.salary = salary;
        }

        protected override PaymentClassification MakeClassification() => new SalariedClassification(salary);

        protected override PaymentSchedule MakeSchedule() => new MonthlySchedule();
    }
}