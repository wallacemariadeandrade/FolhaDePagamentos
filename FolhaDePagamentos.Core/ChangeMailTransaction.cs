namespace FolhaDePagamentos.Core
{
    public class ChangeMailTransaction : ChangeEmployeeTransaction
    {
        public ChangeMailTransaction(int empId) : base(empId)
        {
        }

        protected override void Change(Employee e)
        {
            e.Method = new MailMethod();
        }
    }
}