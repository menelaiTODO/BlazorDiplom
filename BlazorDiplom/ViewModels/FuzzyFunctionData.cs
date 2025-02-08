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
        /// Количество точек необходимое для функции принадлежности
        /// </summary>
        public int PointCount { get; set; }

        /// <summary>
        /// Создание датасурса 
        /// </summary>
        public static IEnumerable<FuzzyFunctionData> BuildDataSource()
        {
            Func<int, string, string, int, FuzzyFunctionData> func = (int id, string description, string imgSrc, int pointCount) => new FuzzyFunctionData { Id = id, Description = description, PointCount = pointCount, ImgSrc = imgSrc };

            foreach (var item in DataHelper.GetDatasoureByEnum<FuzzyFunctionEnum>(false))
            {
                switch ((FuzzyFunctionEnum)item.Key)
                {
                    case FuzzyFunctionEnum.TriangularFunction:
                        yield return func(item.Key, item.Value, "Images/treug.png", 3);
                        break;
                    case FuzzyFunctionEnum.TrapezoidalFunction:
                        yield return func(item.Key, item.Value, "Images/trapec.png", 4);
                        break;
                    case FuzzyFunctionEnum.ZShapedFunctionType1:
                        yield return func(item.Key, item.Value, "Images/z-type1.png", 3);
                        break;
                    case FuzzyFunctionEnum.ZShapedFunctionType2:
                        yield return func(item.Key, item.Value, "Images/z-type2.png", 3);
                        break;
                    case FuzzyFunctionEnum.SplineFunctionType1:
                        yield return func(item.Key, item.Value, "Images/spline1.png", 3);
                        break;
                    case FuzzyFunctionEnum.SplineFunctionType2:
                        yield return func(item.Key, item.Value, "Images/spline2.png", 3);
                        break;
                    case FuzzyFunctionEnum.PShapedFunction:
                        yield return func(item.Key, item.Value, "Images/p-type1.png", 3);
                        break;
                }
            }
        }
    }
}
