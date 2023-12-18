namespace Day02;

public class Game
{
    public required int Id { get; init; }
    public required Set[] Sets { get; init; }

    public int Total(GameColor color) => color switch
    {
        GameColor.Red => Sets.Sum(s => s.Red),
        GameColor.Green => Sets.Sum(s => s.Green),
        GameColor.Blue => Sets.Sum(s => s.Blue),
        _ => throw new ArgumentOutOfRangeException(nameof(color))
    };
};

public record Set
{
    public required int Red { get; init; }
    public required int Green { get; init; }
    public required int Blue { get; init; }
}

public enum GameColor
{
    Red,
    Green,
    Blue
}