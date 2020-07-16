using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paycompute.Entity;
using Paycompute.Persistence;

namespace Paycompute.Services.Implementation
{
    public class PayComputationService : IPayComputationService
    {
        private decimal contratualEarnings;
        private decimal overtimeHours;
        private readonly ApplicationDbContext _context;

        public PayComputationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
            await _context.PaymentRecords.AddAsync(paymentRecord);
            await _context.SaveChangesAsync();
        }

        public PaymentRecord GetById(int id) => _context.PaymentRecords.Where(pay => pay.Id == id).FirstOrDefault();
       

        public IEnumerable<PaymentRecord> GetAll() => _context.PaymentRecords.OrderBy(p => p.EmployeeId);
       

        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var allTaxYear = _context.TaxYears.Select(taxYear => new SelectListItem
            {
                Text = taxYear.YearOfTax,
                Value = taxYear.Id.ToString()
            });
            return allTaxYear;
        }

        public decimal OverTimeHours(decimal hoursWorked, decimal contractualHours)
        {
            if (hoursWorked <= contractualHours)
                overtimeHours = 0.00m;
            else if (hoursWorked > contractualHours)
                overtimeHours = hoursWorked - contractualHours;
            return overtimeHours;

        }

        public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            if (hoursWorked < contractualHours)
                contratualEarnings = hoursWorked * hourlyRate;
            else
                contratualEarnings = contractualHours * hourlyRate;
            return contratualEarnings;
        }

        public decimal OvertimeRate(decimal hourlyRate) => hourlyRate * 1.5m;
        

        public decimal OvertimeEarnings(decimal overtimeRate, decimal overTimeHours) => overTimeHours * overtimeRate;


        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings)
        => overtimeEarnings + contractualEarnings;

        public decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFee)
       => tax + nic + studentLoanRepayment + unionFee;

        public decimal NetPay(decimal totalEarnings, decimal totalDeduction) => totalEarnings - totalDeduction;

        public TaxYear GetTaxYearById(int id)
        => _context.TaxYears.Where(year => year.Id == id).FirstOrDefault();
    }
}
