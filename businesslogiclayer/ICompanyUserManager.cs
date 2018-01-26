using System.Collections.Generic;
using SodgeIt.Workshop.DataTransferObjects;

namespace SodgeIt.Workshop.BusinessLogicLayer
{
    public interface ICompanyUserManager
    {
        IEnumerable<EmployeeDto> GetAllEmployees();

        EmployeeDto GetEmployeeById(int id);

    }
}