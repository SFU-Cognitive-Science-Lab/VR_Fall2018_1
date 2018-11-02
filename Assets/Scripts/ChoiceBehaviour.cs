/*[Controls materials on choice buttons + next button]
 * 
 * Author: Rollin Poe
 * Created: October 19, 2018
 * Last Edit: October 20, 2018
 * 
 * Cognitive Science Lab, Simon Fraser University
 * Originally Created for: VR_Fall2018_1
 * 
 * In unity editor, attach to all game object which you wish to change the color of based on player choice
 *  Choices should be == number of choices + 1 (for next button)
 *  Insert choices as appropriate in the following List
 *  Apply materials to the material slots
 *  
 * firstChoice is used to denote whether the plyer choice this game object
 * If this is the player's first choice, colors will change, otherwise colors will stay the same.
 * 
 * Additional scripts Used: [CustomTag]
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceBehaviour : MonoBehaviour {

    public List<GameObject> choices;
    public Material correctMat, wrongMat, neutralMat, nextMat, nextActiveMat;
    public bool firstChoice;

   
    private List<Valve.VR.InteractionSystem.Hand> controllers;
    public Valve.VR.InteractionSystem.Hand fallbackController;
    public Valve.VR.InteractionSystem.Hand leftController;
    public Valve.VR.InteractionSystem.Hand rightController;
    public SteamVR_TrackedController controller;
    public GameObject contR;
    

    void Start () {
        firstChoice = true;
        controllers = new List<Valve.VR.InteractionSystem.Hand>();
        controllers.Add(fallbackController);
        controllers.Add(leftController);
        controllers.Add(rightController);

        controller = GetComponent<SteamVR_TrackedController>();
    }

    void Update() {
       /* if (controller.triggerPressed == true)
        {
            Debug.Log("A trigger has been pressed");
        }
        if (leftController.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("pressed");
        }*/
    }

    public void setFirstChoice(bool parity)
    {
        firstChoice = parity;
    }

    public void Activate() {
        /*
        if (controller.triggerPressed == true) { 
            Debug.Log("A trigger has been pressed");
        }*/



        if (firstChoice == true)
        {
            GameObject[] currentObjs = GameObject.FindGameObjectsWithTag("Interactable Object");
            foreach (GameObject cube in currentObjs)
            {
                if (cube.GetComponent<CustomTag>().getTag(0) == this.GetComponent<CustomTag>().getTag(0))
                {
                    //Correct Choice == change this to green, others to grey, next to blue
                    this.GetComponent<Renderer>().material = correctMat;

                    for (int i = 0; i < choices.Count; i++)
                    {
                        if (choices[i] != gameObject)
                        {
                            if (choices[i].GetComponent<CustomTag>().getTag(0) != "NEXT") //Set all other buttons to graey.
                            {
                                choices[i].GetComponent<Renderer>().material = neutralMat;
                                choices[i].GetComponent<ChoiceBehaviour>().setFirstChoice(false);
                            }
                            else //next button to blue
                            {
                                choices[i].GetComponent<Renderer>().material = nextMat;
                                choices[i].GetComponent<ChoiceBehaviour>().setFirstChoice(false);
                            }
                        }
                    }
                }
                else
                {
                    //Incorrect choice == change this to red, correct to green, others to grey, next to blue
                    this.GetComponent<Renderer>().material = wrongMat;
                    for (int i = 0; i < choices.Count; i++)
                    {
                        if (choices[i] != gameObject)
                        {
                            if (choices[i].GetComponent<CustomTag>().getTag(0) != "NEXT")
                            {
                                if (choices[i].GetComponent<CustomTag>().getTag(0) != cube.GetComponent<CustomTag>().getTag(0))
                                { //if not next AND not correct choice
                                    choices[i].GetComponent<Renderer>().material = neutralMat;
                                    choices[i].GetComponent<ChoiceBehaviour>().setFirstChoice(false);
                                }
                                else //Not next AND correct choice
                                {
                                    choices[i].GetComponent<Renderer>().material = correctMat;
                                    choices[i].GetComponent<ChoiceBehaviour>().setFirstChoice(false);
                                }
                            }
                            else //next button
                            {
                                choices[i].GetComponent<Renderer>().material = nextMat;
                                choices[i].GetComponent<ChoiceBehaviour>().setFirstChoice(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public void Next()
    {
        if (!firstChoice)
        {
            this.GetComponent<Renderer>().material = nextActiveMat;
        }
    }

}
