using System;
using System.Data;
using System.Web.UI.WebControls;
using Models;
using RadarSoft.RadarCube.Common;
using RadarSoft.RadarCube.Web;
using System.Globalization;

namespace SamplesFactory
{
    public class ColorModificationsSample : BaseGridSample
    {
        public ColorModificationsSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitSample()
        {
            base.InitSample();
            OlapAnalysis.ShowLegends = true;
            OlapAnalysis.ShowModificationAreas = true;
        }

        protected override void InitLayout()
        {
            OlapAnalysis.BeginUpdate();

            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Products by categories");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laRow);

            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Date");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColumn);

            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            if (M != null)
                OlapAnalysis.Pivoting(M, TLayoutArea.laRow, null);

            var h = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Products by categories");
            var m = OlapAnalysis.Measures.FindByDisplayName("Sales");

            OlapAnalysis.PivotingFirst(h, TLayoutArea.laColor);

            OlapAnalysis.Pivoting(m, TLayoutArea.laColorFore);

            OlapAnalysis.EndUpdate();
        }
    }
}