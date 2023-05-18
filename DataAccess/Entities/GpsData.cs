using System;

namespace DataAccess.Entities
{
    public class GpsData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime GpsTime { get; set; }
        public double Speed { get; set; }
        public float Angle { get; set; }
        public double Altitude { get; set; }
        public int Satellites { get; set; }
    }
}