namespace FolhaDePagamentos.Core
{
    public class DeleteEmployeeTransaction : Transaction
    {
        private readonly int id;

        public DeleteEmployeeTransaction(int id)
        {
            this.id = id;
        }

        public void Execute() => PayrollDatabase.DeleteEmployee(id);
    }
}