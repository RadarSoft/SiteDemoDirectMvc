using System;
using System.Data;
using System.Web.UI.WebControls;
using Models;
using RadarSoft.RadarCube.Common;
using RadarSoft.RadarCube.Web;
using System.Globalization;
using System.Collections.Generic;
using RadarSoft.RadarCube.Web.Mvc;
using OLAPDemoASP.Code;
using RadarSoft.RadarCube.Web.Analysis;

namespace SamplesFactory
{
    public class MeasureModesSample : BaseGridSample
    {
        public MeasureModesSample(SamplesModel samplesModel) : base(samplesModel)
        {

        }

        public override MvcOlapAnalysis OlapAnalysis
        {
            get
            {
                if (_OlapAnalysis == null)
                {
                    _OlapAnalysis = new MvcOlapAnalysis("MvcOlapAnalysis1");

                    _OlapAnalysis.AnalysisType = AnalysisType.Grid;

                    _OlapAnalysis.InitOlap += delegate { InitOlapAnalysis(); };

                    _OlapAnalysis.OnInitMeasures += OlapAnalysis_OnInitMeasures;

                    _OlapAnalysis.OnShowMeasure += OlapAnalysis_OnShowMeasure;

                    var cube = new MvcRCube
                    {
                        DataSet = new Northwind(),
                        ID = OlapAnalysis.ID + "_Cube",
                    };
                    cube.OnCalculateField += Cube_OnCalculateField;
                    _OlapAnalysis.Cube = cube;
                }
                return _OlapAnalysis;
            }
        }

        private void OlapAnalysis_OnInitMeasures(object sender, EventArgs e)
        {
            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");

            if (M != null)
            {
                //"Rank by Row" measure mode is calculated in the OnShowMeasure event handler
                M.ShowModes.Add("Rank by row");
            }
        }

        private void OlapAnalysis_OnShowMeasure(object sender, TShowMeasureArgs e)
        {
            if (e.ShowMode.Caption == "Rank by row")
            {
                //Assign an empty value to cells which rank is impossible to calculate
                e.ReturnValue = "";
                //If a cell is empty, then return
                if (!(e.OriginalData is IComparable))
                    return;
                //Set the initial rank value
                int Rank = 1;
                //A cycle over all members which are neighbor for a given one in the row area
                IComparable cmp = e.OriginalData as IComparable;
                for (int i = 0; i < e.RowSiblings.Count; i++)
                {
                    //If a value of the neighbor cell is greater that the current, then to increase the rank variable
                    try
                    {
                        if (cmp.CompareTo(e.Evaluator.SiblingValue(e.RowSiblings[i])) < 0)
                            Rank++;
                    }
                    catch
                    {; }
                }
                //Assign the return event values
                e.ReturnValue = Rank.ToString();
                e.ReturnData = Rank;
            }
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

            M.Visible = true;

            //Turn on the "Percent of Column Total" and "Rank by row" modes for the "Sales" measure
            M.ShowModes.Find(TMeasureShowModeType.smPercentColTotal).Visible = true;

            M.ShowModes.Find("Rank by row").Visible = true;

            OlapAnalysis.EndUpdate();
        }
    }
}