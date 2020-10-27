using AutoMapper;
using CRM.Application.Dto;
using CRM.Application.Dto.Customer;
using CRM.Application.Dto.User;
using CRM.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Reflection;

namespace CRM.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerViewDto>()
                .ForMember(v=>v.ContactInformation,opt=>opt.MapFrom(x=>x.ContactInformation.CustomerContacts));
            CreateMap<CustomerAddressDetails, CustomerAddressViewDto>()
                .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country.Name));
            CreateMap<CustomerContact, CustomerContactViewDto>()
                .ForMember(s => s.ContactType, opt => opt.MapFrom(s => s.ContactType.Name));
            CreateMap<ApplicationUser, ApplicationUserVM>()
                .ForMember(s => s.Roles, opt => opt.Ignore());
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerAddressCreateDto, CustomerAddressDetails>();
            CreateMap<CustomerContactCreateDto, CustomerContact>();
            CreateMap<CreateRoleVm, IdentityRole>();
            CreateMap<IdentityRole, UserRolesVM>();
        }
    }
}