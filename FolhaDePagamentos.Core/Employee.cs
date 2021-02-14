namespace FolhaDePagamentos.Core
{
    public class Employee
    {
        public Employee( int empId, string name, string address)
        {
            Name = name;
            Address = address;
            EmpId = empId;
        }

        public PaymentClassification Classification { get; set; }
        public PaymentSchedule Schedule { get; set; }
        public PaymentMethod Method { get; set; }
        public Affiliation Affiliation { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int EmpId { get; }
    }
}