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
    public suit: number;
    public rank: number;
}

export class PlayersSummaryGetGameInfoHistoryResponseViewItem {
    public name: string;
    public points: number;
    public state: number;
}
