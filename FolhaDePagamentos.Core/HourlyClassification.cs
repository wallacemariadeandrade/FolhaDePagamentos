using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FolhaDePagamentos.Core
{
    // Exercício proposto
    public class HourlyClassification : PaymentClassification
    {
        private List<TimeCard> timeCards = new List<TimeCard>();
        public HourlyClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
        }

        public double HourlyRate { get; }

        public void AddTimeCard(TimeCard timeCard)
        {
            timeCards.Add(timeCard);
        }

        public TimeCard GetTimeCard(DateTime dateTime)
        {
            return timeCards.FirstOrDefault(x => x.Date.Date == dateTime.Date);
        }
    }
}