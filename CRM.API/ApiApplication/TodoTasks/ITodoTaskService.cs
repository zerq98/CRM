using ApiApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.TodoTasks
{
    public interface ITodoTaskService
    {
        Task<bool> MarkAsCompletedAsync(int todoTaskId);
        Task<TodoTaskDto> AddTodoTaskAsync(CreateTodoTaskDto todoTask);
        Task<List<TodoTaskDto>> GetTodoTasksForUserAsync(string userId);
        Task<TodoTaskDto> GetByIdAsync(int todoTaskId);
    }
}
