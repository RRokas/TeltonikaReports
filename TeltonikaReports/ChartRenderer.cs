using System;
using System.Collections.Generic;
using System.Linq;

namespace TeltonikaReports
{
    public static class ChartRenderer
    {
        public static void HorizontalChart(List<(string, int)> chartData)
        {
            const string labelSeparator = "|";
            var maxPlainLabelLength = chartData.Select(x => x.Item1.Length).Max();
            var highestValue = chartData.Select(data => data.Item2).Max();
            var maxWidthForGraphCells = Console.WindowWidth - labelSeparator.Length - maxPlainLabelLength;
            
            var singleCellSize = Math.Ceiling((float)highestValue / (float)maxWidthForGraphCells);

            foreach (var data in chartData)
            {
                var cellsToDraw = data.Item2 / singleCellSize;
                var label = data.Item1.PadLeft(maxPlainLabelLength);

                Console.Write(label+labelSeparator);
                for (int drawnCells = 0; drawnCells < cellsToDraw; drawnCells++)
                {
                    Console.Write("█");
                }
                Console.WriteLine();
            }
        }
    }
}