import { Bot } from './../../shared/models/table.model';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-bot-hand',
  templateUrl: './bot-hand.component.html',
  styleUrls: ['./bot-hand.component.scss']
})
export class BotHandComponent implements OnInit {

  constructor() { }

  private loop: boolean[];

  @Input() bot: Bot;

  public ngOnInit(): void {
    this.loop = new Array<boolean>(this.bot.cardsInHand);
  }

}
