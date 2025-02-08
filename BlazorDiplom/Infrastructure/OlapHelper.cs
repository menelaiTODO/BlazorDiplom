using BlazorDiplom.ViewModels;
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
