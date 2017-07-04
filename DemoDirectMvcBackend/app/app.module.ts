import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { OlapComponent } from './olap.component';
import { SamplesMenuComponent } from './samplesmenu.component';
import { SamplesMenuCloneComponent } from './samplesmenuclone.component';
import { BreadCrumbsComponent } from './breadcrumbs.component';
import { DescriptionComponent } from './description.component';
import { SamplesTabsComponent } from './samplestabs.component';
 
const appRoutes: Routes = [
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpModule, RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, OlapComponent,
        BreadCrumbsComponent, SamplesMenuComponent, SamplesMenuCloneComponent,
        DescriptionComponent, SamplesTabsComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }