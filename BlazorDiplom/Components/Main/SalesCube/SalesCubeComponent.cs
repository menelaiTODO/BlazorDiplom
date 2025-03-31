using BlazorDiplom.Infrastructure;
using BlazorDiplom.ViewModels;
using FuzzyDataDbCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OLTPDatabaseCore.Jobs;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BlazorDiplom.Components.Main.SalesCube
{
    public class SalesCubeComponent : ComponentBase
    {
        [Inject] 
        IJSRuntime? JsRunTime { get; set; }

        protected IEnumerable<OlapSales> GridData = Enumerable.Empty<OlapSales>();

        protected bool DxChartVisible { get; set; } = true;

        [Inject]
        protected SalesETLJob? SalesETLJob { get; set; }

        [Inject]
        protected OlapHelper? OlapHelper { get; set; }

        /// <summary>
        /// Идентификатор разреза куба
        /// </summary>
        public int CubeSliceId => 1;

        protected bool IsFuzzyQueryConstuctorVisible { get; set; } = false;
       
        protected bool IsFilterPopupVisible { get; set; } = false;

        protected async Task RunSalesEtlJob()
        {
            await JsRunTime!.InvokeVoidAsync("alert", "ETL процесс запущен!"); // Alert

            await SalesETLJob!.RunAsync();

            await JsRunTime!.InvokeVoidAsync("alert", "ETL процесс завершен!"); // Alert
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            GridData = GetSalesDataByMonth();
        }

        protected IEnumerable<OlapSales> GetSalesDataByMonth()
        {
            return OlapHelper!.GetSalesData();
        }

        protected void ApplyFilter() 
        {
            IsFilterPopupVisible = true;
        }

        protected void CreateFuzzyParam()
        {
            IsFuzzyQueryConstuctorVisible = true;
        }

        protected void HandleFilterChanged(object value)
        {
            var variables = ((IEnumerable<CustomLinguisticVariable>)value).Select(item => new CustomLinguisticVariableEx { LinguisticVariable = item});

            GridData = OlapHelper?.GetFuzzySalesData(variables.ToArray())!;

            IsFilterPopupVisible = false;
            
            DxChartVisible = false;
        }
    }
}
