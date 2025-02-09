using BlazorDiplom.Infrastructure;
using BlazorDiplom.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OLTPDatabaseCore.Jobs;

namespace BlazorDiplom.Components.Main.SalesCube
{
    public class SalesCubeComponent : ComponentBase
    {
        [Inject] 
        IJSRuntime? JsRunTime { get; set; }

        protected IEnumerable<OlapSales> GridData = Enumerable.Empty<OlapSales>();

        [Inject]
        protected SalesETLJob? SalesETLJob { get; set; }

        [Inject]
        protected OlapHelper? OlapHelper { get; set; }

        protected bool IsFuzzyQueryConstuctorVisible { get; set; } = false;

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

        protected void CreateFuzzyParam()
        {
            IsFuzzyQueryConstuctorVisible = true;
        }
    }
}
