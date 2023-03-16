using CubeIntersectionAPI.Application.Models;
using CubeIntersectionAPI.Application.Services;
using CubeIntersectionAPI.Application.Services.Interfaces;
using CubeIntersectionAPI.Domain.Entities;
using CubeIntersectionAPI.Domain.ValueObjects;
using CubeIntersectionAPI.Infrastructure.Repositories;
using Moq;

namespace CubeIntersectionAPITest
{
    public class CubeIntersectionServiceTests
    {
        private readonly Mock<ICubeRepository> _mockCubeRepository;
        private readonly ICubeIntersectionService _cubeIntersectionService;

        public CubeIntersectionServiceTests()
        {
            _mockCubeRepository = new Mock<ICubeRepository>();
            _cubeIntersectionService = new CubeIntersectionService(_mockCubeRepository.Object);
        }

        [Fact]
        public void AddCube_ShouldAddCubeToRepository()
        {
            // Arrange
            var cubeDTO = new CubeDTO
            {
                CenterX = 0,
                CenterY = 0,
                CenterZ = 0,
                SideLength = 10
            };

            // Act
            _cubeIntersectionService.AddCube(cubeDTO);

            // Assert
            _mockCubeRepository.Verify(x => x.Add(It.IsAny<Cube>()), Times.Once);
        }

        [Fact]
        public void UpdateCube_WithExistingCubeInRepository()
        {
            // Arrange
            var cubeDTO = new CubeDTO
            {
                Id = 1,
                CenterX = 0,
                CenterY = 0,
                CenterZ = 0,
                SideLength = 10
            };
            var existingCube = new Cube(new Point(0, 0, 0), 5)
            {
                Id = 1
            };
            _mockCubeRepository.Setup(x => x.GetById(cubeDTO.Id)).Returns(existingCube);

            // Act
            _cubeIntersectionService.UpdateCube(cubeDTO);

            // Assert
            _mockCubeRepository.Verify(x => x.Update(It.IsAny<Cube>()), Times.Once);
            Assert.Equal(cubeDTO.CenterX, existingCube.Center.X);
            Assert.Equal(cubeDTO.CenterY, existingCube.Center.Y);
            Assert.Equal(cubeDTO.CenterZ, existingCube.Center.Z);
            Assert.Equal(cubeDTO.SideLength, existingCube.SideLength);
        }

        [Fact]
        public void UpdateCube_WithoutExistingCubeInRepository()
        {
            // Arrange
            var cubeDTO = new CubeDTO
            {
                Id = 1,
                CenterX = 0,
                CenterY = 0,
                CenterZ = 0,
                SideLength = 10
            };
            _mockCubeRepository.Setup(x => x.GetById(cubeDTO.Id)).Returns((Cube)null);

            // Act
            _cubeIntersectionService.UpdateCube(cubeDTO);

            // Assert
            _mockCubeRepository.Verify(x => x.Update(It.IsAny<Cube>()), Times.Never);
        }

        [Fact]
        public void DeleteCube_WithExistingCubeInRepository()
        {
            // Arrange
            var cubeDTO = new CubeDTO { Id = 1, CenterX = 0, CenterY = 0, CenterZ = 0, SideLength = 1 };
            _mockCubeRepository.Setup(repo => repo.GetById(cubeDTO.Id)).Returns(new Cube(new Point(cubeDTO.CenterX, cubeDTO.CenterY, cubeDTO.CenterZ), cubeDTO.SideLength));

            // Act
            _cubeIntersectionService.DeleteCube(cubeDTO);

            // Assert
            _mockCubeRepository.Verify(repo => repo.Delete(It.IsAny<Cube>()), Times.Once);
        }

        [Fact]
        public void DeleteCube_WithoutExistingCubeInRepository()
        {
            // Arrange
            var cubeDTO = new CubeDTO { Id = 1, CenterX = 0, CenterY = 0, CenterZ = 0, SideLength = 1 };
            _mockCubeRepository.Setup(repo => repo.GetById(cubeDTO.Id)).Returns((Cube)null);

            // Act
            _cubeIntersectionService.DeleteCube(cubeDTO);

            // Assert
            _mockCubeRepository.Verify(repo => repo.Delete(It.IsAny<Cube>()), Times.Never);
        }

        [Fact]
        public void GetCubeById_WithValidId_ReturnsCubeDTO()
        {
            // Arrange
            var cube = new Cube(new Point(0, 0, 0), 1);
            cube.Id = 1;
            _mockCubeRepository.Setup(repo => repo.GetById(1)).Returns(cube);

            // Act
            var result = _cubeIntersectionService.GetCubeById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(0, result.CenterX);
            Assert.Equal(0, result.CenterY);
            Assert.Equal(0, result.CenterZ);
            Assert.Equal(1, result.SideLength);
        }

        [Fact]
        public void GetCubeById_WithInvalidId_ReturnsNull()
        {
            // Arrange
            _mockCubeRepository.Setup(repo => repo.GetById(1)).Returns((Cube)null);

            // Act
            var result = _cubeIntersectionService.GetCubeById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAllCubes_ReturnsListOfCubeDTOs()
        {
            // Arrange
            var cubes = new List<Cube>
            {
                new Cube(new Point(0, 0, 0), 1),
                new Cube(new Point(1, 1, 1), 2)
            };
            _mockCubeRepository.Setup(repo => repo.GetAll()).Returns(cubes);

            // Act
            var result = _cubeIntersectionService.GetAllCubes();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(0, result.ElementAt(0).CenterX);
            Assert.Equal(0, result.ElementAt(0).CenterY);
            Assert.Equal(0, result.ElementAt(0).CenterZ);
            Assert.Equal(1, result.ElementAt(0).SideLength);
            Assert.Equal(1, result.ElementAt(1).CenterX);
            Assert.Equal(1, result.ElementAt(1).CenterY);
            Assert.Equal(1, result.ElementAt(1).CenterZ);
            Assert.Equal(2, result.ElementAt(1).SideLength);
        }

        [Fact]
        public void CalculateIntersectionVolume_ReturnsZero_WhenCube1IsNull()
        {
            // Arrange
            Cube cube2 = new Cube(new Point(0, 0, 0), 10);
            _mockCubeRepository.Setup(repo => repo.GetById(1)).Returns((Cube)null);
            _mockCubeRepository.Setup(repo => repo.GetById(2)).Returns(cube2);

            // Act
            double volume = _cubeIntersectionService.CalculateIntersectionVolume(1, 2);

            // Assert
            Assert.Equal(0, volume);
        }

        [Fact]
        public void CalculateIntersectionVolume_ReturnsZero_WhenCube2IsNull()
        {
            // Arrange
            Cube cube1 = new Cube(new Point(0, 0, 0), 10);
            _mockCubeRepository.Setup(repo => repo.GetById(1)).Returns(cube1);
            _mockCubeRepository.Setup(repo => repo.GetById(2)).Returns((Cube)null);

            // Act
            double volume = _cubeIntersectionService.CalculateIntersectionVolume(1, 2);

            // Assert
            Assert.Equal(0, volume);
        }

        [Fact]
        public void CalculateIntersectionVolume_ReturnsZero_WhenCubesDoNotIntersect()
        {
            // Arrange
            Cube cube1 = new Cube(new Point(0, 0, 0), 5);
            Cube cube2 = new Cube(new Point(20, 20, 20), 5);
            _mockCubeRepository.Setup(repo => repo.GetById(1)).Returns(cube1);
            _mockCubeRepository.Setup(repo => repo.GetById(2)).Returns(cube2);

            // Act
            double volume = _cubeIntersectionService.CalculateIntersectionVolume(1, 2);

            // Assert
            Assert.Equal(0, volume);
        }

        [Fact]
        public void CalculateIntersectionVolume_ReturnsCorrectVolume_WhenCubesIntersect()
        {
            // Arrange
            Cube cube1 = new Cube(new Point(0, 0, 0), 10);
            Cube cube2 = new Cube(new Point(5, 5, 5), 10);
            _mockCubeRepository.Setup(repo => repo.GetById(1)).Returns(cube1);
            _mockCubeRepository.Setup(repo => repo.GetById(2)).Returns(cube2);

            // Act
            double volume = _cubeIntersectionService.CalculateIntersectionVolume(1, 2);

            // Assert
            Assert.Equal(125, volume);
        }

    }
}
