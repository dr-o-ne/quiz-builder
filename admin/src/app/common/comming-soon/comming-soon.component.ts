import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '../../../@vex/animations/fade-in-up.animation';

@Component({
  selector: 'vex-comming-soon',
  templateUrl: './comming-soon.component.html',
  styleUrls: ['./comming-soon.component.scss'],
  animations: [
    fadeInUp400ms
  ]
})
export class CommingSoonComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
