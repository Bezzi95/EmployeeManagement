using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTApp.Model.Repository
{
  public class  DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext appDbContext;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

         async Task<Department> IDepartmentRepository.GetDepartment(int departmentId)
        {
            return await appDbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }

        public async Task<IEnumerable<Department>> GetDepartments()

        {
            return await appDbContext.Departments.ToListAsync();
        }

      
    }
}
