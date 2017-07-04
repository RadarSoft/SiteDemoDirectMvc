import { Component, Input, ElementRef, ViewChild, OnInit, NgZone } from '@angular/core';
import { Samples } from './app.component';
import { SamplesMenuComponent, SampleNode } from './samplesmenu.component';


@Component({
    selector: 'breadcrumbs-comp',
    template: `<ol #breadCrumbsContainer id="breadcrumb-samples-menu" class="breadcrumb"></ol>`,
})
export class BreadCrumbsComponent implements OnInit {
    @ViewChild("breadCrumbsContainer")
    breadCrumbsContainer: ElementRef;

    _sampleForBreadCrumbs: Samples;

    constructor(private _ngZone: NgZone) {
        window['BreadCrumbsComponentRef'] = { component: this, zone: _ngZone };
    }

    ngOnInit() {
        //this.loadBreadCrumbs();
    }

    @Input()
    set sampleForBreadCrumbs(sample: Samples) {
        if (this._sampleForBreadCrumbs == sample)
            return;

        this._sampleForBreadCrumbs = sample;

        this.loadBreadCrumbs();
    }

    get sampleForBreadCrumbs() { return this._sampleForBreadCrumbs; }

    getSamplesMenuComponent(): SamplesMenuComponent {
        return window['SamplesMenuComponentRef'].component as SamplesMenuComponent;
    }

    getSamplesTree() {
        return this.getSamplesMenuComponent().samplesTree;
    }

    getSampleName() {
        return this.getSamplesMenuComponent().getSampleName(this._sampleForBreadCrumbs);
    }

    searchSampleNode() {
        return this.getSamplesMenuComponent().searchSampleNode(this._sampleForBreadCrumbs);
    }

    changeSample(sample: Samples) {
        return this.getSamplesMenuComponent().changeSample(sample);
    }

    loadBreadCrumbs() {
        if (this.getSamplesTree() == null)
            return;

        this.buildBreadcrumbs(this.searchSampleNode());
    }

    buildBreadcrumbs(sampleNode: any) {
        var nodes = [sampleNode];

        var parentNode = this.getParentNode(sampleNode);
        while (parentNode != null) {
            nodes.unshift(parentNode);
            parentNode = this.getParentNode(parentNode);
        }
        var bcLine = "<li><a href=\"javascript:void(0)\" class=\"breadcrumb-menu\">Samples</a></li>";
        for (var i = 0; i < nodes.length; i++) {
            if (i < nodes.length - 1) {
                bcLine += "<li><a href=\"javascript:void(0)\" class=\"breadcrumb-menu\">";
                bcLine += nodes[i].text;
                bcLine += "</a></li>";
            } else {
                bcLine += "<li class=\"active\">";
                bcLine += nodes[i].text;
                bcLine += "</li>";
            }
        }

        this.breadCrumbsContainer.nativeElement.innerHTML = bcLine;

        $.contextMenu({
            selector: '.breadcrumb-menu',
            trigger: 'none',
            selectableSubMenu: true,
            callback: function (key: string, options: any) {
                let samplesMenuComponent: SamplesMenuComponent = window['SamplesMenuComponentRef'].component;
                samplesMenuComponent.changeSample(Samples[key]);
            },
            build: function ($trigger: any, e: any) {
                e.preventDefault();
                return $trigger.data('makeMenu')($trigger);
            }
        });

        $('.breadcrumb-menu').on('mouseup', function (e) {
            var $this: any = $(this);
            $this.data('makeMenu', window['BreadCrumbsComponentRef'].component.createItemMenu);
            var _offset = $this.offset();
            var position =
                {
                    x: _offset.left,
                    y: _offset.top + 15
                }
            $this.contextMenu(position);
        });
    };

    getParentNode(node: any) {
        if (node.parentId != null)
            return $('.samples-menu-tree').treeview('getNode', node.parentId);
        else return null;
    };

    buildMenu(source: SampleNode|SampleNode[], menu: any) {
        let $this: BreadCrumbsComponent = this;

        if ($.isArray(source)){
            $.each(source, function (k, v) {
                $this.buildMenu(v, menu);
            });
            return;
        }

        let s = source as SampleNode;
        var sample = s.sample;
        var object = {};
        object[sample] = { "name": s.text, "items": {} };
        if (sample === Samples[$this._sampleForBreadCrumbs]) {
            object[sample].className = "selected-item";
        }
        if (s.nodes != null) {
            for (var i in s.nodes) {
                $this.buildMenu(s.nodes[i], object[sample]);
            }
        }
        else
            delete object[sample].items;

        $.extend(menu.items, object);
    }

    createItemMenu(item: any) {
        let $this: BreadCrumbsComponent = window['BreadCrumbsComponentRef'].component;
        let samplesMenuComp: SamplesMenuComponent = window['SamplesMenuComponentRef'].component;
        var node: SampleNode | SampleNode[] = samplesMenuComp.getSampleNode(item.html());
        var menu = { "items": {} }
        $this.buildMenu(node, menu);
        return menu;
    }
}
