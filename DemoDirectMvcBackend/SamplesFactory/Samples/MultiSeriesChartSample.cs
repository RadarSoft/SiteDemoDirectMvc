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
    public class MultiSeriesChartSample : BaseChartSample
    {
        public MultiSeriesChartSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitLayout()
        {
            OlapAnalysis.BeginUpdate();

            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            OlapAnalysis.Pivoting(M, TLayoutArea.laRow);

            M = OlapAnalysis.Measures.FindByDisplayName("Quantity");

            TMeasureGroup mg = ((TChartAxesLayout)OlapAnalysis.AxesLayout).YAxis[0];

            OlapAnalysis.Pivoting(M, TLayoutArea.laRow, mg, TLayoutArea.laRow);

            M.DefineChartMeasureType(SeriesType.Bar);

            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Shippers");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColumn);

            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Categories");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColumn);
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColor);

            OlapAnalysis.EndUpdate();
        }
    }
}