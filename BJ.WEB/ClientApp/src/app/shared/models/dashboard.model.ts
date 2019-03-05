import { Guid } from 'guid-typescript';


export class Dashboard {
    games: Game[];
    constructor() {
        this.games = [];
    }
}

export class Game {
    gameId: Guid;
    creationDate: Date;
    numberOfPlayers: number;
    result: Result;
}

export class Result {
    public State: number;
    public StateAsString: string;
}
