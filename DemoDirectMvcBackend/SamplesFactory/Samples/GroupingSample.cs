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
    public class GroupingSample : BaseGridSample
    {
        public GroupingSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitLayout()
        {
            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Products by categories");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laRow);

            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Date");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColumn);

            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            if (M != null)
                OlapAnalysis.Pivoting(M, TLayoutArea.laRow, null);

            M = OlapAnalysis.Measures.FindByDisplayName("Quantity");
            if (M != null)
                OlapAnalysis.Pivoting(M, TLayoutArea.laRow, null);

            CreateGroup();

            OlapAnalysis.CellSet.Rebuild();

            //Drill down the 'Food' member.
            IMemberCell imcell = OlapAnalysis.CellSet.Cells(0, 5) as IMemberCell;
            imcell.DrillAction(TPossibleDrillActions.esParentChild);
        }

        void CreateGroup()
        {
            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Products by categories");
            // We only need the first level - "Categories"
            TLevel L = H.Levels[0];
            List<TMember> Members = new List<TMember>();
            foreach (TMember M in L.Members)
            {
                if (M.DisplayName != "Beverages" && M.DisplayName != "Condiments")
                    Members.Add(M);
            }

            H.CreateGroup("Food", TCustomMemberPosition.cmpLast, Members);
        }
    }
}