using BlazorDiplom.Infrastructure.Enums;
using FuzzyDataDbCore.Models;
using System.ComponentModel.DataAnnotations;

namespace BlazorDiplom.ViewModels
{
    /// <summary>
    /// Модель, отображающая сведения об создаваемой лингвистической переменной
    /// </summary>
    public class SingletoneLinguisticVariable
    {
        public SingletoneLinguisticVariable(FuzzyFunctionData data, CustomLinguisticVariable? dbObj = null)
        {
            Points = dbObj?.Points?.OrderBy(item => item.PointSeq).Select(item => item.XValue).ToArray() ?? new double[data.YValues.Count()];

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
                var defaultPoints =  Points.Zip(FuzzyFunctionData.YValues).ToList();

                if (FuzzyFunctionData.Id == (int)FuzzyFunctionEnum.ZShapedFunctionType1 || FuzzyFunctionData.Id == (int)FuzzyFunctionEnum.ZShapedFunctionType2)
                {
                    if (defaultPoints.Count() != 2)
                        return Enumerable.Empty<(double X, double Y)>();

                    var first = defaultPoints[0];
                    var second = defaultPoints[1];

                    defaultPoints.Insert(0, (first.First - first.First * 0.5, first.Second));
                    defaultPoints.Insert(3, (second.First + second.First * 0.5, second.Second));
                }

                return defaultPoints.AsEnumerable();
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
