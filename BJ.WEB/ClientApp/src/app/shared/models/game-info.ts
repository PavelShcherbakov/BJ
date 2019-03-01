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
    public suit: number;
    public rank: number;
}

export class Sammury {
    public name: string;
    public points: number;
    public state: number;
}
