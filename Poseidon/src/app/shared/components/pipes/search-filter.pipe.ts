import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'searchFilter'
})
export class SearchFilterPipe implements PipeTransform {

  transform(value: any, arg: any): any {
    if(!value) {
      return null;
    }
    if(!arg){
      return value;
    }
    const result = [];
    for (const i of value) {
      if (i.name != null && i.name.toLowerCase().indexOf(arg.toLowerCase()) > -1 || i.ip != null && i.ip.toLowerCase().indexOf(arg.toLowerCase()) > -1 
        || i.ports != null && i.ports.toString().toLowerCase().indexOf(arg.toLowerCase()) > -1 || i.dateInsert != null && i.dateInsert.toLowerCase().indexOf(arg.toLowerCase()) > -1 
        || i.queryString != null && i.queryString.toLowerCase().indexOf(arg.toLowerCase()) > -1 || i.type != null && i.type.toString().toLowerCase().indexOf(arg.toLowerCase()) > -1
        || i.location != null && i.location.toLowerCase().indexOf(arg.toLowerCase()) > -1 || i.os != null && i.os.toLowerCase().indexOf(arg.toLowerCase()) > -1
        || i.dateModify != null && i.dateModify.toLowerCase().indexOf(arg.toLowerCase()) > -1 || i.dateDisable != null && i.dateDisable.toLowerCase().indexOf(arg.toLowerCase()) > -1
        || i.active.toString().toLowerCase().indexOf(arg.toLowerCase()) > -1) {
        result.push(i);
      }
    }
    return result;
  }

}
