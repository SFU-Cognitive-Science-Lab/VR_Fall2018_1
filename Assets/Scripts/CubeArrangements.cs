using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;


// stores all possible groups of category/cube maps for the experiment
// one map group is picked for each participant - this is what the participant has to learn
public class CubeArrangements
{
    // Arrangements == our set of counterbalancing conditions
    // first list is the set of counterbalancing arrangements of cat -> cubes
    // second list is one cat -> cube mapping
    // third list is the 3 pieces of data needed to make one cube
    // ColorShapeRotation describes the properties of one axis (l/r, t/b, f/b)
    // data may be somewhat massaged to make it easier to work with in matlab
    // for example the category is saved redundantly for each dimension
    // this may change ... example:
    // [[[{"cat":"c0","color":"r","rotation":0,"shape":"O"},{"cat":"c0","color":"b","rotation":120,"shape":"A"},{"cat":"c0","color":"g","rotation":240,"shape":"@"}], ...
    private List<List<List<ColorShapeRotation>>> Arrangements { get; set; }

    // the arrangements string comes from a JSON data file 
    // see experiments.config.txt arrangements key or DataFarmer for where this is
    // the file is derived from an external web application
    public CubeArrangements(string arrangements)
    {
        Arrangements = JsonConvert.DeserializeObject<List<List<List<ColorShapeRotation>>>>(arrangements);
        foreach (List<List<ColorShapeRotation>> arr in Arrangements)
        {
            Debug.Log(string.Format("we have {0} options", arr.Count));
        }
    }

    public bool IsEmpty()
    {
        return (Arrangements.Count == 0);
    }

    public int CountArrangements()
    {
        return Arrangements.Count;
    }

    public int CountCubes(int arrangement)
    {
        if (IsEmpty()) return 0;
        return Arrangements[arrangement].Count;
    }

    public List<CubeTuple> Shuffle(int arrangement)
    {
        List<List<ColorShapeRotation>> arr = Arrangements[arrangement];
        CubeTuple[] shuffled = new CubeTuple[arr.Count];
        int i = 0;
        foreach (var c in arr)
        {
            shuffled[i] = new CubeTuple(c);
            i++;
        }
        System.Random random = new System.Random();
        for (i=0; i<arr.Count; i++)
        {
            var replace = random.Next(arr.Count);
            var temp = shuffled[replace];
            shuffled[replace] = shuffled[i];
            shuffled[i] = temp;
        }
        return new List<CubeTuple>(shuffled);
    }
}