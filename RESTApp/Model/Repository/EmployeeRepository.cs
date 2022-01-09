using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTApp.Model.Repository
{
    public class EmployeeRepository : IEmployeeRepository

    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var resultat = await appDbContext.Employees.AddAsync(employee);
            await appDbContext.SaveChangesAsync();
          return resultat.Entity;
        }

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            var resultat = await appDbContext.Employees.SingleOrDefaultAsync(e => e.EmployeeId == employeeId);
           if (resultat!= null)
            {
                appDbContext.Employees.Remove(resultat);
                await appDbContext.SaveChangesAsync();
                return resultat;
            }
            return null;
            
        }
        public async Task<Employee> GetEmployee(int employeeId)

        {
            return await appDbContext.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await appDbContext.Employees.ToListAsync();
        }

        public async  Task<Employee> UpdateEmployee(Employee employee)
        {
            var resultat = await appDbContext.Employees.SingleOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
            if (resultat != null)
            {
                resultat.FirstName = employee.FirstName;
                resultat.LastName = employee.LastName;
                resultat.Email = employee.Email;
                resultat.DatofBirth = employee.DatofBirth;
                resultat.Gender = employee.Gender;
                resultat.DepartmentId = employee.DepartmentId;
                resultat.PhotoPath = employee.PhotoPath;
                await appDbContext.SaveChangesAsync(); 
                return resultat;
            }
            return null;
        }
        public async Task<Employee> GetEmployeeByEmail(String email)
        {
            return await appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> query = appDbContext.Employees;

            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }
            if(gender != null)
            {
                query = query.Where(e => e.Gender == gender);
            }

            return await query.ToListAsync();
        }
    }
}
