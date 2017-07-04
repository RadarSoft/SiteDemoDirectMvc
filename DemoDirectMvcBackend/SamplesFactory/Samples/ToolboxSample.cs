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
using System.IO;

namespace SamplesFactory
{
    public class ToolboxSample : GettingStartedSample
    {
        public ToolboxSample(SamplesModel samplesModel) : base(samplesModel)
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

                    _OlapAnalysis.ToolboxItemAction += OlapAnalysis_ToolboxItemAction;

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

        private void OlapAnalysis_ToolboxItemAction(object sender, ToolboxItemActionArgs e)
        {
            //Save current layout
            if (e.Item != null && e.Item.ButtonID == "56b4a187-2112-4569-870d-913021761b4c")
            {
                var response = Model.Context.Response;
                response.ClearContent();
                response.Clear();
                response.ClearHeaders();
                response.ContentType = "APPLICATION/OCTET-STREAM";
                response.AppendHeader("Cache-Control", "maxage=0");
                response.AppendHeader("Pragma", "public");
                response.AppendHeader("Content-Disposition", "Attachment; Filename=layout.data");

                string storeFile = Model.Context.Request.MapPath("~/temp.data");
                using (FileStream fileStream = File.Create(storeFile))
                {
                    OlapAnalysis.SaveCompressed(fileStream, TStreamContent.All);
                }

                response.WriteFile(storeFile);
                response.Flush();
                response.End();

                File.Delete(storeFile);

                e.Handled = true;
            }

            //Load layout
            if (e.Item != null && e.Item.ButtonID == "c706b091-e49e-4e17-b28d-89dbbab1ce20")
            {
                var fs = Model.Context.Request.Files;
                if (fs.Count > 0)
                {
                    if (fs[0].FileName.ToLower().EndsWith(".data"))
                    {
                        OlapAnalysis.Load(fs[0].InputStream);
                        e.CallbackData = CallbackData.Toolbox;
                        e.PostbackData = PostbackData.OlapGridContainer | PostbackData.FilterGrid;
                        OlapAnalysis.CellSet.Rebuild();
                        OlapAnalysis.ApplyChanges();
                    }
                }

            }
        }

        protected override void InitSample()
        {
            OlapAnalysis.Width = new Unit("100%");
            OlapAnalysis.ShowModificationAreas = false;
            OlapAnalysis.ShowLegends = false;

            OlapAnalysis.CallbackController = "OlapAnalysis";
            OlapAnalysis.CallbackAction = "CallbackHandler";
            OlapAnalysis.ExportController = "OlapAnalysis";
            OlapAnalysis.ExportAction = "ExportHandler";

            OlapAnalysis.AddCalculatedMeasureButton.Visible = false;
            OlapAnalysis.ClearLayoutButton.Visible = false;
            OlapAnalysis.PivotAreaButton.Visible = false;
            OlapAnalysis.AllAreasButton.Visible = false;
            OlapAnalysis.DataAreaButton.Visible = false;
            OlapAnalysis.ModeButton.Visible = false;
            OlapAnalysis.ResizingButton.Visible = false;
            OlapAnalysis.DelayPivotingButton.Visible = false;
            OlapAnalysis.MeasurePlaceButton.Visible = false;
            OlapAnalysis.ExportCSVButton.Visible = false;
            OlapAnalysis.ExportHTMLButton.Visible = false;
            OlapAnalysis.ExportJPEGButton.Visible = false;
            OlapAnalysis.ExportPDFButton.Visible = false;
            OlapAnalysis.ExportXLSButton.Visible = false;
            OlapAnalysis.ExportXLSXButton.Visible = false;

            OlapAnalysis.SaveLayoutButton.Image = "/Content/images/save.png";
            OlapAnalysis.LoadLayoutButton.Image = "/Content/images/open.png";

            OlapAnalysis.CustomButtons.Clear();
            MvcCustomToolboxButton b = new MvcCustomToolboxButton();
            b.ButtonID = "CustomButton_1";
            b.Text = "Custom button";
            b.Tooltip = "Custom button";
            b.ClientScript = "alert('Custom button has been clicked.')";
            OlapAnalysis.CustomButtons.Add(b);

        }

    }
}