using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Flock : MonoBehaviour
{
    public FlockManager flockmanager;
    public Efforts Efforts;

    public  float speed;

    public bool turning = false;

 
  
    // Start is called before the first frame update
    void Start()
    {
        speed = flockmanager.minSpeed* 2.5f;
        Efforts = flockmanager.controller.GetComponent<Efforts>();
    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = new Bounds(flockmanager.transform.position, flockmanager.limits);

        RaycastHit hit = new RaycastHit();

        Vector3 direction = flockmanager.transform.position - transform.position;

        if (!bounds.Contains(transform.position))
        {
            turning = true;
        }
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else
        {
            turning = false;
        }

        if (turning)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                flockmanager.rotSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 20)
                Rules();
        }

        if (Efforts.beenPressed == true) 
        {
            speed = Efforts.finalSpeed;
            flockmanager.neighborDistance = Efforts.KineticEnergy;
            flockmanager.rotSpeed = Efforts.finalSpeed;
           
        }
        transform.Translate(0 , 0 ,Time.deltaTime * speed);

    }

  


    void Rules()
    {
        GameObject[] allGameObjects;
        allGameObjects = flockmanager.allBoids;
        
        Vector3 vcenter = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float groupSpeed = 0.01f;
        float neighbourDist;
        int groupSize = 0;

        foreach (GameObject go in allGameObjects)
        {
            if (go != this.gameObject)
            {
                neighbourDist = Vector3.Distance(go.transform.position, this.transform.position);
                if (neighbourDist <= flockmanager.neighborDistance)
                {
                    vcenter += go.transform.position;
                    groupSize++;

                    if (neighbourDist < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    
                    Flock nextFlock = go.GetComponent<Flock>();
                    groupSpeed = groupSpeed + nextFlock.speed;

                }
            }
            
        }

        if (groupSize > 0)
        {
            vcenter = vcenter / groupSize +(flockmanager.goalPosition - this.transform.position);
            speed = groupSpeed / groupSize;

            Vector3 direction = (vcenter + vavoid) - transform.position;
            if(direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction),flockmanager.rotSpeed * Time.deltaTime );
        }

    }
   
}
