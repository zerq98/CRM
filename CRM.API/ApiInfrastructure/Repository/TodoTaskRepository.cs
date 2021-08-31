using ApiDomain.Entity;
using ApiDomain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInfrastructure.Repository
{
    public class TodoTaskRepository : BaseRepository, ITodoTaskRepository
    {
        public TodoTaskRepository(AppDbContext context) : base(context) { }

        public async Task<TodoTask> CreateTodoTaskAsync(TodoTask todoTask)
        {
            try
            {
                await _context.TodoTasks.AddAsync(todoTask);
                await _context.SaveChangesAsync();
                return todoTask;
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "TodoTaskRepository/CreateTodoTaskAsync"
                });
                await _context.SaveChangesAsync();

                throw;
            }
        }

        public async Task<TodoTask> GetTodoTaskByIdAsync(int todoTaskId)
        {
            return await _context.TodoTasks.FirstOrDefaultAsync(x => x.Id == todoTaskId);
        }

        public async Task<List<TodoTask>> GetTodoTasksForUserAsync(string userId)
        {
            return await _context.TodoTasks.Where(x => x.UserId==userId).ToListAsync();
        }

        public async Task<List<TodoTask>> GetTodoTasksForUserWithinDateRangeAsync(DateTime start, DateTime end, string userId)
        {
            try
            {
                return await _context.TodoTasks.Where(x => x.UserId == userId && x.TaskDate <= end && x.TaskDate >= start).ToListAsync();
            }
            catch(Exception ex)
            {
                return new List<TodoTask>();
            }
        }

        public async Task<bool> MarkTodoTaskAsFinishedAsync(int todoTaskId)
        {
            try
            {
                var task = await _context.TodoTasks.FirstOrDefaultAsync(x => x.Id == todoTaskId);

                if (task != null)
                {
                    task.Completed = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "TodoTaskRepository/CreateTodoTaskAsync"
                });
                await _context.SaveChangesAsync();

                throw;
            }
        }
    }
}
