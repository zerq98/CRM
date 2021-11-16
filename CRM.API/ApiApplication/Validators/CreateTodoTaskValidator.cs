using ApiApplication.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Validators
{
    public class CreateTodoTaskValidator : AbstractValidator<CreateTodoTaskDto>
    {
        public CreateTodoTaskValidator()
        {
            RuleFor(x => x.TaskDate).NotNull().WithMessage("Data zadania nie może być pusta.").Must(x=>x.Date>=DateTime.Now.Date).WithMessage("Można dodać tylko zadania przyszłe lub dzisiejsze");
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Zadanie musi posiadać tytuł.");
        }
    }
}
