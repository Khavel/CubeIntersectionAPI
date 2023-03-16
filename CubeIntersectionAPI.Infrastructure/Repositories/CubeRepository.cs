using CubeIntersectionAPI.Domain.Entities;
using CubeIntersectionAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CubeIntersectionAPI.Infrastructure.Repositories
{
    public class CubeRepository : ICubeRepository
    {
        private readonly CubeIntersectionContext _context;

        public CubeRepository(CubeIntersectionContext context)
        {
            _context = context;
        }

        public List<Cube> GetAll()
        {
            return _context.Cubes.ToList();
        }

        public Cube GetById(int id)
        {
            return _context.Cubes.Find(id);
        }

        public void Add(Cube cube)
        {
            _context.Cubes.Add(cube);
            _context.SaveChanges();
        }

        public void Update(Cube cube)
        {
            _context.Entry(cube).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Cube cube)
        {
            _context.Cubes.Remove(cube);
            _context.SaveChanges();
        }
    }
}
