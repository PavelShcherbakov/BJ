import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-bot-hand',
  templateUrl: './bot-hand.component.html',
  styleUrls: ['./bot-hand.component.scss']
})
export class BotHandComponent implements OnInit {

  constructor() { }

  @Input() name: string;
  @Input() cardsInHand: number;
  ngOnInit() {
  }

}
