using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    
    
    public GameObject boid;
    public GameObject controller;
    private Efforts efforts;
    public GameObject tank;
    public int amount = 20;
    public GameObject[] allBoids;
    public Vector3 limits = new Vector3(5,5,5);
    public Vector3 goalPosition;

    public float minSpeed = 1;
    public float maxSpeed = 5;
    [Range(1.0f, 10.0f)] public float neighborDistance;
    [Range(1.0f, 5.0f)] public float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        efforts = controller.GetComponent<Efforts>();
        allBoids = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(
                Random.Range(-limits.x, limits.x),
                Random.Range(-limits.y, limits.y),
                Random.Range(-limits.z, limits.z)
            );
            allBoids[i] = (GameObject) Instantiate(boid, pos, Quaternion.identity);
            allBoids[i].GetComponent<Flock>().flockmanager = this;
        }
        goalPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Random.Range(0,100)<10){ goalPosition = this.transform.position + new Vector3(
                                                       Random.Range(-limits.x, limits.x),
                                                       Random.Range(-limits.y, limits.y),
                                                       Random.Range(-limits.z, limits.z)); }

       
        
        
    }
    
   

}
