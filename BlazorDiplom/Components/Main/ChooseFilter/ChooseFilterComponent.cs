using FuzzyDataDbCore.DatabaseContext;
using FuzzyDataDbCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BlazorDiplom.Components.Main.ChooseFilter
{
    public class ChooseFilterComponent : ComponentBase
    {
        [Parameter]
        public EventCallback OnChoosedCallback { get; set; }

        [Parameter]
        public int CubeSlice { get; set; }

        [Inject]
        public FuzzyDataDbContext? FuzzyDataDbContext { get; set; }

        protected IEnumerable<CustomLinguisticVariable>? GridData { get; set; }

        protected CustomLinguisticVariable? Selected { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            GridData = FuzzyDataDbContext?.CustomLinguisticVariables.Include(item => item.Points).Where(item => item.CubeSliceId == CubeSlice).Select(item => item);
        }

        protected async Task ApplyFilter()
        {
            if (Selected is null)
                return;

            await OnChoosedCallback.InvokeAsync(Selected!);
        }
    }
}
