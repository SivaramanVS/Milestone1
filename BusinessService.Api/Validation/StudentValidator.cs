using BusinessService.Data.DBModel;
using FluentValidation;

namespace BusinessService.Api.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class StudentValidator : AbstractValidator<Student>
    {
        /// <summary>
        /// 
        /// </summary>
        public StudentValidator()
        {
            //RuleFor(x => x.).NotNull();
            RuleFor(x => x.Name).NotEmpty().WithMessage(x => "name cannot be empty");
            RuleFor(x => x.Name).Length(0, 50).WithMessage(x => $"name {x.Name} exceeds the max length.");
        }
    }
}