using System;
using System.Data;
using System.Web.UI.WebControls;
using Models;
using RadarSoft.RadarCube.Common;
using RadarSoft.RadarCube.Web;


namespace SamplesFactory
{
    public class CellCommentsSample : BaseGridSample
    {
        public CellCommentsSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitLayout()
        {
            OlapAnalysis.BeginUpdate();

            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Categories");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laRow);

            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Date");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColumn);

            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            if (M != null)
                OlapAnalysis.Pivoting(M, TLayoutArea.laRow, null);

            OlapAnalysis.EndUpdate();

            OlapAnalysis.CellSet.Rebuild();

            //Adding a comment to the 'Dairy Products' member cell.
            IMemberCell mcell = OlapAnalysis.CellSet.Cells(0, 2) as IMemberCell;
            mcell.Comment = "The comment to the 'Dairy Products' member cell.";

            //Adding a comment to the ([Dairy Products], [1996]) data cell.
            IDataCell dcell = OlapAnalysis.CellSet.Cells(1, 2) as IDataCell;
            dcell.Comment = "The comment to the ([Dairy Products], [1996]) data cell.";
        }
    }
}