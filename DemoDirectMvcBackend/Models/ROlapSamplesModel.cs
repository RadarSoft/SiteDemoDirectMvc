using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RadarSoft.RadarCube.Web.Mvc;
using SamplesFactory;

namespace Models
{
    public class ROlapSamplesModel : SamplesModel
    {
        public ROlapSamplesModel(Samples sampleType = Samples.None) : base(sampleType)
        {
        }

        public MvcOlapAnalysis OlapAnalysis
        {
            get { return ((RSample)Sample).OlapAnalysis; }
        }

        protected override void FillSamplesCreators()
        {
            var gettingStartedCreator = new GettingStartedSampleCreator();

            _SamplesCreators.Add(Samples.None, new RSampleCreator());
            _SamplesCreators.Add(Samples.Testing, new TestingSampleCreator());
            _SamplesCreators.Add(Samples.GettingStarted, gettingStartedCreator);
            _SamplesCreators.Add(Samples.Grid, gettingStartedCreator);
            _SamplesCreators.Add(Samples.Drilling, new DrillingSampleCreator());
            _SamplesCreators.Add(Samples.Sorting, new SortingSampleCreator());
            _SamplesCreators.Add(Samples.Filtering, new FilteringSampleCreator());
            _SamplesCreators.Add(Samples.Grouping, new GroupingSampleCreator());
            _SamplesCreators.Add(Samples.CalculatedFields, new CalculatedFieldsSampleCreator());
            _SamplesCreators.Add(Samples.CellContentCustomization, new CellContentCustomizationSampleCreator());
            _SamplesCreators.Add(Samples.ColorModifications, new ColorModificationsSampleCreator());
            _SamplesCreators.Add(Samples.MeasureModes, new MeasureModesSampleCreator());
            _SamplesCreators.Add(Samples.InfoAttributes, new InfoAttributesSampleCreator());
            _SamplesCreators.Add(Samples.CellComments, new CellCommentsSampleCreator());
            _SamplesCreators.Add(Samples.ContextMenu, new ContextMenuSampleCreator());
            _SamplesCreators.Add(Samples.Toolbox, new ToolboxSampleCreator());
            _SamplesCreators.Add(Samples.Chart, new ChartSampleCreator());
            _SamplesCreators.Add(Samples.ChartTypes, new ChartTypesSampleCreator());
            _SamplesCreators.Add(Samples.MultiSeriesChart, new MultiSeriesChartSampleCreator());
            _SamplesCreators.Add(Samples.SeriesModifications, new SeriesModificationsSampleCreator());
        }
    }
}       