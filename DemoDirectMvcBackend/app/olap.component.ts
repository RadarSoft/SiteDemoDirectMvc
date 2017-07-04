import { Component, OnInit, ViewChild, ElementRef, Input  } from '@angular/core';
import { Response } from '@angular/http';

import { SamplesService } from './samples.service';
import 'rxjs/add/operator/map';
import { Samples } from './app.component';


@Component({
    selector: 'olap-comp',
    templateUrl: 'app/olap.component.html',
    providers: [SamplesService]
})

export class OlapComponent implements OnInit {

    @ViewChild("radarCubeContainer")
    radarCubeContainer: ElementRef;

    panelVisibility: boolean = true;

    _sampleForOlap: Samples;

    @Input()
    set sampleForOlap(sample: Samples) {
        if (this._sampleForOlap == sample)
            return;

        this._sampleForOlap = sample;
        this.loadSample();
    }

    get sampleForOlap() { return this._sampleForOlap; }

    constructor(private olapService: SamplesService) { }

    ngOnInit() {
    }

    loadSample() {
        let childElement = this.radarCubeContainer.nativeElement.children[0];
        if (!childElement.classList.contains('ajax-loader'))
            this.radarCubeContainer.nativeElement.insertAdjacentHTML("beforeend", "<div class='ajax-loader-olap'></div>");

        switch (this._sampleForOlap) {
            case Samples.DataSource:
                $("#samplesContainer").addClass("samples-container");
                this.radarCubeContainer.nativeElement.innerHTML = "<img src='Content/images/tables.png'/>";
                break;
            case Samples.Mvc:
            case Samples.InitControls:
            case Samples.CubeStructure:
            case Samples.Export:
            case Samples.Pivoting:
            case Samples.Callbacks:
            case Samples.Angular2:
                this.loadDescription();
                break;
            default:
                var sample = this._sampleForOlap;

                this.olapService.loadRadarCube(sample).subscribe(
                    (jsonResponce) => {
                        this.renderRadarCube(jsonResponce);
                    }
                );
                break;
        }
    }

    loadDescription() {
        $("#samplesContainer").removeClass("samples-container");
        this.olapService.loadDescription("/SamplesDescriptions/" + Samples[this._sampleForOlap] + ".html").subscribe(
            result => {
                this.radarCubeContainer.nativeElement.innerHTML = result.text();
            }
        );
    }

    renderRadarCube(responce: any) {
        $("#samplesContainer").addClass("samples-container");
        this.radarCubeContainer.nativeElement.innerHTML = responce.data;
        var grid = new RadarSoft.MvcOlapAnalysis();
        grid.initialize(responce.settings);
    }
}