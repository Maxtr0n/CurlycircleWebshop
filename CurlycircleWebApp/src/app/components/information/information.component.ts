import { Component, OnInit, ViewChild } from '@angular/core';

import { faFacebook, faInstagram, faYoutube } from '@fortawesome/free-brands-svg-icons';
import { MatAccordion } from '@angular/material/expansion';

@Component({
  selector: 'app-information',
  templateUrl: './information.component.html',
  styleUrls: ['./information.component.css']
})
export class InformationComponent implements OnInit {
  @ViewChild(MatAccordion) accordion!: MatAccordion;

  faFacebook = faFacebook;
  faInstagram = faInstagram;
  faYoutube = faYoutube;

  constructor() { }

  ngOnInit(): void {
  }

}
