namespace FolhaDePagamentos.Core
{
    // Pattern Command
    public interface Transaction
    {
        void Execute();
    }
}