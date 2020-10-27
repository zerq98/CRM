using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRM.Application.Dto;
using CRM.Application.Dto.User;
using CRM.Application.Interface;
using CRM.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Service
{
    public class RoleService :IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository,
                           IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<string> CreateRoleAsync(CreateRoleVm model)
        {
            var role = _mapper.Map<IdentityRole>(model);
            return await _roleRepository.AddAsync(role);
        }

        public async Task RemoveRoleAsync(string id)
        {
            await _roleRepository.RemoveRoleAsync(id);
        }

        public async Task<List<UserRolesVM>> GetRolesAsync()
        {
            var roles = new List<UserRolesVM>();
            foreach(var role in await _roleRepository.GetAllAsync())
            {
                roles.Add(new UserRolesVM { RoleId = role.Id, RoleName = role.Name });
            }

            return roles;
        }
    }
}
