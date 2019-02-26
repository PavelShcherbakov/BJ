export class GetStateResponseGameView {
    public bots: BotGetStateGameResponseViewItem[];
    public user: UserGetStateGameResponseView;
}

export class BotGetStateGameResponseViewItem {
    public name: string;
    public cardsInHand: number;
}

export class UserGetStateGameResponseView {
    public name: string;
    public cards: CardGetStateGameResponseViewItem[];
    public state: number;
}

export class CardGetStateGameResponseViewItem {
    public suit: number;
    public Rank: number;
}
