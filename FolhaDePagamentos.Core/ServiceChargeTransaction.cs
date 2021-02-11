using System;

namespace FolhaDePagamentos.Core
{
    public class ServiceChargeTransaction : Transaction
    {
        private readonly int memberId;
        private readonly DateTime time;
        private readonly double charge;

        public ServiceChargeTransaction(int memberId, DateTime time, double charge)
        {
            this.memberId = memberId;
            this.time = time;
            this.charge = charge;
        }

        public void Execute()
        {
            Employee e = PayrollDatabase.GetUnionMember(memberId);

            if(e != null)
            {
                UnionAffiliation ua = null;
                if(e.Affiliation is UnionAffiliation)
                    ua = e.Affiliation as UnionAffiliation;

                if(ua != null)
                    ua.AddServiceCharge(new ServiceCharge(time, charge));
                else
                    throw new InvalidOperationException(
                        "Tries to add service charge to union member without an union affiliation"
                    );
            }
            else
                throw new InvalidOperationException("No such union member");
        }
    }
}