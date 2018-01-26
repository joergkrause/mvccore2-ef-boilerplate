using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SodgeIt.Workshop.BusinessLogicLayer;
using SodgeIt.Workshop.DataTransferObjects;

namespace servicelayer.Controllers
{
    [Route("api/[controller]")]
    public class CompanyUserController : Controller
    {

        private readonly ICompanyUserManager companyUserManager;

        public CompanyUserController(ICompanyUserManager companyUserManager){
            this.companyUserManager = companyUserManager;
        }

        // GET api/companyuser
        [HttpGet]
        public IEnumerable<EmployeeDto> Get()
        {
            return companyUserManager.GetAllEmployees();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public EmployeeDto Get(int id)
        {
            var result = companyUserManager.GetEmployeeById(id);
            return result;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]EmployeeDto value)
        {
          
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]EmployeeDto value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
