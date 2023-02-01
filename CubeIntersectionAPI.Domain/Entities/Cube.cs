using CubeIntersectionAPI.Domain.ValueObjects;

namespace CubeIntersectionAPI.Domain.Entities
{
    public class Cube
    {
        public int Id;
        public Point center;
        public double sideLength;

        public Cube(Point cubeCenter, double length)
        {
            center = cubeCenter;
            sideLength = length;
        }

        public bool CollidesWith(Cube cube)
        {
            if (Math.Abs(center.X - cube.center.X) < sideLength + cube.sideLength)
            {
                if (Math.Abs(center.Y - cube.center.Y) < sideLength + cube.sideLength)
                {
                    if (Math.Abs(center.Y - cube.center.Y) < sideLength + cube.sideLength)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public double CalculateIntersectedVolume(Cube cube)
        {
            if(this.CollidesWith(cube))
            {
                return 
                Math.Max(0, Math.Min(center.X + (sideLength / 2), cube.center.X + (cube.sideLength / 2)) - Math.Max(center.X - (sideLength / 2), cube.center.X - (cube.sideLength / 2))) *
                Math.Max(0, Math.Min(center.Y + (sideLength / 2), cube.center.Y + (cube.sideLength / 2)) - Math.Max(center.Y - (sideLength / 2), cube.center.Y - (cube.sideLength / 2))) *
                Math.Max(0, Math.Min(center.Z + (sideLength / 2), cube.center.Z + (cube.sideLength / 2)) - Math.Max(center.Z - (sideLength / 2), cube.center.Z - (cube.sideLength / 2)));
            }
            return 0.0;
        }
    }
}
