using System;
using System.Data;
using System.Web.UI.WebControls;
using Models;
using RadarSoft.RadarCube.Common;
using RadarSoft.RadarCube.Web;


namespace SamplesFactory
{
    public class DrillingSample : BaseGridSample
    {
        public DrillingSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitLayout()
        {
            OlapAnalysis.BeginUpdate();

            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Employees");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laRow);

            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Categories");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laRow);

            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Date");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColumn);

            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            if (M != null)
                OlapAnalysis.Pivoting(M, TLayoutArea.laRow, null);

            OlapAnalysis.EndUpdate();

            OlapAnalysis.CellSet.Rebuild();

            //Drill down the 'Andrew Fuller' member to its child members.
            IMemberCell imcell = OlapAnalysis.CellSet.Cells(0, 2) as IMemberCell;
            imcell.DrillAction(TPossibleDrillActions.esParentChild);

            //Drill down the 'Steven Buchanan' member to the next 'Categories' hierarchy.
            imcell = OlapAnalysis.CellSet.Cells(0, 3) as IMemberCell;
            imcell.DrillAction(TPossibleDrillActions.esNextHierarchy);

            //Drill down the '1996' member to the next 'Quarter' level.
            imcell = OlapAnalysis.CellSet.Cells(1, 0) as IMemberCell;
            imcell.DrillAction(TPossibleDrillActions.esNextLevel);
        }
    }
}