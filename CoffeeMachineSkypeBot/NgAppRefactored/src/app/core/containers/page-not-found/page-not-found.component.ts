import { Component, ChangeDetectionStrategy } from '@angular/core';
import { Location } from '@angular/common';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';

@Component({
  selector: 'page-not-found',
  template: `
    <div class="pageNotFound">
    	<div class="pageNotFound-content">
      	<h1>{{ title }}</h1>
      	<h3>{{ subTitle }}</h3>
      	<button (click)="goBack()">{{ btnName }}</button>
    	</div>
    </div>
  `,
  styleUrls: ['./page-not-found.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class PageNotFoundComponent implements OnInit {

  public title: string;
  public subTitle: string;
  public btnName: string;

  constructor(private location: Location) {}

  ngOnInit() {
    this.title = 'Page was not found.';
    this.subTitle = 'Pageyou are navigated on was not found.';
    this.btnName = 'Go Back';
  }

  public goBack() {
    this.location.back();
  }
}
