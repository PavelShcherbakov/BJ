export class GetGameInfoResponseHistoryView {
    public steps: StepGetGameInfoResponseHistoryViewItem[];
    public summary: PlayersSummaryGetGameInfoResponseHistoryViewItem[];
    constructor() {
        this.steps = [];
        this.summary = [];
    }
}

export class StepGetGameInfoResponseHistoryViewItem {
    public playerInfo: PlayerInfoGetGameInfoResponseHistoryViewItem[];
    constructor() {
        this.playerInfo = [];
    }
}

export class PlayerInfoGetGameInfoResponseHistoryViewItem {
    public name: string;
    public card: CardGetGameInfoResponseHistoryView;

}

export class CardGetGameInfoResponseHistoryView {
    public suit: SuitGetGameInfoResponseHistoryView;
    public rank: RankGetGameInfoResponseHistoryView;
}

export class SuitGetGameInfoResponseHistoryView {
    public suit: number;
    public suitAsString: string;
}

export class RankGetGameInfoResponseHistoryView {
    public rank: number;
    public rankAsString: string;
}

export class PlayersSummaryGetGameInfoResponseHistoryViewItem {
    public name: string;
    public points: number;
    public state: StateGetGameInfoResponseHistoryView;
}

export class StateGetGameInfoResponseHistoryView {
    public state: number;
    public stateAsString: string;
}
