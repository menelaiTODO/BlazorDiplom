namespace BlazorDiplom.Components.Main.SalesCube.Models
{
    public class SalesByMonth
    {
        public string MonthName { get; set; } = string.Empty;

        public int SalesCount { get; set; }

        public double SalesSum { get; set; }
    }
}
