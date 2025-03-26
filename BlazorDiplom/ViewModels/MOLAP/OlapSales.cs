using BlazorDiplom.ViewModels.MOLAP.Base;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlazorDiplom.ViewModels
{
    public class OlapSales : IMolapItem
    {
        [Column("[Dim Dates].[Month Name].[Month Name].[MEMBER_CAPTION]")]
        public string MonthName { get; set; } = string.Empty;

        [Column("[Measures].[Sum]")]
        public double Sum { get; set; }

        [Column("[Measures].[Число Fact Sales]")]
        public double SalesCount { get; set; }

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

        public Collection<(string fuzzyKey, double fuzzyResult)> FuzzyResults { get; set; } = new Collection<(string fuzzyKey, double fuzzyResult)>();
    }
}
