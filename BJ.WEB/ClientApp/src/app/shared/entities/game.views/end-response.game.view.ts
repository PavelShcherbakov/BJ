export class EndResponseGameView {
    public bots: BotEndResponseGameViewItem[];
    public user: UserEndResponseGameView;
}

export class BotEndResponseGameViewItem {
    public name: string;
    public cardsInHand: number;
}

export class UserEndResponseGameView {
    public name: string;
    public cards: CardEndResponseGameViewItem[];
    public state: number;
}

export class CardEndResponseGameViewItem {
    public suit: number;
    public rank: number;
}
