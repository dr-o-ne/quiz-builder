import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'enumToArray' })
export class EnumToArrayPipe implements PipeTransform {
    transform(value: { [x: string]: any; }): Object {
        return Object.keys(value).filter(x => !isNaN(+x)).map(x => { return { index: +x, name: value[x] } });
    }
}