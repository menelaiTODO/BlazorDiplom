using System.ComponentModel.DataAnnotations;

namespace BlazorDiplom.ViewModels
{
    /// <summary>
    /// Модель, отображающая сведения об создаваемой лингвистической переменной
    /// </summary>
    public class SingletoneLinguisticVariable
    {
        public SingletoneLinguisticVariable(FuzzyFunctionData data)
        {
            Points = new double[data.YValues.Count()];

            FuzzyFunctionData = data;
        }

        /// <summary>
        /// Наименование лингвистической переменной
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Минимальный порог соответствия
        /// </summary>
        [Range(0, 1)]
        public double MinIndex { get; set; }

        /// <summary>
        /// Точки графика
        /// </summary>
        public double[] Points { get; set; }

        /// <summary>
        /// Точки графика функции принадлежности
        /// </summary>
        public IEnumerable<(double X, double Y)> PointsForChart 
        { 
            get 
            {
                return Points.Zip(FuzzyFunctionData.YValues);
            } 
        }

        /// <summary>
        /// Мера или измерение, к которой относится данная лингвистическая переменная
        /// </summary>
        public string MOLAPItemName { get; set; } = string.Empty;

        /// <summary>
        /// Сведения о функции принадлежности
        /// </summary>
        public FuzzyFunctionData FuzzyFunctionData { get; set;}
    }
}
