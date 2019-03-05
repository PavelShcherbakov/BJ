export class GameInfo {
    public steps: Step[];
    public summary: Sammury[];
    constructor() {
        this.steps = [];
        this.summary = [];
    }
}

export class Step {
    public playerInfo: PlayerInfo[];
    constructor() {
        this.playerInfo = [];
    }
}

export class PlayerInfo {
    public name: string;
    public card: Card;
}

export class Card {
    public suit: Suit;
    public rank: Rank;
}

export class Suit {
    public suit: number;
    public suitAsString: string;
}

export class Rank {
    public rank: number;
    public rankAsString: string;
}


export class Sammury {
    public name: string;
    public points: number;
    public state: State;
}

export class State {
    public state: number;
    public stateAsString: string;
}

