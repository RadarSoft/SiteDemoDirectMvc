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
    public class CalculatedFieldsSample : BaseGridSample
    {
        public CalculatedFieldsSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitLayout()
        {
            OlapAnalysis.BeginUpdate();

            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Employees");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laRow);

            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            if (M != null)
                OlapAnalysis.Pivoting(M, TLayoutArea.laRow, null);

            OlapAnalysis.EndUpdate();

            OlapAnalysis.CellSet.Rebuild();

            //Drill down the first member of the Parent-child hierarchy.
            IMemberCell imcell = OlapAnalysis.CellSet.Cells(0, 2) as IMemberCell;
            imcell.DrillAction(TPossibleDrillActions.esParentChild);
        }
    }
}