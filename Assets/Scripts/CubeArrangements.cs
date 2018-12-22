using System.Collections.Generic;
using UnityEngine;

// stores all possible groups of category/cube maps for the experiment
// one map group is picked for each participant - this is what the participant has to learn
public class CubeArrangements
{
    private string name { get; set; }
    private List<List<CubeTuple>> Arrangements { get; set; }
   
    public CubeArrangements(string arrangements)
    {
        // Unity's json parser can only accept an object as input for some reason
        // but to make it easier to use this data in matlab it was made into arrays of arrays
        Arrangements = 
            JsonUtility.FromJson<List<List<CubeTuple>>>("{\"arrangements\":" + arrangements+"}");
        foreach (List<CubeTuple> arr in Arrangements)
        {
            Debug.Log(string.Format("we have {0} options", arr.Count));
        }
        
    }

    public int CountArrangements()
    {
        return Arrangements.Count;
    }

    public int CountCubes(int arrangement)
    {
        return Arrangements[arrangement].Count;
    }

    public List<CubeTuple> Shuffle(int arrangement)
    {
        List<CubeTuple> arr = Arrangements[arrangement];
        CubeTuple[] shuffled = new CubeTuple[arr.Count];
        int i = 0;
        foreach (CubeTuple c in arr)
        {
            shuffled[i] = c;
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