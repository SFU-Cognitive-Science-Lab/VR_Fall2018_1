using System.Collections.Generic;

public class CubeTuple
{
    public List<ColorShapeRotation> cube { get; set; }

    public static readonly Dictionary<string, string> catmap = new Dictionary<string, string>
    {
        { "c0", "A" },
        { "c1", "B" },
        { "c2", "C" },
        { "c3", "D" },
    };

    public string GetCategory()
    {
        return catmap[cube[0].cat];
    }
}