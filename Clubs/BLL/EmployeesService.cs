using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clubs.DAL;
using Clubs.BLL;
using Clubs.Entities;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Clubs.BLL
{
    public class EmployeesService
    {
        #region set up context connection variable and class constructor 

        //limit use of the variable within this class
        private readonly StarTEDContext _context;

        //constructor to be used in the creation of the instance of this class
        internal EmployeesService(StarTEDContext regcontext) 
        {
            _context = regcontext;
        }
        #endregion

        #region Queries
        public List<Employee> Employee_List()
        {
            return _context.Employees.OrderBy(x => x.FirstName).ToList();
        }

        public List<Employee> GetEmployeeList()
        {
            
            return _context.Employees
                    .Where(x => x.Clubs.Any(y => x.EmployeeID == y.EmployeeID))
                        .OrderBy(x => x.LastName)
                            .ToList();
        }

        public Employee Employee_GetById(int employeeid)
        {
            return _context.Employees
                    .Where(x => x.Clubs.Any(y => x.EmployeeID == y.EmployeeID))
                    .OrderBy(x => x.LastName).FirstOrDefault();
        }
        #endregion



    }
}
