using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using RadarSoft.RadarCube.Common;
using RadarSoft.RadarCube.Web.Analysis;
using RadarSoft.RadarCube.Web.Mvc;
using SamplesFactory;
using System.Web.UI.WebControls;

namespace SamplesFactory
{
    public class BaseGridSample : RSample
    {
        public BaseGridSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitSample()
        {
            base.InitSample();
            OlapAnalysis.AnalysisType = AnalysisType.Grid;
            OlapAnalysis.Width = new Unit("100%");
            OlapAnalysis.ShowModificationAreas = false;
            OlapAnalysis.ShowLegends = false;
        }
    }
}