import { Guid } from 'guid-typescript';


export class GetAllGamesResponseHistoryView {
    games: GameGetAllGamesResponseHistoryViewItem[];
}

export class GameGetAllGamesResponseHistoryViewItem {
    gameId: Guid;
    creationDate: Date;
    numberOfPlayers: number;
    result: ResultGetAllGamesResponseHistoryViewItem;
}
export class ResultGetAllGamesResponseHistoryViewItem {
    public state: number;
    public stateAsString: string;
}
