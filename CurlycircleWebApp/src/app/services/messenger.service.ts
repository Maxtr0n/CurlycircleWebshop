import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { ProductViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class MessengerService {

    subject = new Subject()

    constructor() { }

    sendMessage(product: ProductViewModel) {
        this.subject.next(product);
    }

    getMessage() {
        return this.subject.asObservable();
    }
}
