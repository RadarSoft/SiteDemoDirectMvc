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
    public class SeriesModificationsSample : BaseChartSample
    {
        public SeriesModificationsSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitLayout()
        {
            OlapAnalysis.BeginUpdate();

            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Date");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColumn);

            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            OlapAnalysis.Pivoting(M, TLayoutArea.laRow);
            //Size modification by "Sales" values
            OlapAnalysis.Pivoting(M, TLayoutArea.laSize);

            M = OlapAnalysis.Measures.FindByDisplayName("Quantity");
            OlapAnalysis.Pivoting(M, TLayoutArea.laColumn);

            M.DefineChartMeasureType(SeriesType.Scatter, true);

            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Categories");
            //Color modification by "Categories" members
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColor);

            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Quarter");
            //Shape modification by "Quarter" members
            OlapAnalysis.PivotingLast(H, TLayoutArea.laShape);

            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Customers");
            //Detailing series on "Customers" members
            OlapAnalysis.PivotingLast(H, TLayoutArea.laDetails);

            OlapAnalysis.EndUpdate();
        }
    }
}