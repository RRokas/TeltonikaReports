using System;
using System.Collections.Generic;
using System.Linq;

namespace TeltonikaReports
{
    public static class ChartRenderer
    {
        public static void HorizontalChart(List<(string, int)> chartData)
        {
            var width = Console.WindowWidth;
            var maxNameLength = chartData.Select(x => x.Item1.Length).Max();
            var highestValue = chartData.Select(data => data.Item2).Max();
            var maxWidth = width - maxNameLength-1;
            
            var singleCell = highestValue / maxWidth;

            foreach (var data in chartData)
            {
                var cells = data.Item2 / singleCell;
                var label = data.Item1;
                label = label.PadLeft(maxNameLength);
                
                Console.Write(label+"|");
                for (int j = 0; j < cells; j++)
                {
                    Console.Write("█");
                }
                Console.WriteLine();
            }
        }
    }
}