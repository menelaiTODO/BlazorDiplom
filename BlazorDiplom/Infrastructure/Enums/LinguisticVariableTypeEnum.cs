using System.ComponentModel;

namespace BlazorDiplom.Infrastructure.Enums
{
    /// <summary>
    /// Тип лингвистической переменной
    /// </summary>
    public enum LinguisticVariableTypeEnum
    {
        /// <summary>
        /// Одиночная лингвистическая переменная
        /// </summary>
        [Description("Одиночная лингвистическая переменная")]
        SingletoneLinguisticVariable = 1,

        /// <summary>
        /// Составная лингвистическая переменная
        /// </summary>
        [Description("Составная лингвистическая переменная")]
        MultiplyLunguisticVariable
    }
}
