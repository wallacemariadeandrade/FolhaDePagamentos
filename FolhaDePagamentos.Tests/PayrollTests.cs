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
    }
}