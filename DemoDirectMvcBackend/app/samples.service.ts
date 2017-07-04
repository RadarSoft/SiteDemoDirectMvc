import { Injectable } from '@angular/core';
import { Http, Response, Headers, URLSearchParams, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Samples } from './app.component';

declare global {
    interface Window { ga: any; }
}

@Injectable()
export class SamplesService {

    constructor(private http: Http) {
        (window).ga = (window).ga || function () { ((window).ga.q = (window).ga.q || []).push(arguments) }; (window).ga.l = +new Date;

        (window).ga('create', 'UA-149311-8', 'auto');
        (window).ga('require', 'cleanUrlTracker');
        (window).ga('require', 'eventTracker');
        (window).ga('require', 'outboundLinkTracker');
        (window).ga('require', 'urlChangeTracker');
    }

    loadRadarCube(sample: Samples) {
        return this.http.post('/api/radarcube/' + Samples[sample], "")
            .map((resp: Response) => resp.json());
    }

    getSamplesMenu() {
        return this.http.get('samples-tree.json');
    }

    getSamplesTabs() {
        return this.http.get('samples-tabs.json');
    }

    loadTabContent(path: string) {
        return this.http.get(path);
    }

    loadDescription(path: string) {
        return this.http.get(path);
    }

    pageView(url: string) {
        (window).ga('set', 'page', url);
        (window).ga('send', 'pageview');
    }

    //to track clicks on html attributes
    //ga-on="click"
}
