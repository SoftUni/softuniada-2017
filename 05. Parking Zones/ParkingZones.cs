using System;
using System.Collections.Generic;

namespace _06ParkingZones
{
    public class ParkingSpace : IComparable<ParkingSpace>
    {
        public ParkingSpace(uint x, uint y, uint targetX, uint targetY)
        {
            this.X = x;
            this.Y = y;
            this.DistanceFromTarget = CalculateDistanceFromTarget(x, y, targetX, targetY);
        }

        public uint X { get; set; }
        public uint Y { get; set; }
        public uint DistanceFromTarget { get; set; }

        private uint CalculateDistanceFromTarget(uint x, uint y, uint targetX, uint targetY)
        {
            uint distance = (Math.Max(x, targetX) - Math.Min(x, targetX)) + (Math.Max(y, targetY) - Math.Min(y, targetY)) - 1;
            return distance;
        }

        public int CompareTo(ParkingSpace other)
        {
            return this.DistanceFromTarget.CompareTo(other.DistanceFromTarget);
        }
    }
    public class Zone
    {
        public Zone(string name, uint x, uint y, uint width, uint height, double price)
        {
            this.Name = name;
            this.MinX = x;
            this.MinY = y;
            this.MaxX = x + width;
            this.MaxY = y + height;
            this.PricePerMin = price;
        }

        public string Name { get; set; }

        public uint MinX { get; set; }

        public uint MaxX { get; set; }

        public uint MinY { get; set; }

        public uint MaxY { get; set; }

        public double PricePerMin { get; set; }

        public ParkingSpace BestParkingSpace { get; set; }

        public void AddParkingSpace(ParkingSpace parkingSpace)
        {
            if (this.BestParkingSpace == null ||
                parkingSpace.DistanceFromTarget < this.BestParkingSpace.DistanceFromTarget)
            {
                this.BestParkingSpace = parkingSpace;
            }
        }
    }
    public class ParkingZones
    {
        public static void Main()
        {
            int zonesCount = int.Parse(Console.ReadLine());
            List<Zone> zones = new List<Zone>();
            for (int i = 0; i < zonesCount; i++)
            {
                string[] tokens = Console.ReadLine().Split(':');
                string name = tokens[0];
                string[] parameters = tokens[1].Split(',');
                uint x = uint.Parse(parameters[0]);
                uint y = uint.Parse(parameters[1]);
                uint width = uint.Parse(parameters[2]);
                uint height = uint.Parse(parameters[3]);
                double price = double.Parse(parameters[4]);
                zones.Add(new Zone(name, x, y, width, height, price));
            }

            string[] parkSpaces = Console.ReadLine().Split(';');

            string[] targetTokens = Console.ReadLine().Split(',');
            uint targetX = uint.Parse(targetTokens[0]);
            uint targetY = uint.Parse(targetTokens[1]);

            uint timeConstant = uint.Parse(Console.ReadLine());

            for (int i = 0; i < parkSpaces.Length; i++)
            {
                string[] tokens = parkSpaces[i].Split(',');
                uint x = uint.Parse(tokens[0]);
                uint y = uint.Parse(tokens[1]);
                ParkingSpace space = new ParkingSpace(x, y, targetX, targetY);
                for (int j = 0; j < zones.Count; j++)
                {
                    if (x >= zones[j].MinX && x < zones[j].MaxX && y >= zones[j].MinY && y < zones[j].MaxY)
                    {
                        zones[j].AddParkingSpace(space);
                        break;
                    }
                }
            }

            ParkingSpace bestParkingSpace = null;
            decimal totalPrice = Decimal.MaxValue;
            uint totalTime = int.MaxValue;
            string zoneName = string.Empty;

            for (int i = 0; i < zones.Count; i++)
            {
                ParkingSpace currentBest = zones[i].BestParkingSpace;
                if (currentBest != null)
                {
                    uint currentTime = currentBest.DistanceFromTarget*2*timeConstant;
                    uint timeInMins = currentTime%60 == 0 ? currentTime/60 : currentTime/60 + 1;
                    decimal currentPrice = timeInMins*(decimal)zones[i].PricePerMin;
                    if (currentPrice < totalPrice || (currentPrice == totalPrice && currentTime < totalTime))
                    {
                        totalTime = currentTime;
                        totalPrice = currentPrice;
                        bestParkingSpace = currentBest;
                        zoneName = zones[i].Name;
                    }
                }
            }

            Console.WriteLine("Zone Type: {0}; X: {1}; Y: {2}; Price: {3:F2}", zoneName, bestParkingSpace.X, bestParkingSpace.Y, totalPrice);
        }
    }
}
