using Application.Repositories;
using Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly BasicCoreWebAppDbContext context;
        public StudentsRepository(BasicCoreWebAppDbContext context)
        {
            this.context = context;
        }

        public Task AddAsync(Student student, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Student> FindAsync(int id)
        {
            return this.context.Set<Student>().FindAsync(id);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
