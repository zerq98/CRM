using ApiApplication.DTO;
using ApiDomain.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Helpers.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TodoTask, TodoTaskDto>()
                .ForMember(x => x.TaskRange, opt => opt.MapFrom(c => GetTaskRange(c.TaskDate, c.Completed)));
            CreateMap<CreateTodoTaskDto, TodoTask>()
                .ForMember(x => x.Completed, opt => opt.NullSubstitute(false));
            CreateMap<ProductUpsertDto, ApiDomain.Entity.Product>().ReverseMap();
            CreateMap<ApiDomain.Entity.Product, ProductForListDto>();
        }

        private string GetTaskRange(DateTime TaskDate,bool completed)
        {
            var today = DateTime.Now.Date;
            if(TaskDate.Date<today && !completed)
            {
                return "Overdue";
            }

            if (TaskDate.Date == today)
            {
                return "Today";
            }

            if(TaskDate.Date>=today && TaskDate.Date <= today.Date.AddDays(7))
            {
                return "This week";
            }

            if (TaskDate.Date >= today && TaskDate.Date <= today.Date.AddMonths(1))
            {
                return "This month";
            }

            return "";
        }
    }
}
