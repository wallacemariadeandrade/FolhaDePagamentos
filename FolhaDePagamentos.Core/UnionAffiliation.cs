using System;
using System.Collections.Generic;
using System.Linq;

namespace FolhaDePagamentos.Core
{
    public class UnionAffiliation : Affiliation
    {
        private readonly List<ServiceCharge> charges = new List<ServiceCharge>();

        public double Dues { get; }
        public int MemberId { get; }

        public UnionAffiliation(int memberId, double dues)
        {
            MemberId = memberId;
            Dues = dues;
        }
        
        public UnionAffiliation()
        {
            
        }

        public void AddServiceCharge(ServiceCharge sc)
        {
            charges.Add(sc);
        }

        public ServiceCharge GetServiceCharge(DateTime time)
        {
            return charges.FirstOrDefault(x => x.Time.Date == time.Date);
        }
    }
}