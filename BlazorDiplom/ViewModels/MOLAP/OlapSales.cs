using BlazorDiplom.ViewModels.MOLAP.Base;
using System.ComponentModel;
using System.Text;

namespace BlazorDiplom.ViewModels
{
    public class OlapSales : IMolapItem
    {
        [Description("[Dim Dates].[Month Name].[Month Name].[MEMBER_CAPTION]")]
        public string MonthName { get; set; } = string.Empty;

        [Description("[Measures].[Sum]")]
        public double Sum { get; set; }

        [Description("[Measures].[Число Fact Sales]")]
        public double SalesCount { get; set; }

        [Description("Internal")]
        public string GetFuzzyStrData { 
            get 
            {
                var strBuilder = new StringBuilder();

                foreach (var item in FuzzyResults)
                {
                    strBuilder.AppendLine($"{item.fuzzyKey} - {item.fuzzyResult}");
                }

                return strBuilder.ToString();
            } 
        }

        [Description("Internal")]
        public IEnumerable<(string fuzzyKey, double fuzzyResult)> FuzzyResults { get; set; } = Enumerable.Empty<(string fuzzyKey, double fuzzyResult)>();
    }
}
