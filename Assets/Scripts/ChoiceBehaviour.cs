/*[Controls materials on choice buttons + next button]
 * 
 * Author: Rollin Poe
 * Created: October 19, 2018
 * Last Edit: November 07, 2018
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
    public Material correctMat, wrongMat, neutralMat, nextMat, noChoiceMat;
    public bool firstChoice;

    private string cTag; 

    private List<Valve.VR.InteractionSystem.Hand> controllers;
    public Valve.VR.InteractionSystem.Hand fallbackController;
    public Valve.VR.InteractionSystem.Hand leftController;
    public Valve.VR.InteractionSystem.Hand rightController;

    

    void Start () {
        firstChoice = true;
        cTag = this.GetComponent<CustomTag>().getTag(0);
        controllers = new List<Valve.VR.InteractionSystem.Hand>();
        controllers.Add(fallbackController);
        controllers.Add(leftController);
        controllers.Add(rightController);
    }

    void Update() {
    }

    public void setFirstChoice(bool parity)
    {
        firstChoice = parity;
    }

    public void OnTriggerStay(Collider other)
    {

        if (leftController.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger) || rightController.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (cTag != "NEXT")
            {
                if (firstChoice == true)
                {
                    this.GetComponent<CustomTag>().buttonTagBehaviour(cTag); // Call custom tag top set correct/incorrect

                    GameObject[] currentObjs = GameObject.FindGameObjectsWithTag("Interactable Object");
                    foreach (GameObject cube in currentObjs)
                    {
                        if (cube.GetComponent<CustomTag>().getTag(0) == cTag)
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
            else //Player clicked next button
            {
                //if(firstChoice != true)
                //{
                    ResetButtons();
                //}
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (cTag == "NEXT" && firstChoice != true){
            ResetButtons();
        }
    }

    private void ResetButtons()
    {
        Debug.Log("Trace 2");
        //Despawn current box
        this.GetComponent<DespawnObject>().buttonDespawn();

        //Change Colors/underlying variables
        for(int i = 0; i < choices.Count; i++)
        {
            if (choices[i].GetComponent<CustomTag>().getTag(0) != "NEXT")
            {
                choices[i].GetComponent<Renderer>().material = noChoiceMat;
                choices[i].GetComponent<ChoiceBehaviour>().firstChoice = true;
            }
            else
            {
                choices[i].GetComponent<Renderer>().material = neutralMat;
                choices[i].GetComponent<ChoiceBehaviour>().firstChoice = true;
            }
        }

        //Spawn in new box
        this.GetComponent<SpawnRandomBox>().spawn();
        
    }



    public void Activate()
    {
        /*
        if (controller.triggerPressed == true) { 
            Debug.Log("A trigger has been pressed");
        }*/


        //if (leftController.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger) || rightController.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    if (firstChoice == true)
        //    {
        //        GameObject[] currentObjs = GameObject.FindGameObjectsWithTag("Interactable Object");
        //        foreach (GameObject cube in currentObjs)
        //        {
        //            if (cube.GetComponent<CustomTag>().getTag(0) == this.GetComponent<CustomTag>().getTag(0))
        //            {
        //                //Correct Choice == change this to green, others to grey, next to blue
        //                this.GetComponent<Renderer>().material = correctMat;

        //                for (int i = 0; i < choices.Count; i++)
        //                {
        //                    if (choices[i] != gameObject)
        //                    {
        //                        if (choices[i].GetComponent<CustomTag>().getTag(0) != "NEXT") //Set all other buttons to graey.
        //                        {
        //                            choices[i].GetComponent<Renderer>().material = neutralMat;
        //                            choices[i].GetComponent<ChoiceBehaviour>().setFirstChoice(false);
        //                        }
        //                        else //next button to blue
        //                        {
        //                            choices[i].GetComponent<Renderer>().material = nextMat;
        //                            choices[i].GetComponent<ChoiceBehaviour>().setFirstChoice(false);
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //Incorrect choice == change this to red, correct to green, others to grey, next to blue
        //                this.GetComponent<Renderer>().material = wrongMat;
        //                for (int i = 0; i < choices.Count; i++)
        //                {
        //                    if (choices[i] != gameObject)
        //                    {
        //                        if (choices[i].GetComponent<CustomTag>().getTag(0) != "NEXT")
        //                        {
        //                            if (choices[i].GetComponent<CustomTag>().getTag(0) != cube.GetComponent<CustomTag>().getTag(0))
        //                            { //if not next AND not correct choice
        //                                choices[i].GetComponent<Renderer>().material = neutralMat;
        //                                choices[i].GetComponent<ChoiceBehaviour>().setFirstChoice(false);
        //                            }
        //                            else //Not next AND correct choice
        //                            {
        //                                choices[i].GetComponent<Renderer>().material = correctMat;
        //                                choices[i].GetComponent<ChoiceBehaviour>().setFirstChoice(false);
        //                            }
        //                        }
        //                        else //next button
        //                        {
        //                            choices[i].GetComponent<Renderer>().material = nextMat;
        //                            choices[i].GetComponent<ChoiceBehaviour>().setFirstChoice(false);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }

}
