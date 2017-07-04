import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'demo-app',
    templateUrl: 'app/app.component.html',
})
export class AppComponent implements OnInit {
    sample: Samples;
    productName: string = "RadarCube for ASP.NET MVC (Direct Edition)";

    constructor() {
    }

    ngOnInit() {
    }

    menuSampleChanged(sample: Samples) {
        this.sample = sample;
    }
}

export enum Samples {
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
