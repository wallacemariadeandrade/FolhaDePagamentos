using System;

namespace FolhaDePagamentos.Core
{
    public abstract class ChangeEmployeeTransaction : Transaction
    {
        private readonly int empId;

        protected ChangeEmployeeTransaction(int empId)
        {
            this.empId = empId;
        }

        public void Execute()
        {
            Employee e = PayrollDatabase.GetEmployee(empId);

            if(e != null)
                Change(e);
            else
                throw new InvalidOperationException("No such employee");
        }

        protected abstract void Change(Employee e);
    }
}