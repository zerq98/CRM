using AutoMapper;
using CRM.Application.Dto.User;
using CRM.Application.Interface;
using CRM.Domain.Entity;
using CRM.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
                           IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<string> AddUserAsync(ApplicationUserCreateVM user)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.Email = user.Email;
            applicationUser.UserName = user.Email;
            applicationUser.EmailConfirmed = true;
            applicationUser.FirstName = user.FirstName;
            applicationUser.LastName = user.LastName;
            applicationUser.IsActive = true;
            applicationUser.LockoutEnabled = true;
            applicationUser.PhoneNumber = user.PhoneNumber;
            applicationUser.PhoneNumberConfirmed = true;
            applicationUser.PrivateEmail = user.PrivateEmail;
            return await _userRepository.AddUserAsync(applicationUser,user.Password);
        }

        public async Task AssignUserToRoleAsync(string userId, List<UserRolesVM> roles)
        {
            var rolesToAdd = roles.Where(x => x.IsSelected).Select(x=>x.RoleName).ToList();
            await _userRepository.AssignUserToRoleAsync(userId, rolesToAdd);
        }

        public async Task<string> EditUserAsync(ApplicationUserEditVM model)
        {
            var user = await _userRepository.GetUserByIdAsync(model.Id);

            if (user == null)
            {
                return String.Empty;
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.PrivateEmail = model.PrivateEmail;
            await _userRepository.EditUserAsync(user);
            return user.Id;
        }

        public async Task RemoveUser(string id)
        {
            await _userRepository.RemoveUser(id);
        }

        public async Task<ApplicationUserVM> GetApplicationUser(string id)
        {
            var user = _mapper.Map<ApplicationUserVM>(await _userRepository.GetUserByIdAsync(id));
            user.Roles = await _userRepository.GetUserRoles(id);
            return user;
        }

        public async Task<List<ApplicationUserVM>> GetAllUsers()
        {
            var usersFromApp = await _userRepository.GetApplicationUsers();
            var users = new List<ApplicationUserVM>();
            foreach(var user in usersFromApp)
            {
                users.Add(_mapper.Map<ApplicationUserVM>(user));
            }
            return users;
        }
    }
}
