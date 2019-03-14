import { Guid } from 'guid-typescript';

export class GetStateResponseGameView {
    public bots: BotGetStateResponseGameViewItem[];
    public user: UserGetStateResponseGameView;
    public gameId: Guid;
}

export class BotGetStateResponseGameViewItem {
    public name: string;
    public cardsInHand: number;
}

export class UserGetStateResponseGameView {
    public name: string;
    public cards: CardGetStateResponseGameViewItem[];
    public state: StateGetStateResponseGameView;
}

export class StateGetStateResponseGameView {
    public state: number;
    public stateAsString: string;
}

export class CardGetStateResponseGameViewItem {
    public suit: number;
    public rank: number;
}
