namespace Day07;

public record Hand
{

    public Hand(IEnumerable<char> cards, int bid)
    {
        Cards = [.. cards];
        Bid = bid;
        Type = GetHandType();
    }

    public char[] Cards { get; }
    public int Bid { get; }
    public HandType Type { get; }
    public int Rank { get; init; }
    public long Winnings { get; init; }
    private HandType GetHandType()
    {
        return Cards switch
        {
            var oneDistinct when oneDistinct.Distinct().Count() == 1 => HandType.FiveOfAKind,
            var twoDistinct when twoDistinct.Distinct().Count() == 2 => IsFourOfAKindOrFullHouse(),
            var threeDistinct when threeDistinct.Distinct().Count() == 3 => IsThreeOfAKindOrTwoPair(),
            var fourDistinct when fourDistinct.Distinct().Count() == 4 => HandType.OnePair,
            _ => HandType.HighCard
        };

        HandType IsFourOfAKindOrFullHouse()
        {
            var first = Cards.Count(x => x == Cards[0]);
            return first is 1 or 4 ? HandType.FourOfAKind : HandType.FullHouse;
        }

        HandType IsThreeOfAKindOrTwoPair()
        {
            var groupedCards = Cards.GroupBy(x => x);
            if (groupedCards.Any(g => g.Count() == 2)) return HandType.TwoPair;
            else return HandType.ThreeOfAKind;
        }
    }
}
public enum HandType
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind
}