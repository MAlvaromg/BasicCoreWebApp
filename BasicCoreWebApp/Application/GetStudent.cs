using BasicCoreWebApp.DataAccess;
using BasicCoreWebApp.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BasicCoreWebApp.Application
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
        //Demo purpose only, you should not be working directly with the context
        private readonly BasicCoreWebAppDbContext context;
        public GetStudentHandler(BasicCoreWebAppDbContext context)
        {
            this.context = context;
        }

        public async Task<StudentResponse> Handle(GetStudent request, CancellationToken cancellationToken)
        {
            var student = await this.context.Set<Student>().FindAsync(request.Id);
            return new StudentResponse { Name = student.Name, Age = student.Age, Id = student.Id };
        }
    }
}
