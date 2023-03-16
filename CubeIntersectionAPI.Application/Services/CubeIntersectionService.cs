using CubeIntersectionAPI.Application.Models;
using CubeIntersectionAPI.Application.Services.Interfaces;
using CubeIntersectionAPI.Domain.Entities;
using CubeIntersectionAPI.Domain.ValueObjects;
using CubeIntersectionAPI.Infrastructure.Repositories;

namespace CubeIntersectionAPI.Application.Services
{
    public class CubeIntersectionService : ICubeIntersectionService
    {
        private readonly ICubeRepository _cubeRepository;

        public CubeIntersectionService(ICubeRepository cubeRepository)
        {
            _cubeRepository = cubeRepository;
        }

        public void AddCube(CubeDTO cubeDTO)
        {
            var cube = new Cube(new Point(cubeDTO.CenterX, cubeDTO.CenterY, cubeDTO.CenterZ), cubeDTO.SideLength);
            _cubeRepository.Add(cube);
        }

        public void UpdateCube(CubeDTO cubeDTO)
        {
            var cube = _cubeRepository.GetById(cubeDTO.Id);
            if(cube != null)
            {
                cube.Center = new Point(cubeDTO.CenterX, cubeDTO.CenterY, cubeDTO.CenterZ);
                cube.SideLength = cubeDTO.SideLength;
                _cubeRepository.Update(cube);
            }
        }

        public void DeleteCube(CubeDTO cubeDTO)
        {
            var cube = _cubeRepository.GetById(cubeDTO.Id);
            if(cube != null)
            {
                _cubeRepository.Delete(cube);
            }
        }

        public CubeDTO GetCubeById(int id)
        {
            var cube = _cubeRepository.GetById(id);
            if(cube != null)
            {
                return new CubeDTO
                {
                    Id = cube.Id,
                    CenterX = cube.Center.X,
                    CenterY = cube.Center.Y,
                    CenterZ = cube.Center.Z,
                    SideLength = cube.SideLength
                };
            }
            return null;
        }

        public IEnumerable<CubeDTO> GetAllCubes()
        {
            var cubes = _cubeRepository.GetAll();
            var cubeDTOs = new List<CubeDTO>();
            foreach(var cube in cubes)
            {
                cubeDTOs.Add(new CubeDTO
                {
                    Id = cube.Id,
                    CenterX = cube.Center.X,
                    CenterY = cube.Center.Y,
                    CenterZ = cube.Center.Z,
                    SideLength = cube.SideLength
                });
            }
            return cubeDTOs;
        }

        public double CalculateIntersectionVolume(int cubeId1, int cubeId2)
        {
            var cube1 = _cubeRepository.GetById(cubeId1);
            var cube2 = _cubeRepository.GetById(cubeId2);
            if(cube1 != null && cube2 != null)
            {
                return cube1.CalculateIntersectedVolume(cube2);
            }
            return 0.0;
        }
    }
}
