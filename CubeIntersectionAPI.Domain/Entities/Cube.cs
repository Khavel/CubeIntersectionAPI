using CubeIntersectionAPI.Domain.ValueObjects;

namespace CubeIntersectionAPI.Domain.Entities
{
    public class Cube
    {
        public int Id;
        public Point Center;
        public double SideLength;

        public Cube(Point cubeCenter, double length)
        {
            Center = cubeCenter;
            SideLength = length;
        }

        public bool CollidesWith(Cube cube)
        {
            if(Math.Abs(Center.X - cube.Center.X) < SideLength + cube.SideLength)
            {
                if(Math.Abs(Center.Y - cube.Center.Y) < SideLength + cube.SideLength)
                {
                    if(Math.Abs(Center.Y - cube.Center.Y) < SideLength + cube.SideLength)
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
                Math.Max(0, Math.Min(Center.X + (SideLength / 2), cube.Center.X + (cube.SideLength / 2)) - Math.Max(Center.X - (SideLength / 2), cube.Center.X - (cube.SideLength / 2))) *
                Math.Max(0, Math.Min(Center.Y + (SideLength / 2), cube.Center.Y + (cube.SideLength / 2)) - Math.Max(Center.Y - (SideLength / 2), cube.Center.Y - (cube.SideLength / 2))) *
                Math.Max(0, Math.Min(Center.Z + (SideLength / 2), cube.Center.Z + (cube.SideLength / 2)) - Math.Max(Center.Z - (SideLength / 2), cube.Center.Z - (cube.SideLength / 2)));
            }
            return 0.0;
        }
    }
}
