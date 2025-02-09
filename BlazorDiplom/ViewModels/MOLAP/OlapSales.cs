using System.ComponentModel;

namespace BlazorDiplom.ViewModels
{
    public class OlapSales
    {
        [Description("[Dim Dates].[Month Name].[Month Name].[MEMBER_CAPTION]")]
        public string MonthName { get; set; } = string.Empty;

        [Description("[Measures].[Sum]")]
        public double Sum { get; set; }

        [Description("[Measures].[Число Fact Sales]")]
        public double SalesCount { get; set; }
    }
}
