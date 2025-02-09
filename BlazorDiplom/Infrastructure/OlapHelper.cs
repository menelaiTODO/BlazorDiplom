using BlazorDiplom.ViewModels;
using BlazorDiplom.ViewModels.MOLAP;
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
            var data = Enumerable.Empty<OlapAttr>();

            using (var conn = new AdomdConnection(_connectionString))
            {
                conn.Open();
                var commandText = @"SELECT MEASURE_UNIQUE_NAME FROM $system.mdschema_measures";

                data = conn.MolapQuery<OlapAttr>(commandText);
                conn.Close();
            }

            return data;
        }

        /// <summary>
        /// Состояние продаж (Тип 1) 
        /// </summary>
        public IEnumerable<OlapSales> GetSalesData()
        {
            var data = Enumerable.Empty<OlapSales>();

            using (var conn = new AdomdConnection(_connectionString))
            {
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
                
                data = conn.MolapQuery<OlapSales>(commandText);
                conn.Close();
            }

            return data;
        }
    }
}
