using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: tidy things up into a nice function
// see createSet() below with no arguments, if it worked it might be a candidate

public class applicator : MonoBehaviour {
    public Material[] material;
    Renderer rend;

    void Start()
    {
        Cubes cubes = new Cubes();
        Debug.Log("new dress!");
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        Debug.Log(name);
        if (DataFarmer.GetInstance().CubeLists.IsEmpty())
        {
            throw new MissingComponentException("Missing needed cube map arrangements in CubeArrangements!");
        }
        else
        {
            rend.materials = createSet();
        }
    }

    // makes a cube from a description of the 3 axes
    // for a description of this see ColorShapeRotation.cs
    // a CubeTuple describes a cube 
    // mapping of cubes to categories is stored externally 
    // as json data
    // for any participant we pick one set of mappings 
    // and go through them somewhat randomly

    // I guess this is the current version of the "nice function"
    // this pulls the materials based on what we get for
    // the externally generated counterbalancing conditions
    // and maps the materials to the raw cube
    Material[] createSet()
    {
        // a CubeTuple is the 3 axes of the cube
        CubeTuple c = ParticipantStatus.GetInstance().GetCube();
        Material[] matlist = rend.materials;

        // this is the basic background material for the cube
        matlist[0] = material[0];

        // ColorShapeRotation describes one of the axes of the cube
        foreach (ColorShapeRotation csr in c.cube)
        {
            // we dress the two faces for the axis with the appropriate material
            foreach (int face in csr.GetFaces())
            {
                matlist[face] = material[csr.GetMaterial()];
            }
        }
        return matlist;
    }
}
