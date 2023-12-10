namespace Common;

public static class MathExtensions
{
    public static long LeastCommonMultiple(long left, long right)
    {
        var greater = Math.Max(left, right);
        var smallest = Math.Min(left, right);
        var result = greater;
        while (result % smallest != 0)
        {
            result += greater;
        }

        return result;
    }
}
