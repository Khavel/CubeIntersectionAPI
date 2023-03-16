using CubeIntersectionAPI.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeIntersectionAPI.Application.Services.Interfaces
{
    public interface ICubeIntersectionService
    {
        void AddCube(CubeDTO cubeDTO);
        void UpdateCube(CubeDTO cubeDTO);
        void DeleteCube(CubeDTO cubeDTO);
        CubeDTO GetCubeById(int id);
        IEnumerable<CubeDTO> GetAllCubes();
        double CalculateIntersectionVolume(int cubeId1, int cubeId2);
    }
}
