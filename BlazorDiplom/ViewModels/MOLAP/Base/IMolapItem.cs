namespace BlazorDiplom.ViewModels.MOLAP.Base
{
    public interface IMolapItem
    {
        public IEnumerable<(string fuzzyKey, double fuzzyResult)> FuzzyResults { get; set; }
    }
}
