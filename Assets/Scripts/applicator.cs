using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: tidy things up into a nice function

public class applicator : MonoBehaviour {
    public Material[] material;
    Renderer rend;
    
    void Start()
    {
        Cubes cubes = new Cubes();

        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.materials = createSet(int.Parse(GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<CustomTag>().getTag(0)), name.Replace("(Clone)", ""));
        Debug.Log("ID: " + int.Parse(GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<CustomTag>().getTag(0)));
    }

    void swap(ref int a, ref int b)
    {
        int  temp = a;
        a = b;
        b = temp;
        return;
    }


    /* material is an array containing the base and feature materials-
    INITIAL MATERIAL ORDER
    [0] = base layer 
    [1] = Red Diamond
    [2] = Red Omega
    [3] = Green Sprial
    [4] = Green Bamboo
    [5] = Blue Downward Triangle
    [6] = Blue Upward Triangle
    */

    Material[] createSet(int subject, string cube)
    {
        Material[] matlist = rend.materials;
        /*
       MATLIST ORDER
       [0] = base
       [1] = right
       [2] = front
       [3] = left
       [4] = back
       [5] = top
       [6] = bottom

        Function intakes the subject number and desired cube.
        It then determines what icons the features will have and where they will be placed.
        Based on this table:

                F1  F2  F3            F1            F2          F3
              _________________________________________________________
           00|  R   G   B      P0|    F/B           L/R         T/B
           06|  G   R   B      P1|    L/R           F/B         T/B
           12|  G   B   R      P2|    L/R           T/B         F/B
           18|  B   G   R      P3|    T/B           L/R         F/B
           24|  B   R   G      P4|    T/B           F/B         L/R
           30|  R   B   G      P5|    F/B           T/B         L/R

        As there are six of each, there are 36 possible combinations. In this script I will order it
        as pairs (Ix, Py). E.g. the first 6 combinations will be I0, P1-6. The next six combinations
        are I1, P1-6 and so on. The combination is selected based on the subject number.
        */

        int select = 0; // subject % 36; 

        //initialize vars representing features and placements
        int F1x = 1;
        int F1y = 2;
        int F2x = 3;
        int F2y = 4;
        int F3x = 5;
        int F3y = 6;
        
        int right   = 1;
        int front   = 2;
        int left    = 3;
        int back    = 4;
        int top     = 5;
        int bottom  = 6;

        int c = 0;
        //This loop decides which set will belong to each feature
        for (int i = 1; i <= select; i++)
        {
            
            //for every 6th increment, shift feature sets
            if (i % 6 == 0)
            {
                //alternate swaps to ensure all permutations
                if (c % 2 == 0)
                {
                    //Swap F1 with F2
                    swap(ref F1x, ref F2x);
                    swap(ref F1y, ref F2y);
                } else {
                    //Swap F2 with F3
                    swap(ref F2x, ref F3x);
                    swap(ref F2y, ref F3y);
                }
                c++;
            }
        }
        
        //This loop decides where each feature will go on the cube
        for (int i = 1; i <= select; i++)
        {
            //for every increment, swap positions of features in matlist
            //alternate swaps to ensure all permutations
            if (i % 2 == 0)
            {
                //Swap Top/Bottom positions with Left/Right
                swap(ref top, ref left);
                swap(ref bottom, ref right);
            } else {
                //Swap Left/Right positions with Front/Back
                swap(ref left, ref front);
                swap(ref right, ref back);
            }
        }
        
        matlist[0] = material[0];
        switch (cube)
        {
            case "A1":
                matlist[front]  = material[F1x];
                matlist[back]   = material[F1x];
                matlist[left]   = material[F2x];
                matlist[right]  = material[F2x];
                matlist[top]    = material[F3x];
                matlist[bottom] = material[F3x];
                break;
            case "A2":
                matlist[front]  = material[F1x];
                matlist[back]   = material[F1x];
                matlist[left]   = material[F2x];
                matlist[right]  = material[F2x];
                matlist[top]    = material[F3y];
                matlist[bottom] = material[F3y];
                break;
            case "B1":
                matlist[front]  = material[F1x];
                matlist[back]   = material[F1x];
                matlist[left]   = material[F2y];
                matlist[right]  = material[F2y];
                matlist[top]    = material[F3x];
                matlist[bottom] = material[F3x];
                break;
            case "B2":
                matlist[front]  = material[F1x];
                matlist[back]   = material[F1x];
                matlist[left]   = material[F2y];
                matlist[right]  = material[F2y];
                matlist[top]    = material[F3y];
                matlist[bottom] = material[F3y];
                break;
            case "C1":
                matlist[front]  = material[F1y];
                matlist[back]   = material[F1y];
                matlist[left]   = material[F2x];
                matlist[right]  = material[F2x];
                matlist[top]    = material[F3x];
                matlist[bottom] = material[F3x];
                break;
            case "C2":
                matlist[front]  = material[F1y];
                matlist[back]   = material[F1y];
                matlist[left]   = material[F2x];
                matlist[right]  = material[F2x];
                matlist[top]    = material[F3y];
                matlist[bottom] = material[F3y];
                break;
            case "D1":
                matlist[front]  = material[F1y];
                matlist[back]   = material[F1y];
                matlist[left]   = material[F2y];
                matlist[right]  = material[F2y];
                matlist[top]    = material[F3x];
                matlist[bottom] = material[F3x];
                break;
            case "D2":
                matlist[front]  = material[F1y];
                matlist[back]   = material[F1y];
                matlist[left]   = material[F2y];
                matlist[right]  = material[F2y];
                matlist[top]    = material[F3y];
                matlist[bottom] = material[F3y];
                break;
        }
        return matlist;
    }

    

    void Update()
    {   
    }
}
