using BlazorDiplom.Infrastructure;
using BlazorDiplom.Infrastructure.Enums;
using BlazorDiplom.ViewModels;
using BlazorDiplom.ViewModels.MOLAP;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorDiplom.Components.Main.FuzzyConstructor
{
    public class FuzzyConstructorComponent : ComponentBase
    {
        [Inject]
        IJSRuntime? JsRunTime { get; set; }

        [Parameter]
        public EventCallback OnSavedCallback { get; set; }

        protected IEnumerable<KeyValuePair<int, string>>? LinguisticVariableType { get; set; }

        protected IEnumerable<FuzzyFunctionData>? FuzzyFunctionDt { get; set; }

        protected int? LinguisticVariableBindedValue { get; set; }

        protected CustomLinguisticVariable? CustomLinguisticVariableData { get; set; }
        
        [Inject]
        protected OlapHelper? OlapHelper { get; set; }

        protected IEnumerable<OlapAttr>? OlapAttrs { get; set; }

        protected FuzzyFunctionData? SelectedFuzzyFunctionData => FuzzyFunctionDt?.FirstOrDefault(item => item.Id == FuzzyFunctionBindedValue);

        protected int? FuzzyFunctionBindedValue { get; set; }

        public void OnLinguisticChanged(KeyValuePair<int, string> t)
        {
            LinguisticVariableBindedValue = t.Key;

            switch ((LinguisticVariableTypeEnum)LinguisticVariableBindedValue)
            {
                case LinguisticVariableTypeEnum.SingletoneLinguisticVariable:
                    FuzzyFunctionDt = FuzzyFunctionData.BuildDataSource();
                    break;
            }
        }

        protected void SelectedFuzzyFunctionChanged(ChangeEventArgs args)
        {
            FuzzyFunctionBindedValue = Convert.ToInt32(args.Value);

            if (SelectedFuzzyFunctionData != null)
            {
                OlapAttrs = OlapHelper?.GetAttrDescription();

                CustomLinguisticVariableData = new CustomLinguisticVariable(SelectedFuzzyFunctionData)
                {
                    FuzzyFunctionData = SelectedFuzzyFunctionData
                };
            }
        }

        protected async Task SaveChanges(MouseEventArgs eventArgs)
        {
            await JsRunTime!.InvokeVoidAsync("alert", "Лингвистическая переменная создана"); // Alert
            await OnSavedCallback.InvokeAsync();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            LinguisticVariableType = DataHelper.GetDatasoureByEnum<LinguisticVariableTypeEnum>(false);
        }
    }
}
