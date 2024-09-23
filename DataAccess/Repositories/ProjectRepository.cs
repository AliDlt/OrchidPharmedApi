using Microsoft.EntityFrameworkCore;
using OrchidPharmedApi.Core.Interfaces;
using OrchidPharmedApi.DataAccess.DataContext;
using OrchidPharmedApi.Entities;

namespace OrchidPharmedApi.DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectEntity>> GetProjectsAsync()
        {
            return await _context.Projects.Include(p => p.TaskEntities).ToListAsync();
        }

        public async Task<ProjectEntity> GetProjectByIdAsync(int id)
        {
            return await _context.Projects.Include(p => p.TaskEntities).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProjectEntity> CreateProjectAsync(ProjectEntity project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task DeleteProjectAsync(ProjectEntity project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
