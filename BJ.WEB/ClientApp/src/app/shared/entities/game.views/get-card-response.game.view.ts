import { Guid } from 'guid-typescript';

export class GetCardResponseGameView {
    public bots: BotGetCardResponseGameViewItem[];
    public user: UserGetCardResponseGameView;
    public gameId: Guid;
}

export class BotGetCardResponseGameViewItem {
    public name: string;
    public cardsInHand: number;
}

export class UserGetCardResponseGameView {
    public name: string;
    public cards: CardGetCardResponseGameViewItem[];
    public state: StateGetCardResponseGameView;
}

export class StateGetCardResponseGameView {
    public state: number;
    public stateAsString: string;
}

export class CardGetCardResponseGameViewItem {
    public suit: number;
    public rank: number;
}
