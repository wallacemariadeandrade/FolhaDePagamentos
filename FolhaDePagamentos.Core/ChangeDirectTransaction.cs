namespace FolhaDePagamentos.Core
{
    public class ChangeDirectTransaction : ChangeEmployeeTransaction
    {
        public ChangeDirectTransaction(int empId) : base(empId)
        {
        }

        protected override void Change(Employee e)
        {
            e.Method = new DirectMethod();
        }
    }
}