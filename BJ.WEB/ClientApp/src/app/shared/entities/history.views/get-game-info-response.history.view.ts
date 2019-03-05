export class GetGameInfoHistoryResponseView {
    public steps: StepGetGameInfoHistoryResponseViewItem[];
    public summary: PlayersSummaryGetGameInfoHistoryResponseViewItem[];
    constructor() {
        this.steps = [];
        this.summary = [];
    }
}

export class StepGetGameInfoHistoryResponseViewItem {
    public playerInfo: PlayerInfoGetGameInfoHistoryResponseViewItem[];
    constructor() {
        this.playerInfo = [];
    }
}

export class PlayerInfoGetGameInfoHistoryResponseViewItem {
    public name: string;
    public card: CardGetGameInfoHistoryResponseView;

}

export class CardGetGameInfoHistoryResponseView {
    public suit: SuitGetGameInfoHistoryResponseView;
    public rank: RankGetGameInfoHistoryResponseView;
}

export class SuitGetGameInfoHistoryResponseView {
    public suit: number;
    public suitAsString: string;
}

export class RankGetGameInfoHistoryResponseView {
    public rank: number;
    public rankAsString: string;
}

export class PlayersSummaryGetGameInfoHistoryResponseViewItem {
    public name: string;
    public points: number;
    public state: StateGetGameInfoHistoryResponseView;
}

export class StateGetGameInfoHistoryResponseView {
    public state: number;
    public stateAsString: string;
}
