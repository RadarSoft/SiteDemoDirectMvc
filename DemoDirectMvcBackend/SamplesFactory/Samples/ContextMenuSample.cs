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

namespace SamplesFactory
{
    public class ContextMenuSample : GettingStartedSample
    {
        public ContextMenuSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        public override MvcOlapAnalysis OlapAnalysis
        {
            get
            {
                if (_OlapAnalysis == null)
                {
                    _OlapAnalysis = new MvcOlapAnalysis("MvcOlapAnalysis1");

                    _OlapAnalysis.InitOlap += delegate { InitOlapAnalysis(); };

                    _OlapAnalysis.OnShowContextMenu += OlapAnalysis_OnShowContextMenu;

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

        private void OlapAnalysis_OnShowContextMenu(object sender, ShowContextMenuEventArgs e)
        {
            //Changing the context menu for data cells only
            if (e.Cell.CellType == TCellType.ctData)
            {
                IDataCell D = (IDataCell)e.Cell;
                //Add the menu separator
                e.ContextMenu.Items.Add(new ContextMenuItem() { IsSeparator = true });
                ContextMenuItem M = new ContextMenuItem();
                M.Text = "Cell details...";
                e.ContextMenu.Items.Add(M);

                if (D.Address.Measure != null)
                {
                    ContextMenuItem M2 = new ContextMenuItem();
                    M2.Text = "Measure: " + D.Address.Measure.DisplayName;
                    M2.NavigateUrl = "javascript:{ alert('" + D.Address.Measure.DisplayName + " item has been clicked.');}";
                    M.ChildItems.Add(M2);
                }

                for (int i = 0; i < D.Address.LevelsCount; i++)
                {
                    ContextMenuItem M2 = new ContextMenuItem();
                    M2.Text = D.Address.Levels(i).DisplayName + ": " + D.Address.Members(i).DisplayName;
                    M2.NavigateUrl = "javascript:{ alert('" + D.Address.Members(i).DisplayName + " item has been clicked.');}";
                    M.ChildItems.Add(M2);
                }
            }
        }
    }
}