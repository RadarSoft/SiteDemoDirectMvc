using System;
using System.Web.Http;
using RadarSoft.RadarCube.Web;
using Models;

namespace Controllers
{
    public class RadarCubeController : ApiController
    {
        public JSONResponse Post(string id)
        {
            var sm = new ROlapSamplesModel((Samples)Enum.Parse(typeof(Samples), id));
            return sm.OlapAnalysis.AjaxLoading();
        }
    }
}
