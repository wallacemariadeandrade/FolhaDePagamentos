namespace FolhaDePagamentos.Core
{
    public abstract class ChangeAffiliationTransaction : ChangeEmployeeTransaction
    {
        protected ChangeAffiliationTransaction(int empId) : base(empId)
        {
        }

        protected override void Change(Employee e)
        {
            RecordMembership(e);
            Affiliation affiliation = Affiliation;
            e.Affiliation = affiliation;
        }

        protected abstract Affiliation Affiliation { get; }
        protected abstract void RecordMembership(Employee e);
    }
}