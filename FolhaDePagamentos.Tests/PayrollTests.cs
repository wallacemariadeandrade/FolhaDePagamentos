using System;
using FolhaDePagamentos.Core;
using Xunit;

namespace FolhaDePagamentos.Tests
{
    public class PayrollTests
    {
        [Fact]
        public void TestAddSalariedEmployee()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.Equal("Bob", e.Name);

            PaymentClassification pc = e.Classification;
            Assert.True(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;
            Assert.Equal(1000.00, sc.Salary, 3);
            PaymentSchedule ps = e.Schedule;
            Assert.True(ps is MonthlySchedule);

            PaymentMethod pm = e.Method;
            Assert.True(pm is HoldMethod);
        }

        // Exercício proposto
        [Fact]
        public void TestAddHourlyEmployee()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Wallace", "Company", 17.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.Equal("Wallace", e.Name);

            PaymentClassification pc = e.Classification;
            Assert.True(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;
            Assert.Equal(17.00, hc.HourlyRate, 3);
            PaymentSchedule ps = e.Schedule;
            Assert.True(ps is WeeklySchedule);

            PaymentMethod pm = e.Method;
            Assert.True(pm is HoldMethod);
        }

        // Exercício proposto
        [Fact]
        public void TestAddCommissionedEmployee()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Ianna Caren", "Clinic", 1800.00, 0.03);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.Equal("Ianna Caren", e.Name);

            PaymentClassification pc = e.Classification;
            Assert.True(pc is CommissionedClassification);
            CommissionedClassification cc = pc as CommissionedClassification;
            Assert.Equal(1800.00, cc.Salary, 3);
            Assert.Equal(0.03, cc.CommissionRate, 3);
            PaymentSchedule ps = e.Schedule;
            Assert.True(ps is BiweeklySchedule);

            PaymentMethod pm = e.Method;
            Assert.True(pm is HoldMethod);
        }

        [Fact]
        public void DeleteEmployee()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            DeleteEmployeeTransaction dt = new DeleteEmployeeTransaction(empId);
            dt.Execute();

            e = PayrollDatabase.GetEmployee(empId);
            Assert.Null(e);
        }

        [Fact]
        public void TestTimeCardTransaction()
        {
            int empId = 5;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            TimeCardTransaction tct = new TimeCardTransaction(
                new DateTime(2005, 7, 31), 8.0, empId);
            tct.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.True(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;

            TimeCard tc = hc.GetTimeCard(new DateTime(2005, 7, 31));
            Assert.NotNull(tc);
            Assert.Equal(8.0, tc.Hours);
        }

        [Fact]
        public void TestSalesReceiptTransaction()
        {
            int empId = 6;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bob", "Home", 1500.00, 2.5);
            t.Execute();
            SalesReceiptTransaction srt = new SalesReceiptTransaction(
                new DateTime(2021, 1, 10), 300.00, empId);
            srt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.True(pc is CommissionedClassification);
            CommissionedClassification cc = pc as CommissionedClassification;

            SalesReceipt sr = cc.GetSalesReceipt(new DateTime(2021, 1, 10));
            Assert.NotNull(sr);
            Assert.Equal(300.00, sr.Amount);
        }

        [Fact]
        public void AddServiceCharge()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bob", "Home", 15.25);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            UnionAffiliation af = new UnionAffiliation();
            e.Affiliation = af;
            int memberId = 86; // Maxwell Smart
            PayrollDatabase.AddUnionMember(memberId, e);
            ServiceChargeTransaction sct = new ServiceChargeTransaction(
                memberId, new DateTime(2005, 8, 8), 12.95);
            sct.Execute();
            ServiceCharge sc = af.GetServiceCharge(new DateTime(2005, 8, 8));
            Assert.NotNull(sc);
            Assert.Equal(12.95, sc.Amount, 3);
        }

        [Fact]
        public void TestChangeNameTransaction()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bob", "Home", 15.25);
            t.Execute();
            ChangeNameTransaction cnt = new ChangeNameTransaction(empId, "Bob");
            cnt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            Assert.Equal("Bob", e.Name);
        }

        [Fact]
        public void TestChangeAddressTransaction()
        {   
            int empId = 3;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bob", "Home", 15.25);
            t.Execute();
            ChangeAddressTransaction cat = new ChangeAddressTransaction(empId, "Company");
            cat.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            Assert.Equal("Company", e.Address);
        }

        [Fact]
        public void TestChangeHourlyTransaction()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Lance", "Home", 2500.00, 3.2);
            t.Execute();
            ChangeHourlyTransaction cht = new ChangeHourlyTransaction(empId, 27.52);
            cht.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.NotNull(pc);
            Assert.True(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;
            Assert.Equal(27.52, hc.HourlyRate, 3);
            PaymentSchedule ps = e.Schedule;
            Assert.True(ps is WeeklySchedule);
        }

        [Fact]
        public void TestChangeSalariedTransaction()
        {
            int empId = 11;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Will", "Home", 18.90);
            t.Execute();
            ChangeSalariedTransaction cst = new ChangeSalariedTransaction(empId, 1950.00);
            cst.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.NotNull(pc);
            Assert.True(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;
            Assert.Equal(1950.00, sc.Salary, 3);
            PaymentSchedule ps = e.Schedule;
            Assert.True(ps is MonthlySchedule);
        }

        [Fact]
        public void TestChangeCommissionedTransaction()
        {
            int empId = 15;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bob", "Home", 15.25);
            t.Execute();
            ChangeCommissionedTransaction cct = new ChangeCommissionedTransaction(empId, 1600.00, 12.00);
            cct.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.NotNull(pc);
            Assert.True(pc is CommissionedClassification);
            CommissionedClassification cc = pc as CommissionedClassification;
            Assert.Equal(1600.00, cc.Salary, 3);
            Assert.Equal(12.00, cc.CommissionRate, 3);
            PaymentSchedule ps = e.Schedule;
            Assert.True(ps is BiweeklySchedule);
        }

        [Fact]
        public void TestChangeMailTransaction()
        {
            int empId = 7;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00);
            t.Execute();
            ChangeMailTransaction cmt = new ChangeMailTransaction(empId);
            cmt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            PaymentMethod pm = e.Method;
            Assert.True(pm is MailMethod);
        }

        [Fact]
        public void TestChangeDirectTransaction()
        {
            int empId = 77;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Will", "Company", 23.90);
            t.Execute();
            ChangeDirectTransaction cmt = new ChangeDirectTransaction(empId);
            cmt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            PaymentMethod pm = e.Method;
            Assert.True(pm is DirectMethod);
        }

        [Fact]
        public void TestChangeHoldTransaction()
        {
            int empId = 26;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2800.00, 1.5);
            t.Execute();
            ChangeDirectTransaction cmt = new ChangeDirectTransaction(empId);
            cmt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            PaymentMethod pm = e.Method;
            Assert.True(pm is DirectMethod);
            
            ChangeHoldTransaction cht = new ChangeHoldTransaction(empId);
            cht.Execute();
            e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            pm = e.Method;
            Assert.True(pm is HoldMethod);
        }

        [Fact]
        public void ChangeUnionMember()
        {
            int empId = 8;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            int memberId = 7743;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empId, memberId, 99.42);
            cmt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            Affiliation affiliation = e.Affiliation;
            Assert.NotNull(affiliation);
            Assert.True(affiliation is UnionAffiliation);
            UnionAffiliation uf = affiliation as UnionAffiliation;
            Assert.Equal(99.42, uf.Dues, 3);
            Employee member = PayrollDatabase.GetUnionMember(memberId);
            Assert.NotNull(member);
            Assert.Equal(e, member);
        }
    }
}