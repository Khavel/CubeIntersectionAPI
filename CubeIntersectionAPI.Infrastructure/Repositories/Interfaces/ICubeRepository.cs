using CubeIntersectionAPI.Domain.Entities;

namespace CubeIntersectionAPI.Infrastructure.Repositories
{
    public interface ICubeRepository
    {
        List<Cube> GetAll();
        Cube GetById(int id);
        void Add(Cube cube);
        void Update(Cube cube);
        void Delete(Cube cube);
    }
}
