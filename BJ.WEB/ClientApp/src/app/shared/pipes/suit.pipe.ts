import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'suit'
})
export class SuitPipe implements PipeTransform {
    transform(value: number, args?: any): string {

        let result: string;
        switch (value) {
            case 0: {
                result = '♥';
                break;
            }
            case 1: {
                result = '♣';
                break;
            }
            case 2: {
                result = '♦';
                break;
            }
            case 3: {
                result = '♠';
                break;
            }
            default: {
                result = 'It is not suit';
                break;
            }
        }
        return result;


    }
}
