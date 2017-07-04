using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using RadarSoft.RadarCube.Common;
using RadarSoft.RadarCube.Web;
using RadarSoft.RadarCube.Web.Analysis;
using RadarSoft.RadarCube.Web.Mvc;

namespace SamplesFactory
{
    public abstract class Sample
    {
        public SamplesModel Model { get; set; }
        
        protected Sample(SamplesModel samplesModel)
        {
            Model = samplesModel;
        }

        protected abstract void InitOlapAnalysis();
        protected abstract void InitSample();
        protected abstract void DoActive();
        protected abstract void InitLayout();
    }
}