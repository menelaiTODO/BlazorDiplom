using FuzzyDataDbCore.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuzzyDataDbCore.Models
{
    /// <summary>
    /// Таблица, отображающая связь 1-к-м между нечеткой лингвистической переменной и точками на графике её функции принадлежности
    /// </summary>
    public class Point : BaseModel
    {
        public int CustomLinguisticVariableId { get; set; }

        [Column("x_value")]
        public double XValue { get; set; }

        [Column("y_value")]
        public double YValue { get; set; }  
        
        public int PointSeq { get; set; }

        public CustomLinguisticVariable? CustomLinguisticVariable { get; set; }
    }
}
