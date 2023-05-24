using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeltonikaReports
{
    public static class ChartRenderer
    {
        public static void HorizontalChart(List<(string label, int value)> chartData)
        {
            const string labelSeparator = "|";
            var maxPlainLabelLength = chartData.Select(x => x.label.Length).Max();
            var highestValue = chartData.Select(data => data.value).Max();
            var maxWidthForGraphCells = Console.WindowWidth - labelSeparator.Length * 2 - maxPlainLabelLength - highestValue.ToString().Length;
            
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
                Console.Write($"|{data.value}");
                Console.WriteLine();
            }
        }

        public static void VerticalChart(List<(string label, int value)> chartData)
        {
            var maxHeight = Console.WindowHeight - 2; // -1 offset for data labels, -1 for title,
                                                        // -2 for accounting for process finish lines (if any);
            var highestValue = chartData.Select(data => data.value).Max();
            
            var singleCellSize = Math.Ceiling((float)highestValue / (float)maxHeight);

            var valuesConvertedToRows = chartData.Select(x => (x.label, x.value / (int)singleCellSize));

            var graph = new StringBuilder();

            graph.Append($"Highest value: {highestValue}".PadLeft(chartData.Count*3));
            for (int row = maxHeight; row >= 0; row--)
            {
                foreach (var data in valuesConvertedToRows)
                {
                    graph.Append(data.Item2 >= row ? "██ " : "   ");
                }
                graph.AppendLine();
            }
            
            foreach (var data in chartData)
            {
                var formattedLabel = data.label;
                if (formattedLabel.Length < 2)
                    formattedLabel = "0" + formattedLabel;
                graph.Append($"{formattedLabel} ");
            }
            
            Console.WriteLine(graph);
        }
    }
}