using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Features.Interactions;

public class Efforts : MonoBehaviour
{

    public ActionBasedController controller;

    private List<Vector3> _listOfPositions= new List<Vector3>();
   
    
    private float isPressed;
    public bool beenPressed;
    
    private float timeOfAction;
    private float storedTime;

    public float finalSpeed = 0;
    private float initialSpeed = 0;

    public float totalDistance;
    public float totalAccelerations;

    public float maxSpeed = 10;

   
    public float objectMass = 5;
    public float KineticEnergy;

    

    
  
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    void Update()
    {
        isPressed = controller.uiPressAction.action.ReadValue<float>();
       

        if (isPressed == 1 )
        {
           
            _listOfPositions.Add(transform.position);
         
            timeOfAction += Time.deltaTime;
            storedTime = timeOfAction;
            
            beenPressed = true;
        }
        else if (beenPressed == true && isPressed == 0)
        {
            for (int i = 0; i < _listOfPositions.Count - 1; i++)
            {
                totalDistance = totalDistance + Vector3.Distance(_listOfPositions[i], _listOfPositions[i + 1]);
                
            }

            timeOfAction = 0;
            beenPressed = false;
            
            finalSpeed = totalDistance / storedTime;
            finalSpeed = remap(1, 10, 1, 5, finalSpeed);
            totalAccelerations = (finalSpeed - initialSpeed) / storedTime;
           
            KineticEnergy =0.5f * objectMass * (Mathf.Pow(finalSpeed,2));
            KineticEnergy = remap(0, 25, 1, 10, KineticEnergy);
            
            
            _listOfPositions.Clear();
           
            
            totalDistance = 0;
            totalAccelerations = 0;
            KineticEnergy = 0;

        }
 
        
        

    }
    public float remap(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue){
 
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
 
        return(NewValue);
    }
    
}
