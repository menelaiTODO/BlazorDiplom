using FuzzyDataDbCore.Models;

namespace BlazorDiplom.ViewModels
{
    /// <summary>
    /// Составная лингвистическая переменная
    /// </summary>
    public class MultiplyLinguisticVariable
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор разреза куба
        /// </summary>
        public int CubeSliceId { get; set; }

        /// <summary>
        /// Наименование меры, к которой может применяться составная лингвистическая переменная
        /// </summary>
        public string MeasureName { get; set; } = string.Empty;

        /// <summary>
        /// Одиночные лингвистические переменные
        /// </summary>
        public IEnumerable<CustomLinguisticVariable> SingletoneLinguisticVariables { get; set; } = Enumerable.Empty<CustomLinguisticVariable>();
    }
}
