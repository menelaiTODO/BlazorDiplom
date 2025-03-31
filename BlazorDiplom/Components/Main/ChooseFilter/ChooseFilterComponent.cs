using BlazorDiplom.Infrastructure;
using BlazorDiplom.Infrastructure.Enums;
using FuzzyDataDbCore.DatabaseContext;
using FuzzyDataDbCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlazorDiplom.Components.Main.ChooseFilter
{
    public class ChooseFilterComponent : ComponentBase
    {
        private CustomMultiplyLinguisticVariable? _selectedMultiply;

        [Parameter]
        public EventCallback OnChoosedCallback { get; set; }

        [Parameter]
        public int CubeSlice { get; set; }

        [Inject]
        public FuzzyDataDbContext? FuzzyDataDbContext { get; set; }

        protected IEnumerable<CustomLinguisticVariable>? GridDataSingletone { get; set; }

        public IEnumerable<CustomMultiplyLinguisticVariable>? GridDataMultiply { get; private set; }
        public IEnumerable<KeyValuePair<int, (double X, double Y)>>? ChartData2 { get; private set; }
        public IEnumerable<(int Id, (double X, double Y) PointsForChart)> MultiplyChartData { get; private set; }
        
        protected IEnumerable<KeyValuePair<int, string>>? LinguisticVariableType { get; private set; }

        protected int? LinguisticVariableBindedValue { get; set; }

        protected CustomLinguisticVariable? SelectedSingle { get; set; }

        protected CustomMultiplyLinguisticVariable? SelectedMultiply
        {
            get
            {
                return _selectedMultiply;
            }
            set
            {
                _selectedMultiply = value;

                MultiplyCustomLinguisticVariableEx = _selectedMultiply?.CustomLinguisticVariables?.Select(item => new CustomLinguisticVariableEx { LinguisticVariable = item });

                ChartData2 = MultiplyCustomLinguisticVariableEx?.SelectMany(item => item.SingletoneLinguisticVariable!.PointsForChart, (item1, item2) => new KeyValuePair<int, (double X, double Y)>(item1.LinguisticVariable!.Id, item2));

                StateHasChanged();
            }
        }

        protected IEnumerable<CustomLinguisticVariableEx>? MultiplyCustomLinguisticVariableEx { get; set; }

        protected CustomLinguisticVariableEx? CustomLinguisticVariableEx { get; set; }



        protected override void OnInitialized()
        {
            base.OnInitialized();

            GridDataSingletone = FuzzyDataDbContext?.CustomLinguisticVariables.Include(item => item.Points).Where(item => item.CubeSliceId == CubeSlice).Select(item => item);

            LinguisticVariableType = DataHelper.GetDatasoureByEnum<LinguisticVariableTypeEnum>(false);
        }


        public void OnLinguisticChanged(KeyValuePair<int, string> t)
        {
            LinguisticVariableBindedValue = t.Key;

            switch ((LinguisticVariableTypeEnum)LinguisticVariableBindedValue)
            {
                case LinguisticVariableTypeEnum.SingletoneLinguisticVariable:

                    GridDataSingletone = FuzzyDataDbContext?.CustomLinguisticVariables.Include(item => item.Points).Where(item => item.CubeSliceId == CubeSlice).Select(item => item);

                    break;

                case LinguisticVariableTypeEnum.MultiplyLunguisticVariable:

                    GridDataMultiply = FuzzyDataDbContext?.CustomMultiplyLinguisticVariables.Include(item => item.CustomLinguisticVariables)!
                        .ThenInclude(item => item.Points).Where(item => item.CubeSliceId == CubeSlice).Select(item => item);


                    break;
            }
        }

        protected async Task ApplySingleFilter()
        {
            if (SelectedSingle is null)
                return;

            await OnChoosedCallback.InvokeAsync(SelectedSingle!.ToEnumerable());
        }

        protected async Task ApplyMultiplyFilter()
        {
            if (SelectedMultiply is null)
                return;

            await OnChoosedCallback.InvokeAsync(SelectedMultiply!.CustomLinguisticVariables!);
        }
    }
}
