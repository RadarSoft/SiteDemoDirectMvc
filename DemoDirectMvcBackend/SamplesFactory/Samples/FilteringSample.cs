using System;
using System.Data;
using System.Web.UI.WebControls;
using Models;
using RadarSoft.RadarCube.Common;
using RadarSoft.RadarCube.Web;
using System.Globalization;

namespace SamplesFactory
{
    public class FilteringSample : BaseGridSample
    {
        public FilteringSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitLayout()
        {
            OlapAnalysis.BeginUpdate();

            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Products by categories");
            H.ShowEmptyLines = true;
            OlapAnalysis.PivotingLast(H, TLayoutArea.laRow);

            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Date");
            H.ShowEmptyLines = true;
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColumn);

            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            if (M != null)
                OlapAnalysis.Pivoting(M, TLayoutArea.laRow, null);

            OlapAnalysis.EndUpdate();

            SetFilters();
        }

        void SetFilters()
        {
            // Making invisible of the "Dairy Products" member of the "Products by categories" hierarchy.
            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Products by categories");
            TMember m = H.FindMemberByName("Dairy Products");
            m.Visible = false;


            // Context filtering of "Date" hierarchy by captions "Year" members' captions.
            // The hierarchy must be placed into any of the three active areas (row, columns or pages).
            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Date");
            string dateFirst = "1997";
            string dateSecond = "1998";
            TFilter hf = new TFilter(H.Levels[0], TOLAPFilterType.ftOnCaption, null, TOLAPFilterCondition.fcBetween, dateFirst, dateSecond);
            H.Levels[0].Filter = hf;
            H.ShowEmptyLines = false;


            // Context filtering of the "Sales" measure values.
            // The measure must be visible in the Grid.
            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            TMeasureFilter mf = new TMeasureFilter(M, TOLAPFilterCondition.fcGreater, "1000", null);
            mf.RestrictsTo = TMeasureFilterRestriction.mfrFactTable;
            M.Filter = mf;
        }
    }
}