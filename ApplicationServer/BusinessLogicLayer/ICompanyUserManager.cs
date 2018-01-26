using System.Collections.Generic;
using JoergIsAGeek.Workshop.DataTransferObjects;

namespace JoergIsAGeek.Workshop.BusinessLogicLayer
{
    public interface ICompanyUserManager
    {
        IEnumerable<EmployeeDto> GetAllEmployees();

        EmployeeDto GetEmployeeById(int id);

    }
}