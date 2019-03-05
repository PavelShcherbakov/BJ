import { Guid } from 'guid-typescript';

export class GetStateResponseGameView {
    public bots: BotGetStateGameResponseViewItem[];
    public user: UserGetStateGameResponseView;
    public gameId: Guid;
}

export class BotGetStateGameResponseViewItem {
    public name: string;
    public cardsInHand: number;
}

export class UserGetStateGameResponseView {
    public name: string;
    public cards: CardGetStateGameResponseViewItem[];
    public state: StateGetStateGameResponseView;
}

export class StateGetStateGameResponseView {
    public state: number;
    public stateAsString: string;
}

export class CardGetStateGameResponseViewItem {
    public suit: number;
    public rank: number;
}
