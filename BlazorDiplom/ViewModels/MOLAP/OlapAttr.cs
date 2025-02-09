using System.ComponentModel;

namespace BlazorDiplom.ViewModels.MOLAP
{
    /// <summary>
    /// Модель, позволяющая достать описание атрибутов из MS SSAS
    /// </summary>
    public class OlapAttr
    {
        [Description("MEASURE_UNIQUE_NAME")]
        public string AttrName { get; set; } = string.Empty;
    }
}
