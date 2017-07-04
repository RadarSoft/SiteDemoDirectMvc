import { Component, OnInit, OnDestroy, ViewChild, ElementRef, EventEmitter, Output, NgZone } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Response } from '@angular/http';
import { SamplesService } from './samples.service';
import 'rxjs/add/operator/map';
import { Samples } from './app.component';

@Component({
    selector: 'samples-menu-comp',
    templateUrl: 'app/samplesmenu.component.html',
    providers: [SamplesService]
})

export class SamplesMenuComponent implements OnInit, OnDestroy {

    @ViewChild("samplesMenuContainer")
    menuContainer: ElementRef;

    _defaultSample: Samples = Samples.GettingStarted;

    samplesTree: SampleNode[] = [];

    constructor(private menuService: SamplesService,
        private _ngZone: NgZone,
        private _activatedRoute: ActivatedRoute, private _router: Router) {
        window['SamplesMenuComponentRef'] = { component: this, zone: _ngZone };

        _activatedRoute.queryParams.subscribe(
            params => {
            let sampleArg: string = params['sample'];
            let sample: Samples = Samples[sampleArg];
            if (sample != null)
                this._defaultSample = sample;
        });
    }

    ngOnInit() {
        this.menuContainer.nativeElement.innerHTML = "<div class='ajax-loader'></div>";
        this.menuService.getSamplesMenu().subscribe(
            result => {
                this.menuContainer.nativeElement.innerHTML = "";
                this.samplesTree = result.json();
                $('.samples-menu-tree')
                    .treeview({
                        data: result.json(),
                        expandIcon: 'glyphicon glyphicon-menu-right',
                        collapseIcon: 'glyphicon glyphicon-menu-down'
                    });

                $('.samples-menu-tree')
                    .treeview('clearSearch')
                    .on('nodeSelected', function (event, data) {
                        window['SamplesMenuComponentRef'].component.changeSample(Samples[data.sample]);
                    });

                $('#search-button').click(function () {
                    var searcheValue = $('#search-input').val();
                    if (searcheValue) {
                        var nodes = $('.samples-menu-tree').treeview('search', [
                            searcheValue, {
                                ignoreCase: true, // case insensitive
                                exactMatch: false, // like or equals
                                revealResults: true // reveal matching nodes
                            }
                        ]);
                        if (nodes.length === 0) {
                            alert("There is no a sample with '" + searcheValue + "' in name!");
                        }
                    } else {
                        alert("Please input any value!");
                    }
                });

                $('#refresh-search').click(function () {
                    $('.samples-menu-tree').treeview('clearSearch');
                });

                $('#open-all-nodes').click(function () {
                    $('.samples-menu-tree').treeview('expandAll', { silent: true });
                });

                $('#collaps-all-nodes').click(function () {
                    $('.samples-menu-tree').treeview('collapseAll', { silent: true });
                });

                this.changeSample(this._defaultSample);
            }
        );

    }

    ngOnDestroy() {
        window['SamplesMenuComponentRef'] = null;
    }

    @Output() onMenuSampleChanged = new EventEmitter<Samples>();

    searchSampleNode(sample: Samples): any {

        //$('.samples-menu-tree').treeview('clearSearch');
        var nodes = $('.samples-menu-tree').treeview('search', [this.getSampleName(sample), {
            ignoreCase: false,     // case insensitive
            exactMatch: true,    // like or equals
            revealResults: true  // reveal matching nodes
        }]);

        $('.samples-menu-tree-clone').treeview('search', [this.getSampleName(sample), {
            ignoreCase: false,     // case insensitive
            exactMatch: true,    // like or equals
            revealResults: true  // reveal matching nodes
        }]);

        return nodes.length > 0 ? nodes[0] : null;
    }

    getSampleName(sample: Samples, nodes: SampleNode[] = null): string {
        let node: SampleNode; 
        if (nodes == null) {
            return this.getSampleName(sample, this.samplesTree);
        }

        for (var i = 0; i < nodes.length; i++) {
            node = nodes[i];
            if (node.sample == Samples[sample]) {
                return node.text;
            }

            if (node.nodes != null && node.nodes.length > 0) {
                let sn = this.getSampleName(sample, node.nodes);
                if (sn != null)
                    return sn;
            }
        }

        return null;
    }

    getSampleNode(sampleName: string, nodes: SampleNode[] = null): SampleNode | SampleNode[] {
        if (sampleName == "Samples")
            return this.samplesTree;

        let node: SampleNode;
        if (nodes == null) {
            return this.getSampleNode(sampleName, this.samplesTree);
        }

        for (var i = 0; i < nodes.length; i++) {
            node = nodes[i];
            if (node.text == sampleName) {
                return node;
            }

            if (node.nodes != null && node.nodes.length > 0) {
                let sn = this.getSampleNode(sampleName, node.nodes);
                if (sn != null)
                    return sn;
            }
        }

        return null;
    }


    changeSample(sample: Samples) {
        this.onMenuSampleChanged.emit(sample);
        $('.samples-menu-tree').treeview('selectNode', [this.searchSampleNode(sample).nodeId, { silent: true }]);
        $('.samples-menu-tree-clone').treeview('selectNode', [this.searchSampleNode(sample).nodeId, { silent: true }]);
        this.menuService.pageView('/' + Samples[sample]);
    }
}

export class SampleNode {
    text: string;
    sample: string;
    backColor: string;
    color: string;
    selectable: boolean;
    nodes: SampleNode[];
}