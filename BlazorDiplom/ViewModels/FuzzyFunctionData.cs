using BlazorDiplom.Infrastructure;
using BlazorDiplom.Infrastructure.Enums;
using System.Reflection.Metadata.Ecma335;

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
        /// Функция принадлежности
        /// </summary>
        public Func<double[], double, double>? MemberShipFunction { get; set; }

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
            Func<int, string, string, IEnumerable<double>, Func<double[], double, double>?, FuzzyFunctionData> func = (int id, string description, string imgSrc, IEnumerable<double> yValues, Func<double[], double, double>? funcMember) 
                =>
            new FuzzyFunctionData 
            { 
                Id = id, 
                Description = description, 
                YValues = yValues, 
                ImgSrc = imgSrc,
                MemberShipFunction = funcMember
            };

            double[] yValues;
            Func<double[], double, double> memberShipFunction;

            foreach (var item in DataHelper.GetDatasoureByEnum<FuzzyFunctionEnum>(false))
            {
                switch ((FuzzyFunctionEnum)item.Key)
                {
                    case FuzzyFunctionEnum.TriangularFunction:
                        yValues = [0, 1, 0];
                        yield return func(item.Key, item.Value, "Images/treug.png", yValues, null);
                        break;
                    case FuzzyFunctionEnum.TrapezoidalFunction:
                        memberShipFunction = new Func<double[], double, double>((double[] points, double x) => 
                        {
                            double returnValue;

                            if (x >= points[0] && x <= points[1])
                            {
                                returnValue = 1 - ((points[1] - x) / (points[1] - points[0]));
                            }
                            else if (x >= points[1] && x <= points[2])
                            {
                                returnValue = 1;
                            }
                            else if (x >= points[2] && x <= points[3])
                            {
                                returnValue = 1 - ((x - points[2]) / (points[3] - points[2]));
                            }
                            else
                            {
                                returnValue = 0;
                            }

                            return returnValue;
                        } 
                        );

                        yValues = [0, 1, 1, 0];
                        yield return func(item.Key, item.Value, "Images/trapec.png", yValues, memberShipFunction);
                        break;
                    case FuzzyFunctionEnum.ZShapedFunctionType1:
                        yValues = [0, 1, 1, 0];
                        yield return func(item.Key, item.Value, "Images/z-type1.png", yValues, null);
                        break;
                    case FuzzyFunctionEnum.ZShapedFunctionType2:
                        yValues = [0, 1, 1, 0];
                        yield return func(item.Key, item.Value, "Images/z-type2.png", yValues, null);
                        break;
                    case FuzzyFunctionEnum.SplineFunctionType1:
                        yValues = [0, 1, 1, 0];
                        yield return func(item.Key, item.Value, "Images/spline1.png", yValues, null);
                        break;
                    case FuzzyFunctionEnum.SplineFunctionType2:
                        yValues = [0, 1, 1, 0];
                        yield return func(item.Key, item.Value, "Images/spline2.png", yValues, null);
                        break;
                    case FuzzyFunctionEnum.PShapedFunction:
                        yValues = [0, 1, 1, 0];
                        yield return func(item.Key, item.Value, "Images/p-type1.png", yValues, null);
                        break;
                }
            }
        }
    }
}
