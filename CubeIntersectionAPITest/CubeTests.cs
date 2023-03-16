using CubeIntersectionAPI.Domain.Entities;
using CubeIntersectionAPI.Domain.ValueObjects;

namespace CubeIntersectionAPITest
{
    public class CubeTests
    {
        [Fact]
        public void CollidesWith_ReturnsTrue_WhenCubesOverlap()
        {
            // Arrange
            var center1 = new Point(0, 0, 0);
            var center2 = new Point(1, 1, 1);
            var cube1 = new Cube(center1, 2);
            var cube2 = new Cube(center2, 2);

            // Act
            var result = cube1.CollidesWith(cube2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CollidesWith_ReturnsFalse_WhenCubesDoNotOverlap()
        {
            // Arrange
            var center1 = new Point(0, 0, 0);
            var center2 = new Point(10, 10, 10);
            var cube1 = new Cube(center1, 2);
            var cube2 = new Cube(center2, 2);

            // Act
            var result = cube1.CollidesWith(cube2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CalculateIntersectedVolume_ReturnsExpectedValue_WhenCubesOverlap()
        {
            // Arrange
            var center1 = new Point(0, 0, 0);
            var center2 = new Point(1, 1, 1);
            var cube1 = new Cube(center1, 2);
            var cube2 = new Cube(center2, 2);

            // Act
            var result = cube1.CalculateIntersectedVolume(cube2);

            // Assert
            Assert.Equal(1.0, result);
        }

        [Fact]
        public void CalculateIntersectedVolume_ReturnsZero_WhenCubesDoNotOverlap()
        {
            // Arrange
            var center1 = new Point(0, 0, 0);
            var center2 = new Point(10, 10, 10);
            var cube1 = new Cube(center1, 2);
            var cube2 = new Cube(center2, 2);

            // Act
            var result = cube1.CalculateIntersectedVolume(cube2);

            // Assert
            Assert.Equal(0.0, result);
        }

        [Fact]
        public void CollidesWith_ShouldReturnTrue_WhenCubesOverlap()
        {
            // Arrange
            var cube1 = new Cube(new Point(0, 0, 0), 2);
            var cube2 = new Cube(new Point(1, 1, 1), 2);

            // Act
            var result = cube1.CollidesWith(cube2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CollidesWith_ShouldReturnFalse_WhenCubesDoNotOverlap()
        {
            // Arrange
            var cube1 = new Cube(new Point(0, 0, 0), 2);
            var cube2 = new Cube(new Point(10, 10, 10), 2);

            // Act
            var result = cube1.CollidesWith(cube2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CalculateIntersectedVolume_ShouldReturnZero_WhenCubesDoNotCollide()
        {
            // Arrange
            var cube1 = new Cube(new Point(0, 0, 0), 2);
            var cube2 = new Cube(new Point(10, 10, 10), 2);

            // Act
            var result = cube1.CalculateIntersectedVolume(cube2);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateIntersectedVolume_ShouldReturnCorrectVolume_WhenCubesCollide()
        {
            // Arrange
            var cube1 = new Cube(new Point(0, 0, 0), 2);
            var cube2 = new Cube(new Point(1, 1, 1), 2);

            // Act
            var result = cube1.CalculateIntersectedVolume(cube2);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void CalculateIntersectedVolume_ShouldReturnZero_WhenCubesJustTouch()
        {
            // Arrange
            var cube1 = new Cube(new Point(0, 0, 0), 2);
            var cube2 = new Cube(new Point(2, 0, 0), 2);

            // Act
            var result = cube1.CalculateIntersectedVolume(cube2);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateIntersectedVolume_ShouldReturnCorrectVolume_WhenCubesOverlapPartially()
        {
            // Arrange
            var cube1 = new Cube(new Point(0, 0, 0), 2);
            var cube2 = new Cube(new Point(1, 1, 1), 2);

            // Act
            var result = cube1.CalculateIntersectedVolume(cube2);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void CollidesWith_ReturnsTrue_WhenCubesIntersect()
        {
            // Arrange
            var cube1 = new Cube(new Point(0, 0, 0), 2);
            var cube2 = new Cube(new Point(1, 1, 1), 2);

            // Act
            var result = cube1.CollidesWith(cube2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CollidesWith_ReturnsFalse_WhenCubesDoNotIntersect()
        {
            // Arrange
            var cube1 = new Cube(new Point(0, 0, 0), 2);
            var cube2 = new Cube(new Point(10, 10, 10), 2);

            // Act
            var result = cube1.CollidesWith(cube2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CalculateIntersectedVolume_ReturnsZero_WhenCubesDoNotIntersect()
        {
            // Arrange
            var cube1 = new Cube(new Point(0, 0, 0), 2);
            var cube2 = new Cube(new Point(10, 10, 10), 2);

            // Act
            var result = cube1.CalculateIntersectedVolume(cube2);

            // Assert
            Assert.Equal(0.0, result, precision: 5);
        }

        [Fact]
        public void CalculateIntersectedVolume_ReturnsCorrectVolume_WhenCubesIntersect()
        {
            // Arrange
            var cube1 = new Cube(new Point(0, 0, 0), 2);
            var cube2 = new Cube(new Point(1, 1, 1), 2);

            // Act
            var result = cube1.CalculateIntersectedVolume(cube2);

            // Assert
            Assert.Equal(1.0, result, precision: 5);
        }

        [Fact]
        public void CalculateIntersectedVolume_ReturnsZero_WhenCubesAreTouchingButDoNotIntersect()
        {
            // Arrange
            var cube1 = new Cube(new Point(0, 0, 0), 2);
            var cube2 = new Cube(new Point(3, 0, 0), 2);

            // Act
            var result = cube1.CalculateIntersectedVolume(cube2);

            // Assert
            Assert.Equal(0.0, result, precision: 5);
        }
    }
}