using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Models;
using RadarSoft.RadarCube.Common;
using RadarSoft.RadarCube.Web;
using RadarSoft.RadarCube.Web.Analysis;
using RadarSoft.RadarCube.Web.Mvc;
using SamplesFactory;

namespace SamplesFactory
{
    public class TestingSample : RSample
    {
        public TestingSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitSample()
        {
            base.InitSample();
            
            OlapAnalysis.StructureTreeWidth = new Unit("400px");
            OlapAnalysis.Options.HierarchiesDisplayMode = HierarchiesDisplayModeForExport.TreeLike;

            OlapAnalysis.CustomButtons.Clear();
            var button = new MvcCustomToolboxButton();
            button.Text = "Save state";
            button.ClientScript = "{saveLayout();}";
            OlapAnalysis.CustomButtons.Add(button);
            button = new MvcCustomToolboxButton();
            button.Text = "Load state";
            button.ClientScript = "{loadLayout();}";
            OlapAnalysis.CustomButtons.Add(button);
            OlapAnalysis.Options.BGColor = Color.DarkSalmon;
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            //OlapAnalysis.AnalysisType = AnalysisType.Chart;
            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Products by categories");
            H.TotalAppearance = TTotalAppearance.taInvisible;
            H.ShowEmptyLines = true;
            OlapAnalysis.PivotingLast(H, TLayoutArea.laRow);
            H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Date");
            H.ShowEmptyLines = true;
            OlapAnalysis.PivotingLast(H, TLayoutArea.laColumn);

            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            if (M != null)
                OlapAnalysis.Pivoting(M, TLayoutArea.laRow, null);

            M = OlapAnalysis.Measures.FindByDisplayName("Quantity");
            if (M != null)
                OlapAnalysis.Pivoting(M, TLayoutArea.laRow, null);

        }
    }
}