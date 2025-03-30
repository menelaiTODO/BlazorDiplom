using FuzzyDataDbCore.Base;
using System.ComponentModel.DataAnnotations;

namespace FuzzyDataDbCore.Models
{
    /// <summary>
    /// Модель, отображающая нечеткую лингвистическую переменную
    /// </summary>
    public class CustomLinguisticVariable : BaseModel
    {
        /// <summary>
        /// Наименование нечеткой лингвистической переменной
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Минимальный индекс соответствия
        /// </summary>
        [Required]
        public double MinIndex { get; set; }

        /// <summary>
        /// Идентификатор разреза куба
        /// </summary>
        [Required]
        public int CubeSliceId { get; set; }

        /// <summary>
        /// Идентификатор функции принадлежности
        /// </summary>
        [Required]
        public int FuncId { get; set; }

        /// <summary>
        /// Мера, к которой относится нечеткая лингвистическая переменная
        /// </summary>
        [Required]
        public string MeasureName { get; set; } = string.Empty;

        /// <summary>
        /// Точки для функции принадлежности 
        /// </summary>
        public ICollection<Point>? Points { get; set; }

        /// <summary>
        /// Свойство-связь (многие ко многим)
        /// </summary>
        public ICollection<CustomMultiplyLinguisticVariable>? CustomMultiplyLinguisticVariables { get; set; }
    }
}
