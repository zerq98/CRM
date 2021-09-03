using ApiApplication.DTO;
using ApiApplication.Helpers;
using ApiApplication.TodoTasks;
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

            response.Code = result?200:400;
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

            var result = await _todoTaskService.AddTodoTaskAsync(createTodoTaskDto);

            var response = new ApiResponse<TodoTaskDto>();

            response.Code = result!=null?201:500;
            response.Data = result;
            response.ErrorMessage = result!=null?"":"Coś poszło nie tak, sprawdź wszystkie dane i spróbuj ponownie.";

            return new JsonResult(response);
        }

        [HttpPost("Remove")]
        [EnableCors("AllowAnyOrigin")]
        [Authorize]
        public async Task<IActionResult> RemoveTask(int todoTaskId)
        {
            var result = await _todoTaskService.RemoveTaskAsync(todoTaskId);

            var response = new ApiResponse<bool>();

            response.Code = result ? 200 : 400;
            response.Data = result;
            response.ErrorMessage = "";

            return new JsonResult(response);
        }
    }
}
