using System.Collections;

namespace FolhaDePagamentos.Core
{
    // Esta implementação poderia ser feita 
    // usando os patterns SINGLETON ou MONOSTATE,
    // no entanto isso não significa que a atual
    // maneira seja errada
    public class PayrollDatabase
    {
        private static Hashtable employees = new Hashtable();
        public static void AddEmployee(int id, Employee employee) => employees[id] = employee;
        public static Employee GetEmployee(int id) => employees[id] as Employee;
        public static void DeleteEmployee(int id) => employees[id] = null;
    }
}