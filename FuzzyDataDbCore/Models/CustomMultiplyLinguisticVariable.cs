using FuzzyDataDbCore.Base;
using System.ComponentModel.DataAnnotations;

namespace FuzzyDataDbCore.Models
{
    /// <summary>
    /// Кастомная составная лингвистическая переменная
    /// </summary>
    public class CustomMultiplyLinguisticVariable : BaseModel
    {
        /// <summary>
        /// Наименование нечеткой лингвистической переменной
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор разреза куба
        /// </summary>
        [Required]
        public int CubeSliceId { get; set; }

        /// <summary>
        /// Наименование меры, к которой может применяться составная лингвистическая переменная
        /// </summary>
        [Required]
        public string MeasureName { get; set; } = string.Empty;

        /// <summary>
        /// Свойство-связь (многие ко многим)
        /// </summary>
        public ICollection<CustomLinguisticVariable>? CustomLinguisticVariables { get; set; }
    }
}
