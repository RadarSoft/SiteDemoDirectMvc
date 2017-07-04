using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using RadarSoft.RadarCube.Web.Mvc;

namespace Controllers
{
    public class OlapAnalysisController : Controller
    {
        public SamplesModel SampleModel { get; set; }
        public ViewResult Index()
        {
            SampleModel = new SamplesModel();
            return View(SampleModel);
        }

        [AjaxOnly]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CallbackHandler()
        {
            var model = new ROlapSamplesModel();
            return Json(model.OlapAnalysis.DoCallback(), JsonRequestBehavior.DenyGet);
        }

        public ViewResult ChangeSkin(string skin)
        {
            SampleModel = new ROlapSamplesModel(Samples.GettingStarted);
            SampleModel.Skin = skin;
            return View("Index", SampleModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public BinaryStreamResult ExportHandler()
        {
            var sm  = new ROlapSamplesModel();
            SampleModel = sm;
            return sm.OlapAnalysis.DoExport();
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult PaintTrend()
        {
            return new ImageStreamResult(); 
        }
    }
}
