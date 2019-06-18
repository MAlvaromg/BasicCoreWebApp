using Application.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class GetStudent : IRequest<StudentResponse>
    {
        public int Id { get; set; }

        public GetStudent(int id)
        {
            this.Id = id;
        }
    }

    public sealed class GetStudentHandler : IRequestHandler<GetStudent, StudentResponse>
    {
        private readonly IStudentsRepository studentsRepository;
        public GetStudentHandler(IStudentsRepository studentsRepository)
        {
            this.studentsRepository = studentsRepository;
        }

        public async Task<StudentResponse> Handle(GetStudent request, CancellationToken cancellationToken)
        {
            StudentResponse result = null;
            var student = await this.studentsRepository.FindAsync(request.Id);
            if (student != null)
            {
                result = new StudentResponse { Name = student.Name, Age = student.Age, Id = student.Id };
            }
            return result;
        }
    }
}
