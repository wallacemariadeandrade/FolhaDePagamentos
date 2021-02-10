namespace FolhaDePagamentos.Core
{
    public abstract class AddEmployeeTransaction : Transaction
    {
        protected int empId;
        protected string itsAddress;
        protected string itsName;
        
        public AddEmployeeTransaction(int empId, string name, string address)
        {
            this.empId = empId;
            this.itsAddress = address;
            this.itsName = name;
        }

        // Pattern Factory Method
        // Geralmente são nomeados com MakeXXX
        protected abstract PaymentClassification MakeClassification();
        protected abstract PaymentSchedule MakeSchedule();
        
        // Pattern Template Method
        // O cliente pode modificar partes do algoritmo, 
        // mas não sua estrutura base
        public void Execute()
        {
            PaymentClassification pc = MakeClassification();
            PaymentSchedule ps = MakeSchedule();
            PaymentMethod pm = new HoldMethod();

            Employee e = new Employee(empId, itsName, itsAddress);
            e.Classification = pc;
            e.Schedule = ps;
            e.Method = pm;
            PayrollDatabase.AddEmployee(empId, e);
        }
    }
}