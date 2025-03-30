using BlazorDiplom.Infrastructure;
using BlazorDiplom.Infrastructure.Enums;
using BlazorDiplom.ViewModels;
using BlazorDiplom.ViewModels.MOLAP;
using FuzzyDataDbCore.DatabaseContext;
using FuzzyDataDbCore.Models;
using FuzzyDataDbCore.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace BlazorDiplom.Components.Main.FuzzyConstructor
{
    public class FuzzyConstructorComponent : ComponentBase
    {
        [Inject]
        protected AuthenticationStateProvider? AuthProvider { get; set; }

        [Inject]
        private IJSRuntime? JsRunTime { get; set; }

        [Inject]
        private FuzzyDataDbContext? FuzzyDataDbContext { get; set; }

        /// <summary>
        /// Идентификатор разреза куба
        /// </summary>
        [Parameter]
        public int SliceId { get; set; }

        [Parameter]
        public EventCallback OnSavedCallback { get; set; }

        protected IEnumerable<KeyValuePair<int, string>>? LinguisticVariableType { get; set; }

        protected IEnumerable<FuzzyFunctionData>? FuzzyFunctionDt { get; set; }

        protected int? LinguisticVariableBindedValue { get; set; }

        protected IEnumerable<CustomLinguisticVariable>? GridDataForMultiplyVariable { get; set; }

        protected MultiplyLinguisticVariable? MultiplyLinguisticVariableData { get; set; }

        protected SingletoneLinguisticVariable? SingletoneLinguisticVariableData { get; set; }

        [Inject]
        protected OlapHelper? OlapHelper { get; set; }

        protected IEnumerable<OlapAttr>? OlapAttrs { get; set; }

        protected FuzzyFunctionData? SelectedFuzzyFunctionData => FuzzyFunctionDt?.FirstOrDefault(item => item.Id == FuzzyFunctionBindedValue);

        protected int? FuzzyFunctionBindedValue { get; set; }

        public void OnMultipyVariableOlapAttrChanged(OlapAttr attr)
        {
            GridDataForMultiplyVariable = FuzzyDataDbContext?.CustomLinguisticVariables
                .Include(item => item.Points)
                .Where(item => item.CubeSliceId == SliceId)
                .Where(item => item.MeasureName == attr.AttrName)
                .Select(item => item);
        }

        public void OnLinguisticChanged(KeyValuePair<int, string> t)
        {
            LinguisticVariableBindedValue = t.Key;

            switch ((LinguisticVariableTypeEnum)LinguisticVariableBindedValue)
            {
                case LinguisticVariableTypeEnum.SingletoneLinguisticVariable:
                    FuzzyFunctionDt = FuzzyFunctionData.BuildDataSource();
                    break;

                case LinguisticVariableTypeEnum.MultiplyLunguisticVariable:
                    MultiplyLinguisticVariableData = new();
                    OlapAttrs = OlapHelper?.GetAttrDescription();
                    break;
            }
        }

        protected void SelectedFuzzyFunctionChanged(ChangeEventArgs args)
        {
            FuzzyFunctionBindedValue = Convert.ToInt32(args.Value);

            if (SelectedFuzzyFunctionData != null)
            {
                OlapAttrs = OlapHelper?.GetAttrDescription();

                SingletoneLinguisticVariableData = new SingletoneLinguisticVariable(SelectedFuzzyFunctionData)
                {
                    FuzzyFunctionData = SelectedFuzzyFunctionData
                };
            }
        }

        protected async Task SaveSingleVariableChanges(MouseEventArgs eventArgs)
        {
            var user = (await AuthProvider!.GetAuthenticationStateAsync()).User;
            var userStringId = user.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value;

            var dbObjVar = new CustomLinguisticVariable
            {
                CreatedDate = DateTime.Now,
                CreatorName = userStringId ?? string.Empty,
                Name = SingletoneLinguisticVariableData!.Name,
                MinIndex = SingletoneLinguisticVariableData.MinIndex,
                CubeSliceId = SliceId,
                FuncId = SingletoneLinguisticVariableData.FuzzyFunctionData.Id,
                MeasureName = SingletoneLinguisticVariableData.MOLAPItemName
            };

            new CustomLinguisticVariableRepository(FuzzyDataDbContext!).SaveCustomLinguisticVariable(dbObjVar, SingletoneLinguisticVariableData.PointsForChart);

            await JsRunTime!.InvokeVoidAsync("alert", "Лингвистическая переменная создана"); // Alert
            await OnSavedCallback.InvokeAsync();
        }

        protected async Task SaveMultiplyVariableChanges(MouseEventArgs eventArgs)
        {
            var user = (await AuthProvider!.GetAuthenticationStateAsync()).User;
            var userStringId = user.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value;

            var dbObjVar = new CustomMultiplyLinguisticVariable
            {
                CreatedDate = DateTime.Now,
                CreatorName = userStringId ?? string.Empty,
                Name = MultiplyLinguisticVariableData!.Name,
                CustomLinguisticVariables = MultiplyLinguisticVariableData?.SingletoneLinguisticVariables.ToList(),
                CubeSliceId = SliceId,
                MeasureName = MultiplyLinguisticVariableData!.MeasureName
            };

            new CustomLinguisticVariableRepository(FuzzyDataDbContext!).SaveCustomMultiplyLinguisticVariable(dbObjVar);

            await JsRunTime!.InvokeVoidAsync("alert", "Составная лингвистическая переменная создана"); // Alert
            await OnSavedCallback.InvokeAsync();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            LinguisticVariableType = DataHelper.GetDatasoureByEnum<LinguisticVariableTypeEnum>(false);
        }
    }
}
