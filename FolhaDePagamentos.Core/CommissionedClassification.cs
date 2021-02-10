namespace FolhaDePagamentos.Core
{
    // Exercício proposto
    public class CommissionedClassification : PaymentClassification
    {
        public CommissionedClassification(double salary, double commissionRate)
        {
            Salary = salary;
            CommissionRate = commissionRate;
        }

        public double Salary { get; }
        public double CommissionRate { get; }
    }
}