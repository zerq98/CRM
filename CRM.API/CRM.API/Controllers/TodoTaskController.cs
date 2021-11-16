using ApiApplication.DTO;
using ApiApplication.Helpers;
using ApiApplication.TodoTasks;
using ApiApplication.Validators;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTaskController : Controller
    {
        private readonly ITodoTaskService _todoTaskService;

        public TodoTaskController(ITodoTaskService todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }

        [HttpPost("MarkAsCompleted")]
        [EnableCors("AllowAnyOrigin")]
        [Authorize]
        public async Task<IActionResult> MarkAsCompleted(int todoTaskId)
        {
            var result = await _todoTaskService.MarkAsCompletedAsync(todoTaskId);

            var response = new ApiResponse<bool>();

            response.Code = result?200:404;
            response.Data = result;
            response.ErrorMessage = "";

            return new JsonResult(response);
        }

        [HttpGet("GetAllUserTasks")]
        [EnableCors("AllowAnyOrigin")]
        [Authorize]
        public async Task<IActionResult> GetAllUserTasks()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;

            var result = await _todoTaskService.GetTodoTasksForUserAsync(userId);

            var response = new ApiResponse<List<TodoTaskDto>>();

            response.Code = 200;
            response.Data = result;
            response.ErrorMessage = "";

            return new JsonResult(response);
        }

        [HttpPost("AddNewTask")]
        [EnableCors("AllowAnyOrigin")]
        [Authorize]
        public async Task<IActionResult> AddNewTask(CreateTodoTaskDto createTodoTaskDto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;
            createTodoTaskDto.UserId = userId;

            TodoTaskDto result = null;
            var errors = "";

            var validator = new CreateTodoTaskValidator();
            var valRes = validator.Validate(createTodoTaskDto);

            if (valRes.IsValid)
            {
                result = await _todoTaskService.AddTodoTaskAsync(createTodoTaskDto);
            }
            else
            {
                foreach(var error in valRes.Errors)
                {
                    errors += error.ErrorMessage + "\r\n";
                }
            }

            var response = new ApiResponse<TodoTaskDto>();

            response.Code = result!=null?201:valRes.IsValid?406:500;
            response.Data = result;
            response.ErrorMessage = result!=null?"":valRes.IsValid?"Coś poszło nie tak, sprawdź wszystkie dane i spróbuj ponownie.":errors;

            return new JsonResult(response);
        }

        [HttpPost("Remove")]
        [EnableCors("AllowAnyOrigin")]
        [Authorize]
        public async Task<IActionResult> RemoveTask(int todoTaskId)
        {
            var result = await _todoTaskService.RemoveTaskAsync(todoTaskId);

            var response = new ApiResponse<bool>();

            response.Code = result ? 200 : 404;
            response.Data = result;
            response.ErrorMessage = "Nie znaleziono ";

            return new JsonResult(response);
        }
    }
}
