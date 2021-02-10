using System;

namespace FolhaDePagamentos.Core
{
    public class TimeCard
    {
        private readonly DateTime date;
        private readonly double hours;

        public TimeCard(DateTime date, double hours)
        {
            this.date = date;
            this.hours = hours;
        }

        public double Hours => hours;
        public DateTime Date => date;
    }
}