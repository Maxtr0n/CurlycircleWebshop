import { Component, OnInit } from '@angular/core';

import { faFacebook, faInstagram, faYoutube } from '@fortawesome/free-brands-svg-icons';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  isExpanded = false;

  faFacebook = faFacebook;
  faInstagram = faInstagram;
  faYoutube = faYoutube;

  constructor() { }

  ngOnInit(): void { }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

}
