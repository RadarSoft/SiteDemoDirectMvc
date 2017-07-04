using System;
using System.Data;
using System.Web.UI.WebControls;
using Models;
using RadarSoft.RadarCube.Common;
using RadarSoft.RadarCube.Web;
using System.Globalization;
using System.Collections.Generic;

namespace SamplesFactory
{
    public class ChartTypesSample : BaseChartSample
    {
        public ChartTypesSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitLayout()
        {
            OlapAnalysis.BeginUpdate();

            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Date");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColumn);
            H.Levels[2].Visible = true;

            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            OlapAnalysis.Pivoting(M, TLayoutArea.laRow);
            M.DefineChartMeasureType(SeriesType.StackedArea);

            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Shippers");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColor);

            M = OlapAnalysis.Measures.FindByDisplayName("Quantity");
            OlapAnalysis.Pivoting(M, TLayoutArea.laRow);
            M.DefineChartMeasureType(SeriesType.Column);
            OlapAnalysis.EndUpdate();
        }
    }
}