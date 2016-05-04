public class DoubleString
{
    public string check(string S)
    {
        var i = S.Length / 2;
        return S.Substring(0, i) == S.Substring(i) ? "square" : "not square";
    }
}