using System;

namespace FolhaDePagamentos.Core
{
    public class ServiceCharge
    {
        private readonly DateTime time;
        private readonly double amount;
        public ServiceCharge(DateTime time, double amount)
        {
            this.time = time;
            this.amount = amount;
        }

        public double Amount => amount;
        public DateTime Time => time;
    }
}