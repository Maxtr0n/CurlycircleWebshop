import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { faFacebook, faInstagram, faYoutube } from '@fortawesome/free-brands-svg-icons';

@Component({
    selector: 'app-socials',
    templateUrl: './socials.component.html',
    styleUrls: ['./socials.component.css']
})
export class SocialsComponent implements OnInit {

    faFacebook = faFacebook;
    faInstagram = faInstagram;
    faYoutube = faYoutube;

    submitted = false;


    constructor() { }

    ngOnInit(): void {
    }


    onSubmit() { this.submitted = true; }

}
