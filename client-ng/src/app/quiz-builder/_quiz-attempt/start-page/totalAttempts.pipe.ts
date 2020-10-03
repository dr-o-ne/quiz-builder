import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'formatTotalAttempts' })
export class TotalAttemptsPipe implements PipeTransform {

    transform(value: number): string {

        if(value > 100000)
            return "unlimited"

        return value.toString();
    }

}