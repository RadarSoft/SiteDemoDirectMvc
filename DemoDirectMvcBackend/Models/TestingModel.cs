using RadarSoft.RadarCube.Common;

namespace Models
{
    public class TestingModel : ROlapSamplesModel
    {
        private string _parameters;

        public TestingModel(Samples sampleType, string parameters) : base(sampleType)
        {
            _parameters = parameters;
        }

        public string GetJsonResponce()
        {
            string res = "";
            switch (_parameters)
            {
                case "savelayout":
                    OlapAnalysis.EnsureStateRestored();

                    OlapAnalysis.SaveUncompressed(Context.Request.MapPath("~/layout.data"), TStreamContent.All);
                    break;
                case "loadlayout":
                    //((RSample)Sample).DoActive();
                    OlapAnalysis.Load(Context.Request.MapPath("~/layout.data"));
                   
                    break;

            }
            return res;
        }
    }
}