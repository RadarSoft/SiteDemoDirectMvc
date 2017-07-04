import { Component, OnInit, ViewChild, ElementRef, Input  } from '@angular/core';
import { Response } from '@angular/http';

import { SamplesService } from './samples.service';
import 'rxjs/add/operator/map';
import { Samples } from './app.component';
import { SamplesMenuComponent } from './samplesmenu.component';


@Component({
    selector: 'description-comp',
    template: `
<div [style.display]="panelVisibility==true?'block':'none'" class="panel panel-default description-panel">
    <div class="panel-heading">
        <h3 class="panel-title">Description</h3>
    </div>
    <div class="panel-body" #descriptionContainer>
       <div class='ajax-loader'></div>
    </div>
</div> 
`,
    providers: [SamplesService]
})

export class DescriptionComponent implements OnInit {
    @ViewChild("descriptionContainer")
    descriptionContainer: ElementRef;

    panelVisibility: boolean = true;

    _sampleForDescription: Samples;

    @Input()
    set sampleForDescription(sample: Samples) {
        if (this._sampleForDescription == sample)
            return;

        this._sampleForDescription = sample;

        switch (this._sampleForDescription) {
            case Samples.Mvc:
            case Samples.InitControls:
            case Samples.CubeStructure:
            case Samples.Export:
            case Samples.Pivoting:
            case Samples.Callbacks:
            case Samples.Angular2:
                this.panelVisibility = false;
                this.descriptionContainer.nativeElement.innerHTML = "";
                break;
            default:
                this.panelVisibility = true;
                this.loadDescription();
                break;
        }

    }

    get sampleForDescription() {
        return this._sampleForDescription;
    }

    constructor(private service: SamplesService) { }

    ngOnInit() {
    }

    loadDescription() {
        this.service.loadDescription("/SamplesDescriptions/" + Samples[this._sampleForDescription] + ".html").subscribe(
            result => {
                this.descriptionContainer.nativeElement.innerHTML = result.text();
            }
        );
    }
}