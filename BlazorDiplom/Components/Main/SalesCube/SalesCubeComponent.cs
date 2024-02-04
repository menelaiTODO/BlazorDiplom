using BlazorSpinner;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OLTPDatabaseCore.Jobs;

namespace BlazorDiplom.Components.Main.SalesCube
{
    public class SalesCubeComponent : ComponentBase
    {
        [Inject] 
        IJSRuntime? JsRunTime { get; set; }

        [Inject]
        protected SalesETLJob? SalesETLJob { get; set; }

        protected async Task RunSalesEtlJob()
        {
            await JsRunTime!.InvokeVoidAsync("alert", "ETL процесс запущен!"); // Alert

            await SalesETLJob!.RunAsync();

            await JsRunTime!.InvokeVoidAsync("alert", "ETL процесс завершен!"); // Alert
        }
    }
}
