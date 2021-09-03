using ApiApplication.DTO;
using ApiApplication.Helpers.AutoMapper;
using ApiDomain.Entity;
using ApiDomain.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.TodoTasks
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly ITodoTaskRepository _todoTaskRepository;
        private readonly IMapper _mapper;

        public TodoTaskService(ITodoTaskRepository todoTaskRepository,IMapper mapper)
        {
            _todoTaskRepository = todoTaskRepository;
            _mapper = mapper;
        }

        public async Task<TodoTaskDto> AddTodoTaskAsync(CreateTodoTaskDto todoTask)
        {
            try
            {
                var newTodoTask = _mapper.Map<TodoTask>(todoTask);
                return _mapper.Map<TodoTaskDto>(await _todoTaskRepository.CreateTodoTaskAsync(newTodoTask));
            }
            catch
            {
                return null;
            }
        }

        public async Task<TodoTaskDto> GetByIdAsync(int todoTaskId)
        {
            return _mapper.Map<TodoTaskDto>(await _todoTaskRepository.GetTodoTaskByIdAsync(todoTaskId));
        }

        public async Task<List<TodoTaskDto>> GetTodoTasksForUserAsync(string userId)
        {
            return _mapper.Map<List<TodoTaskDto>>(await _todoTaskRepository.GetTodoTasksForUserAsync(userId));
        }

        public async Task<bool> MarkAsCompletedAsync(int todoTaskId)
        {
            try
            {
                return await _todoTaskRepository.MarkTodoTaskAsFinishedAsync(todoTaskId);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveTaskAsync(int todoTaskId)
        {
            try
            {
                return await _todoTaskRepository.RemoveTodoTaskAsync(todoTaskId);
            }
            catch
            {
                return false;
            }
        }
    }
}
