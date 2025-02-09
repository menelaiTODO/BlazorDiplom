using BlazorDiplom.Infrastructure;
using BlazorDiplom.Infrastructure.Enums;

namespace BlazorDiplom.ViewModels
{
    /// <summary>
    /// Модель, отображающая основную информацию по функции принадлежности
    /// </summary>
    public class FuzzyFunctionData
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Путь к картинке
        /// </summary>
        public string ImgSrc { get;set; } = string.Empty;

        /// <summary>
        /// Значение функции принадлежности (необходимо для последующего указание точек по оси X)
        /// </summary>
        public IEnumerable<double> YValues { get; set; } = Enumerable.Empty<double>();

        /// <summary>
        /// Создание датасурса 
        /// </summary>
        public static IEnumerable<FuzzyFunctionData> BuildDataSource()
        {
            Func<int, string, string, IEnumerable<double>, FuzzyFunctionData > func = (int id, string description, string imgSrc, IEnumerable<double> yValues) 
                =>
            new FuzzyFunctionData 
            { 
                Id = id, 
                Description = description, 
                YValues = yValues, 
                ImgSrc = imgSrc
            };

            double[] yValues;
            foreach (var item in DataHelper.GetDatasoureByEnum<FuzzyFunctionEnum>(false))
            {
                switch ((FuzzyFunctionEnum)item.Key)
                {
                    case FuzzyFunctionEnum.TriangularFunction:
                        yValues = [0, 1, 0];
                        yield return func(item.Key, item.Value, "Images/treug.png", yValues);
                        break;
                    case FuzzyFunctionEnum.TrapezoidalFunction:
                        yValues = [0, 1, 1, 0];
                        yield return func(item.Key, item.Value, "Images/trapec.png", yValues);
                        break;
                    case FuzzyFunctionEnum.ZShapedFunctionType1:
                        yValues = [0, 1, 1, 0];
                        yield return func(item.Key, item.Value, "Images/z-type1.png", yValues);
                        break;
                    case FuzzyFunctionEnum.ZShapedFunctionType2:
                        yValues = [0, 1, 1, 0];
                        yield return func(item.Key, item.Value, "Images/z-type2.png", yValues);
                        break;
                    case FuzzyFunctionEnum.SplineFunctionType1:
                        yValues = [0, 1, 1, 0];
                        yield return func(item.Key, item.Value, "Images/spline1.png", yValues);
                        break;
                    case FuzzyFunctionEnum.SplineFunctionType2:
                        yValues = [0, 1, 1, 0];
                        yield return func(item.Key, item.Value, "Images/spline2.png", yValues);
                        break;
                    case FuzzyFunctionEnum.PShapedFunction:
                        yValues = [0, 1, 1, 0];
                        yield return func(item.Key, item.Value, "Images/p-type1.png", yValues);
                        break;
                }
            }
        }
    }
}
