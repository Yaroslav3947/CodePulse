import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'timeAgo',
  pure: true
})
export class TimeAgoPipe implements PipeTransform {

  transform(value: Date, ...args: unknown[]): unknown {
    return moment(value).fromNow();
  }

}
