export class TableModel {
    public bots: Bot[];
    public user: User;

    constructor() {
        this.bots = [];
// tslint:disable-next-line: no-use-before-declare
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
    public state: number;

    constructor() {
        this.cards = [];
    }
}

export class Card {
    public suit: number;
    public rank: number;
}
