using System;
using System.Linq;
using System.Collections.Generic;
using JoergIsAGeek.Workshop.DomainModel;
using JoergIsAGeek.Workshop.DataTransferObjects;
using JoergIsAGeek.Workshop.Repository;
using AutoMapper;

namespace JoergIsAGeek.Workshop.BusinessLogicLayer {

    public class CompanyUserManager : Manager, ICompanyUserManager {

        private IGenericRepository<CompanyUser> repCompanyUser;

        public CompanyUserManager(IGenericRepository<CompanyUser> repCompanyUser){
            this.repCompanyUser = repCompanyUser;
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeDto>()
                    // Fall 1: Raum zwingend
                   .ForMember(m => m.RoomId, e => e.MapFrom(o => o.Room.Id))
                    // Fall 2: Raum optional
                   .ForMember(m => m.RoomId, e => e.MapFrom(o => o.Room == null ? 0 : o.Room.Id))
                   .ForMember(m => m.HasProjects, e => e.MapFrom(o => o.Projects.Any()));
            });
            mapper = config.CreateMapper();
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(){
            var models = this.repCompanyUser.FilterRead<Employee>(e => true, e => e.Room, e => e.Projects);
            return models.Select(m => mapper.Map<EmployeeDto>(m));
        }

        public EmployeeDto GetEmployeeById(int id){
            var model = this.repCompanyUser.FilterRead<Employee>(e => e.Id == id, e => e.Room, e => e.Projects).SingleOrDefault();
            return mapper.Map<EmployeeDto>(model);
        }

        public bool SaveEmployee(EmployeeDto dto) {
            
            return true;
        }

    }

}