using ApiDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface ITodoTaskRepository
    {
        Task<TodoTask> CreateTodoTaskAsync(TodoTask todoTask);
        Task<List<TodoTask>> GetTodoTasksForUserAsync(string userId);
        Task<List<TodoTask>> GetTodoTasksForUserWithinDateRangeAsync(DateTime start, DateTime end, string userId);
        Task<bool> MarkTodoTaskAsFinishedAsync(int todoTaskId);
        Task<TodoTask> GetTodoTaskByIdAsync(int todoTaskId);
        Task<bool> RemoveTodoTaskAsync(int todoTaskId);
    }
}
