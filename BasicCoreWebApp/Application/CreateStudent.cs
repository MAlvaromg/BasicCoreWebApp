using BasicCoreWebApp.DataAccess;
using BasicCoreWebApp.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace BasicCoreWebApp.Application
{
    public class CreateStudent : IRequest<StudentResponse>
    {
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public sealed class CreateStudentHandler : IRequestHandler<CreateStudent, StudentResponse>
    {
        //Demo purpose only, you should not be working directly with the context
        private readonly BasicCoreWebAppDbContext context;
        public CreateStudentHandler(BasicCoreWebAppDbContext context)
        {
            this.context = context;
        }

        public async Task<StudentResponse> Handle(CreateStudent request, CancellationToken cancellationToken)
        {
            var student = Student.Create(request.Name, request.Age);
            await this.context.AddAsync(student, cancellationToken);
            await this.context.SaveChangesAsync();
            return new StudentResponse { Name = student.Name, Age = student.Age, Id = student.Id };
        }
    }
}
