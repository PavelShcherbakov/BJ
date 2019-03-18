import { User } from './../../shared/models/table.model';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-user-hand',
  templateUrl: './user-hand.component.html',
  styleUrls: ['./user-hand.component.scss']
})
export class UserHandComponent {
  @Input() user: User = new User();
}
