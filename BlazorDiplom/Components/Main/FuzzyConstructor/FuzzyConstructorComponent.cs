using BlazorDiplom.Infrastructure;
using BlazorDiplom.Infrastructure.Enums;
using BlazorDiplom.ViewModels;
using Microsoft.AspNetCore.Components;

namespace BlazorDiplom.Components.Main.FuzzyConstructor
{
    public class FuzzyConstructorComponent : ComponentBase
    {
        protected IEnumerable<KeyValuePair<int, string>>? LinguisticVariableType { get; set; }

        protected IEnumerable<FuzzyFunctionData>? FuzzyFunctionDt { get; set; }

        protected int? LinguisticVariableBindedValue { get; set; }

        protected FuzzyFunctionData? SelectedFuzzyFunctionData => FuzzyFunctionDt?.FirstOrDefault(item => item.Id == FuzzyFunctionBindedValue);

        protected string Test { get; set; }

        protected int? FuzzyFunctionBindedValue { get; set; }

        public void OnLinguisticChanged(KeyValuePair<int , string> t)
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
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            LinguisticVariableType = DataHelper.GetDatasoureByEnum<LinguisticVariableTypeEnum>(false);
        }
    }
}
