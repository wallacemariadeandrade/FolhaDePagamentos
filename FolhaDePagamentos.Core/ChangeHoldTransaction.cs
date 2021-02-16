namespace FolhaDePagamentos.Core
{
    public class ChangeHoldTransaction : ChangeEmployeeTransaction
    {
        public ChangeHoldTransaction(int empId) : base(empId)
        {
        }

        protected override void Change(Employee e)
        {
            e.Method = new HoldMethod();
        }
    }
}