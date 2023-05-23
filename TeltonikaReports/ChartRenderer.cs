using System;
using System.Collections.Generic;
using System.Linq;

namespace TeltonikaReports
{
    public static class ChartRenderer
    {
        public static void HorizontalChart(List<(string label, int value)> chartData)
        {
            const string labelSeparator = "|";
            var maxPlainLabelLength = chartData.Select(x => x.label.Length).Max();
            var highestValue = chartData.Select(data => data.value).Max();
            var maxWidthForGraphCells = Console.WindowWidth - labelSeparator.Length - maxPlainLabelLength;
            
            var singleCellSize = Math.Ceiling((float)highestValue / (float)maxWidthForGraphCells);

            foreach (var data in chartData)
            {
                var cellsToDraw = data.value / singleCellSize;
                var label = data.label.PadLeft(maxPlainLabelLength);

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