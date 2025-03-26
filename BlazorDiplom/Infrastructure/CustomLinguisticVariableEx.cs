using BlazorDiplom.ViewModels;
using FuzzyDataDbCore.Models;

namespace BlazorDiplom.Infrastructure
{
    /// <summary>
    /// Расширенный класс, содержащий БД данные по лингвистической переменной
    /// и серверные настройки функции принадлежности
    /// </summary>
    public class CustomLinguisticVariableEx
    {
        /// <summary>
        /// БД данные по нечеткой лингвистической переменной
        /// </summary>
        public CustomLinguisticVariable? LinguisticVariable { get; set; }

        /// <summary>
        /// Серверные настройки функции принадлежности
        /// </summary>
        public FuzzyFunctionData? FuzzyFunctionData => FuzzyFunctionData.BuildDataSource().Where(item => item.Id == LinguisticVariable?.FuncId).First();
    }
}
