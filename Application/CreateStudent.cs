using Application;
using Application.Repositories;
using Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class CreateStudent : IRequest<StudentResponse>
    {
        [Required]
        [StringLength(Student.NameMaxLength)]
        public string Name { get; set; }

        [GreaterOrEqualsThan(Student.MinAge)]
        public int Age { get; set; }
    }

    public sealed class CreateStudentHandler : IRequestHandler<CreateStudent, StudentResponse>
    {
        private readonly IStudentsRepository studentsRepository;
        public CreateStudentHandler(IStudentsRepository studentsRepository)
        {
            this.studentsRepository = studentsRepository;
        }

        public async Task<StudentResponse> Handle(CreateStudent request, CancellationToken cancellationToken)
        {
            var student = Student.Create(request.Name, request.Age);
            await this.studentsRepository.AddAsync(student, cancellationToken);
            await this.studentsRepository.SaveChangesAsync();
            return new StudentResponse { Name = student.Name, Age = student.Age, Id = student.Id };
        }
    }
}
