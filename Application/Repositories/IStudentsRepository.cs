using Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IStudentsRepository
    {
        Task SaveChangesAsync();
        Task AddAsync(Student student, CancellationToken cancellationToken);
        Task<Student> FindAsync(int id);
    }
}
