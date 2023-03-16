using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeIntersectionAPI.Application.Models
{
    public class CubeDTO
    {
        public int Id { get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double CenterZ { get; set; }
        public double SideLength { get; set; }
    }
}
