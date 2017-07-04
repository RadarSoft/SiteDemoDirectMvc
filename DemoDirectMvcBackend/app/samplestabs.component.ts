import { Component, OnInit, AfterContentChecked, ViewChild, ElementRef, Input, NgZone  } from '@angular/core';
import { Response } from '@angular/http';

import { SamplesService } from './samples.service';
import { Samples } from './app.component';


@Component({
    selector: 'tabs-comp',
    templateUrl: 'app/samplestabs.component.html',
    providers: [SamplesService]
})

export class SamplesTabsComponent implements OnInit, AfterContentChecked {

    _samplesTabs: SampleTabs[] = [];
    _sampleForTabs: Samples;
    _tabsHighLighted = false;

    @ViewChild("tabsContainer")
    tabsContainer: ElementRef;

    panelVisibility: boolean = true;

    sampleTabs: SampleTab[] = [];

    @Input()
    set sampleForTabs(sample: Samples) {
        if (this._sampleForTabs == sample)
            return;

        this._sampleForTabs = sample;

        if (this._samplesTabs.length > 0)
            this.initTabsForSample();
    }

    get sampleForTabs() {
        return this._sampleForTabs;
    }

    constructor(private service: SamplesService, private _ngZone: NgZone) {
        window['SamplesTabsComponentRef'] = { component: this, zone: _ngZone };
    }

    ngOnInit() {
        this.service.getSamplesTabs()
            .subscribe(
            result => {
                this._samplesTabs = result.json();
                this.initTabsForSample();
            }
        );  
    }

    ngAfterContentChecked(): void {
        this.highLightContent();
    }

    highLightContent() {
        if (this.sampleTabs.length == 0 || this._tabsHighLighted)
            return;

        if ($('pre code').length == 0)
            return;

        let doHighLight = true;
        $('pre code').each(function (i, block) {
            if ($(this).html() == "" || $(this).hasClass("hljs")) {
                doHighLight = false;
                return false;
            }
        });

        if (!doHighLight)
            return;

        let st: SampleTab;

        $('pre code').each(function (i, block) {
            window["hljs"].highlightBlock(block);
        });

        this._tabsHighLighted = true;

        $('a[data-toggle="tab"]').on('shown.bs.tab', { tabs_comp: this}, function (e) {
            var tab = $(e.target).text();
            tab = tab.replace(" ", "_");
            e.data.tabs_comp.service.pageView('/' + Samples[e.data.tabs_comp._sampleForTabs] + '/' + tab);
        })
    }

    loadContent(sampleTab: SampleTab) {
        if (sampleTab.file != "") {
            let sample: string = sampleTab.sample != null ? sampleTab.sample : Samples[this._sampleForTabs];
            this.service.loadTabContent("/SamplesTabs/" + sample + "/" + sampleTab.file).subscribe(
                result => {
                    sampleTab.content = result.text();
                }
            );
        }
    }

    initTabsForSample() {
        this._tabsHighLighted = false;
        this.panelVisibility = true;

        let sts: SampleTabs;
        for (var i = 0; i < this._samplesTabs.length; i++) {
            sts = this._samplesTabs[i];
            if (sts.sample == Samples[this._sampleForTabs]) {
                this.sampleTabs = sts.tabs;
                let st: SampleTab;
                for (var i = 0; i < this.sampleTabs.length; i++) {
                    st = this.sampleTabs[i];
                    if (st.content == "") {
                        this.loadContent(st);
                    }
                }

                return;
            }
        }

        this.sampleTabs = [];
        this.panelVisibility = false;
    }
}

export class SampleTabs {
    sample: string;
    tabs: SampleTab[] = [];
}

export class SampleTab {
    title: string;
    file: string = "";
    code: boolean;
    active: boolean;
    content: string = "";
    sample: string;
}