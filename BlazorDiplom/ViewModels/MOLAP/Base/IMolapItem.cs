using System.Collections.ObjectModel;

namespace BlazorDiplom.ViewModels.MOLAP.Base
{
    public interface IMolapItem
    {
        public Collection<(string fuzzyKey, double fuzzyResult)> FuzzyResults { get; set; }
    }
}
