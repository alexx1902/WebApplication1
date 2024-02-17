namespace WebApplication1.Models
{
    public class ResultModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public TimeSpan TotalTime { get; set; }
        public DateTime StartTime { get; set; }
        public double AverageExecutionTime { get; set; }
        public double AverageIndicatorValue { get; set; }
        public double MedianIndicatorValue { get; set; }
        public decimal MaxIndicatorValue { get; set; }
        public decimal MinIndicatorValue { get; set; }
        public int RowsCount { get; set; }
        public int ValueId { get; set; } 
        public ValueModel Value { get; set; } 
    }

}
