using ApiApplication.DTO;
using ApiApplication.Helpers;
using ApiDomain.Entity;
using ApiDomain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Account.DashboardData
{
    public class GetDashboardDataHandler : IRequestHandler<GetDashboardDataQuery, IActionResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ITodoTaskRepository _todoTaskRepository;

        public GetDashboardDataHandler(IUserRepository userRepository, IDepartmentRepository departmentRepository,
                                       ITodoTaskRepository todoTaskRepository)
        {
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _todoTaskRepository = todoTaskRepository;
        }
        public async Task<IActionResult> Handle(GetDashboardDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.UserId);

                if (user != null)
                {
                    Department department = await _departmentRepository.GetDepartmentByIdAsync(user.DepartmentId);

                    var todoTasks = await _todoTaskRepository.GetTodoTasksForUserWithinDateRangeAsync(DateTime.Now, DateTime.Now, request.UserId);

                    var dashboardData = new DashboardDataDto
                    {
                        Name = user.FirstName + " " + user.LastName,
                        Department = department.Name,
                        Position = user.CompanyPosition,
                        TodoTasks = new List<DashboardTodoTaskDto>()
                    };

                    foreach(var task in todoTasks)
                    {
                        dashboardData.TodoTasks.Add(new DashboardTodoTaskDto
                        {
                            Id = task.Id,
                            Completed = task.IsFinished,
                            Title = task.Title
                        });
                    }

                    return new JsonResult(new ApiResponse<DashboardDataDto>
                    {
                        Code = 200,
                        Data = dashboardData,
                        ErrorMessage = ""
                    });
                }

                return new JsonResult(new ApiResponse<object>
                {
                    Data = null,
                    Code = 404,
                    ErrorMessage = "Nie znaleziono użytkownika."
                });
            }
            catch
            {
                return new JsonResult(new ApiResponse<object>
                {
                    Data = null,
                    Code = 500,
                    ErrorMessage = "Nastąpił problem z serwerem, skontaktuj się z działem IT."
                });
            }
        }
    }
}
