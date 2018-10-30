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
        //student repository reference
        //private readonly StudentRepository studentRepository;
        public CreateStudentHandler()
        {

        }

        public Task<StudentResponse> Handle(CreateStudent request, CancellationToken cancellationToken)
        {
            var student = Student.Create(request.Name, request.Age);
            // await studentRepository.Add(student, cancellationToken)

            //The use of the task is only because the async call to the repository is not implemented and its necessary to compile.
            return Task.Run(() => new StudentResponse { Name = request.Name, Age = request.Age, Id = 7 });
        }
    }
}
