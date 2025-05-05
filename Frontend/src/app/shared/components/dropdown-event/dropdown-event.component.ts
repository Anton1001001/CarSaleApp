import { Component } from '@angular/core';

import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzIconModule } from 'ng-zorro-antd/icon';

@Component({
  selector: 'app-dropdown-event',
  imports: [NzDropDownModule, NzIconModule],
  templateUrl: './dropdown-event.component.html'
})
export class DropdownEventComponent {
  log(data: string): void {
    console.log(data);
  }
}
