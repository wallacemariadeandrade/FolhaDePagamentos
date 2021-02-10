namespace FolhaDePagamentos.Core
{
    // Exercício proposto
    public class AddHourlyEmployee : AddEmployeeTransaction
    {
        private readonly double hourlyRate;
        public AddHourlyEmployee(int empId, string name, string address, double hourlyRate) : base(empId, name, address)
        {
            this.hourlyRate = hourlyRate;
        }

        protected override PaymentClassification MakeClassification() => new HourlyClassification(hourlyRate);

        protected override PaymentSchedule MakeSchedule() => new WeeklySchedule();
    }
}