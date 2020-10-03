import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'formatTotalAttempts' })
export class TotalAttemptsPipe implements PipeTransform {

    transform(value: number): string {
        return (value > 100000) ? "unlimited" : value.toString();
    }

}