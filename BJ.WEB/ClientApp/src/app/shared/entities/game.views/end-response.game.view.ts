import { Guid } from 'guid-typescript';
export class EndResponseGameView {
    public bots: BotEndResponseGameViewItem[];
    public user: UserEndResponseGameView;
    public gameId: Guid;
}

export class BotEndResponseGameViewItem {
    public name: string;
    public cardsInHand: number;
}

export class UserEndResponseGameView {
    public name: string;
    public cards: CardEndResponseGameViewItem[];
    public state: StateEndResponseGameView;
}

export class StateEndResponseGameView {
    public state: number;
    public stateAsString: string;
}

export class CardEndResponseGameViewItem {
    public suit: number;
    public rank: number;
}
