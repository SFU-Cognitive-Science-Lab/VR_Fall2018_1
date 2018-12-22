using System;
using System.Collections.Generic;

public class ColorShapeRotation
{
    public string cat { get; set; }
    public string color { get; set; }
    public string shape { get; set; }
    public int rotation { get; set; }

    public static readonly Dictionary<string, int> matmap = new Dictionary<string, int> {
            { "V", 1 },
            { "A", 2 },
            { "@", 3 },
            { "=", 4 },
            { "D", 5 },
            { "O", 6 },
        };

    public int[] GetFaces()
    {
        switch(rotation)
        {
            case 0: return new int[] { 5, 6 };
            case 120: return new int[] { 1, 3 };
            case 240: return new int[] { 2, 4 };
            default: throw new Exception("Unknown rotation should be 0, 120 or 240!");
        }
    }

    public int GetMaterial()
    {
        return matmap[shape];
    }
}