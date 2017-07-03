using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumeralMethodsLibrary;
using PolishNotationLibrary;
using ZedGraph;

namespace MainForm
{
    /// <summary>
    /// Создание графика
    /// </summary>
    static class GraphCreating
    {
        /// <summary>
        /// Создания графка уравнения и его корней
        /// </summary>
        /// <param name="graphControl"> Контрол графика </param>
        /// <param name="solving"> Решение уравнения </param>
        public static void DrawGraph(ZedGraphControl graphControl, Solving solving)
        {
            GraphPane graphPane = graphControl.GraphPane;
            graphPane.CurveList.Clear();
            graphPane.Title.Text = string.Format("{0} = 0", solving.GetEquation.GetInfixNotation);
            graphPane.XAxis.Title.Text = solving.GetEquation.GetVariableSymbol.ToString();
            graphPane.YAxis.Title.Text = string.Format("f({0})", solving.GetEquation.GetVariableSymbol);
            List<double> xList = new List<double>();
            List<double> yList = new List<double>();
            double interavalMin = solving.GetIntervalMin;
            double interavalMax = solving.GetInteravalMax;
            double step = 0.0005;
            PostfixNotation function = solving.GetEquation;
           
            for (double i = interavalMin; i <= interavalMax; i+= step)
            {
                xList.Add(i);
                yList.Add(function.Calcuate(i));
            }
            FilteredPointList filtredPointsList = new FilteredPointList(xList.ToArray(), yList.ToArray());
            PointPairList rootList = new PointPairList();
            List<double> roots = solving.GetRootsList.ConvertAll<double>(x =>
            {
                return x.GetRootValue;
            });
            for (int k = 0; k < roots.Count; k++)
            {
                rootList.Add(roots[k], function.Calcuate(roots[k]));
            }
            PointPairList fakeRootsList = new PointPairList();
            List<double> fakeRoots = solving.GetFakeRootsList.ConvertAll<double>(x =>
            {
                return x.GetRootValue;
            });
            for (int j = 0; j < fakeRoots.Count; j++)
            {
                fakeRootsList.Add(fakeRoots[j], function.Calcuate(fakeRoots[j]));
            }
            LineItem functionLine = graphPane.AddCurve("График уравнения", filtredPointsList, Color.Blue, SymbolType.None);
            functionLine.Line.IsSmooth = true;
            LineItem rootsPoints = graphPane.AddCurve("Корни уравнения", rootList, Color.Green, SymbolType.Circle);
            rootsPoints.Line.IsVisible = false;
            rootsPoints.Symbol.Fill.Color = Color.Green;
            rootsPoints.Symbol.Fill.Type = FillType.Solid;
            LineItem fakeRootsPoints = graphPane.AddCurve("Точки, в которых сходился метод", fakeRootsList, Color.Orange, SymbolType.Circle);
            fakeRootsPoints.Line.IsVisible = false;
            fakeRootsPoints.Symbol.Fill.Color = Color.Orange;
            fakeRootsPoints.Symbol.Fill.Type = FillType.Solid;
            graphPane.IsBoundedRanges = true;
            graphPane.XAxis.Scale.Min = interavalMin;
            graphPane.XAxis.Scale.Max = interavalMax;
            graphPane.YAxis.Scale.Min = -10;
            graphPane.YAxis.Scale.Max = 10;
            graphControl.AxisChange();
            graphControl.Invalidate();
        }
    }
}
