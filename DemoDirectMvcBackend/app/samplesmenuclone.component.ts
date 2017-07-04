import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output  } from '@angular/core';
import { Response } from '@angular/http';
import { SamplesService } from './samples.service';
import 'rxjs/add/operator/map';
import { Samples } from './app.component';

@Component({
    selector: 'samples-menu-clone-comp',
    template: `<div class="samples-menu-tree-clone" #samplesMenuCloneContainer></div>`,
    providers: [SamplesService]
})

export class SamplesMenuCloneComponent implements OnInit {

    @ViewChild("samplesMenuCloneContainer")
    menuContainer: ElementRef;

    constructor(private menuService: SamplesService) {
    }

    ngOnInit() {
        this.menuContainer.nativeElement.innerHTML = "<div class='ajax-loader'></div>";
        this.menuService.getSamplesMenu().subscribe(
            result => {
                this.menuContainer.nativeElement.innerHTML = "";
                $('.samples-menu-tree-clone')
                    .treeview({
                        data: result.json(),
                        expandIcon: 'glyphicon glyphicon-menu-right',
                        collapseIcon: 'glyphicon glyphicon-menu-down'
                    });

                $('.samples-menu-tree-clone')
                    .treeview('clearSearch')
                    .on('nodeSelected', function (event, data) {
                        window['SamplesMenuComponentRef'].component.changeSample(Samples[data.sample]);
                    });
            }
        );

    }
}