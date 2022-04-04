import { Component, OnDestroy } from "@angular/core";
import { Observable, Subscription } from "rxjs";

@Component({
    template: ''
})
export abstract class UnsubscribeOnDestroy implements OnDestroy {
    protected subscriptions: Subscription[] = [];

    ngOnDestroy(): void {
        for (const subscription of this.subscriptions) {
            subscription.unsubscribe();
        }
    }

    subscribe(observable: Observable<any>): void {
        const subscription = observable.subscribe();
        this.subscriptions.push(subscription);
    }
}