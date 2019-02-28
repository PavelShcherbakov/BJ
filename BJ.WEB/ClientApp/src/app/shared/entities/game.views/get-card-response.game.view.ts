export class GetCardResponseGameView {
    public bots: BotGetCardResponseGameViewItem[];
    public user: UserGetCardResponseGameView;
}

export class BotGetCardResponseGameViewItem {
    public name: string;
    public cardsInHand: number;
}

export class UserGetCardResponseGameView {
    public name: string;
    public cards: CardGetCardResponseGameViewItem[];
    public state: number;
}

export class CardGetCardResponseGameViewItem {
    public suit: number;
    public rank: number;
}
