using System;

namespace DataAccess.Entities
{
    public class GpsData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime GpsTime { get; set; }
        public short Speed { get; set; }
        public short Angle { get; set; }
        public short Altitude { get; set; }
        public byte Satellites { get; set; }
    }
}