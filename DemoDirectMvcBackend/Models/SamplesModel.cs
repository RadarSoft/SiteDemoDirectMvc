using System;
using System.Collections.Generic;
using System.Web;
using SamplesFactory;

namespace Models
{
    public class SamplesModel
    {
        public SamplesModel(Samples sampleType = Samples.None)
        {
            Product = "RadarCube for ASP.NET MVC (Direct Edition)";

            if (sampleType != Samples.None)
                SampleType = sampleType;
            Init();            
        }

        private void Init()
        {
            FillSamplesCreators();
        }

        protected virtual void FillSamplesCreators()
        {
        }

        protected Dictionary<Samples, SampleCreator> _SamplesCreators = new Dictionary<Samples, SampleCreator>();

        Sample _Sample = null;
        public Sample Sample
        {
            get
            {
                if (_Sample == null)
                {
                    var sampleCreator = _SamplesCreators[SampleType];
                    _Sample = sampleCreator.CreateSample(this);
                }
                return _Sample;
            }
        }

        public Samples SampleType {
            get
            {
                Samples? samples = Enum.Parse(typeof(Samples), Context.Session["SampleType"].ToString()) as Samples?;

                return samples == null ? Samples.None : samples.Value;
            }
            set
            {
                Context.Session["SampleType"] = value.ToString();
            }
        }


        protected HttpContextBase _Context;
        public virtual HttpContextBase Context
        {
            get { return _Context ?? (_Context = new HttpContextWrapper(HttpContext.Current)); }
        }

        public string Skin
        {
            get
            {
                return (Context.Session["Skin"] != null) ? Context.Session["Skin"].ToString() : "Base";
            }
            set
            {
                Context.Session["Skin"] = value;
            }
        }

        public string Product { get; set; }       

        public SampleCreator GetSampleCreator()
        {
            return _SamplesCreators[SampleType];
        }
    }

    public enum Samples
    {
        None,
        Testing,
        GettingStarted,
        DataSource,
        Mvc,
        InitControls,
        CubeStructure,
        Pivoting,
        Callbacks,
        Export, 
        Grid,
        Drilling,
        Sorting,
        Filtering,
        Grouping,
        CalculatedFields,
        MeasureModes,
        CellContentCustomization,
        ColorModifications,
        ContextMenu,
        InfoAttributes,
        CellComments,
        Toolbox,
        Chart,
        ChartTypes,
        MultiSeriesChart,
        SeriesModifications,
        Angular2
    }
}