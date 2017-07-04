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
    public class ChartSample : BaseChartSample
    {
        public ChartSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitLayout()
        {
            OlapAnalysis.BeginUpdate();

            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            OlapAnalysis.Pivoting(M, TLayoutArea.laRow);

            M.DefineChartMeasureType(SeriesType.Pie);

            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Date");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColumn);

            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Categories");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColumn);
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColor);

            OlapAnalysis.EndUpdate();
        }
    }
}