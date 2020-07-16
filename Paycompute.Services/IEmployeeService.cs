using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Paycompute.Entity;

namespace Paycompute.Services
{
    public interface IEmployeeService
    {
        Task CreateAsync(Employee newEmployee);

        Employee GetById(int employeeId);

        Task UpdateAsync(Employee employee);

        Task UpdateAsync(int id);

        Task Delete(int employeeId);

        //id is used to check if the employee belongs to the union
        decimal UnionFee(int id);

        //similarly id is used to check if employee pays SL repayment depends on amount earn
        decimal StudentLoanRepaymentAmount(int id, decimal totalAmount);

        IEnumerable<Employee> GetAll();

        IEnumerable<SelectListItem> GetAllEmployeesForPayroll();
    }
}
