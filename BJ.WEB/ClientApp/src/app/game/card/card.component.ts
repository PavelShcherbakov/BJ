import { Component, OnInit, Input } from '@angular/core';
import { Card } from 'src/app/shared/models/card';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent {
  @Input() card: Card;
}
