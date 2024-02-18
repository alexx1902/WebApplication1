namespace WebApplication1.DB
{

    public class ValueCsvModel
    {
        
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime DateTime { get; set; }
        public int TimeInSeconds { get; set; }
        public decimal IndicatorValue { get; set; }
    }
}
