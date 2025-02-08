using System.ComponentModel;

namespace BlazorDiplom.Infrastructure.Enums
{
    /// <summary>
    /// Перечисление функций принадлежности
    /// </summary>
    public enum FuzzyFunctionEnum
    {
        /// <summary>
        /// Треугольная функция принадлежности
        /// </summary>
        [Description("Треугольная функция принадлежности")]
        TriangularFunction = 0,

        /// <summary>
        /// Трапециевидная функция принадлежности
        /// </summary>
        [Description("Трапециевидная функция принадлежности")]
        TrapezoidalFunction,

        /// <summary>
        /// Z-образная функция принадлежности (Тип 1)
        /// </summary>
        
        [Description("Z-образная функция принадлежности (Тип 1)")]
        ZShapedFunctionType1,

        /// <summary>
        /// Z-образная функция принадлежности (Тип 2)
        /// </summary>
        [Description("Z-образная функция принадлежности (Тип 2)")]
        ZShapedFunctionType2,

        /// <summary>
        /// Сплайн функция (Тип 1)
        /// </summary>
        [Description("Сплайн функция (Тип 1)")]
        SplineFunctionType1,

        /// <summary>
        /// Сплайн функция (Тип 2)
        /// </summary>
        [Description("Сплайн функция (Тип 2)")]
        SplineFunctionType2,

        /// <summary>
        /// П-образная функция принадлежности
        /// </summary>
        [Description("П-образная функция принадлежности")]
        PShapedFunction
    }
}
