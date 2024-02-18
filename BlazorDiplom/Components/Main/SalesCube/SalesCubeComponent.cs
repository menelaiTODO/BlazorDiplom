using BlazorDiplom.Components.Main.SalesCube.Models;
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

        protected IEnumerable<SalesByMonth> GridData = Enumerable.Empty<SalesByMonth>();

        [Inject]
        protected SalesETLJob? SalesETLJob { get; set; }

        protected async Task RunSalesEtlJob()
        {
            await JsRunTime!.InvokeVoidAsync("alert", "ETL процесс запущен!"); // Alert

            await SalesETLJob!.RunAsync();

            await JsRunTime!.InvokeVoidAsync("alert", "ETL процесс завершен!"); // Alert
        }

/*        protected IEnumerable<SalesByMonth> GetSalesDataByMonth()
        {
            return null;
        }*/

        protected async Task RunCubeProcessing()
        {
            await JsRunTime!.InvokeVoidAsync("alert", "В разработке!"); // Alert
        }
    }
}
