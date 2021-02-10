namespace FolhaDePagamentos.Core
{
    // Exercício proposto
    public class HourlyClassification : PaymentClassification
    {
        public HourlyClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
        }

        public double HourlyRate { get; }
    }
}