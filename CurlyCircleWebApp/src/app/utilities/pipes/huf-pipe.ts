import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: 'huf' })
export class HufPipe implements PipeTransform {
    transform(value: number): string {
        let valueAsString = value.toString();

        for (let i = valueAsString.length - 3; i > 0; i -= 3) {
            valueAsString = valueAsString.substring(0, i) + ' ' + valueAsString.substring(i);
        }

        return `${valueAsString} Ft`;
    }
}