using BlazorDiplom.ViewModels;
using BlazorDiplom.ViewModels.MOLAP;
using Dapper;
using FuzzyDataDbCore.Models;
using Microsoft.AnalysisServices.AdomdClient;

namespace BlazorDiplom.Infrastructure
{
    public class OlapHelper
    {
        private readonly string _connectionString = string.Empty;

        public OlapHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Сведения об атрибутах в MS SSAS 
        /// </summary>
        public IEnumerable<OlapAttr> GetAttrDescription()
        {
            using var conn = new AdomdConnection(_connectionString);

            conn.Open();

            var commandText = @"SELECT * FROM $SYSTEM.MDSCHEMA_MEASURES WHERE CUBE_NAME = 'SalesCube';";

            var adomdCommand = conn.CreateCommand();
            adomdCommand.CommandText = commandText;

            return adomdCommand.ExecuteReader().Parse<OlapAttr>().ToArray();
        }

        /// <summary>
        /// Состояние продаж (Тип 1) 
        /// </summary>
        public IEnumerable<OlapSales> GetSalesData()
        {
            using var conn = new AdomdConnection(_connectionString);

                conn.Open();
            
            var commandText = @"SELECT NON EMPTY 
                                   { 
                                   [Measures].[Число Fact Sales], 
                                   [Measures].[Sum] 
                                   } ON COLUMNS, 
                                   NON EMPTY { 
                                   ([Dim Dates].[Month Name].[Month Name] ) 
                                   } DIMENSION PROPERTIES 
                                   MEMBER_CAPTION, 
                                   MEMBER_UNIQUE_NAME 
                                   ON ROWS FROM [SalesCube]";

            var adomdCommand = conn.CreateCommand();

            adomdCommand.CommandText = commandText;

            var reader = adomdCommand.ExecuteReader();

            var result = reader.Parse<OlapSales>().ToArray();

            return result;
        }

        /// <summary>
        /// Получение нечеткого среза продаж по месяцам
        /// </summary>
        public IEnumerable<OlapSales> GetFuzzySalesData(params CustomLinguisticVariableEx[] linguisticVariables)
        {
            var salesData = GetSalesData();

            foreach (var row in salesData)
            {
                var resultsInternal = new List<(CustomLinguisticVariable Variable, double Result)>();
                
                foreach (var variable in linguisticVariables)
                {
                    // к какому объекту в строке будет применяться вычисление значение функции принадлежности
                    var rowObj = DataHelper.GetPropertyValueByColumnName(row, variable!.LinguisticVariable!.MeasureName);

                    
                    if (rowObj != null)
                    {
                        // точки настройки функции принадлежности
                        var points = variable!.LinguisticVariable.Points!.OrderBy(item => item.PointSeq).Select(item => item.XValue).ToArray();

                        var result = variable.FuzzyFunctionData.MemberShipFunction(points, (double)rowObj);

                        if (result >= variable.LinguisticVariable.MinIndex)
                        {
                            row.FuzzyResults.Add((variable.LinguisticVariable.Name, result));
                        }

                        resultsInternal.Add((variable.LinguisticVariable, result));
                    }
                }

                if (resultsInternal.Any(item => item.Result >= item.Variable.MinIndex))
                    yield return row;
                else
                    continue;
            }
        }
    }
}
