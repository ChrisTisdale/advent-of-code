namespace AdventOfCode2023.day7;

public class HandComparer : IComparer<Hand>
{
    public int Compare(Hand? left, Hand? right)
    {
        if (ReferenceEquals(left, right))
        {
            return 0;
        }

        if (left is null)
        {
            return -1;
        }

        if (right is null)
        {
            return 1;
        }

        var leftScore = left.CalculateScore();
        var rightScore = right.CalculateScore();
        return leftScore == rightScore ? ComparisonInOrder(left, right) : leftScore.CompareTo(rightScore);
    }

    private static int ComparisonInOrder(Hand left, Hand right)
    {
        for (var i = 0; i < left.Numbers.Count; ++i)
        {
            if (left.Numbers[i] != right.Numbers[i])
            {
                return left.Numbers[i].CompareTo(right.Numbers[i]);
            }
        }

        return 0;
    }
}
