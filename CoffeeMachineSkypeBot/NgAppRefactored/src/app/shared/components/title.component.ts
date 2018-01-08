import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'data-title',
  templateUrl: './title.component.html'
})
export class TitleComponent implements OnInit {
  title: string;

  ngOnInit() {
      this.title = "Some title";
  }
}