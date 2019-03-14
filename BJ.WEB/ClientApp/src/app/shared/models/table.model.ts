import { Guid } from 'guid-typescript';

export class TableModel {
    public bots: Bot[];
    public user: User;
    public gameId: Guid;

    constructor() {
        this.bots = [];
        this.user = new User();
    }
}

export class Bot {
    public name: string;
    public cardsInHand: number;
}

export class User {
    public name: string;
    public cards: Card[];
    public state: State;

    constructor() {
        this.cards = [];
        this.state = new State();
    }
}

export class State {
    public state: number;
    public stateAsString: string;
}

export class Card {
    public suit: number;
    public rank: number;
}
