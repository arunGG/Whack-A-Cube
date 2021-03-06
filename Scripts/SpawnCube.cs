using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SpawnCube : MonoBehaviour {

    public GameObject cube; // Prefab of Cube
    private GameObject[] cubes = new GameObject[5]; // Cube Array

    public GameObject particle; // Holds Prefab of particle
    public GameObject[] particles; // Destroy Partilces Array

    public Animation anim; // Variable to Hold Up-Down Animation

    public Transform[] SpawnPoints; //Spawn Points Array

    public TextMeshProUGUI score;
    public int scoreNum = 0;

    private int i=0; //CubeIndex
    private int SpawnIndex = 0; //Index
    public float NoOfSpawnPoints;

    public float speed = 1f; //Animation Speed
    public float speedIncrease = 0.01f; //Animation Speed Increase 
    public float speedLimit = 3.0f;

    public float destroyTime = 1f; // Particle Destory Time

    public bool isCreated = false; // Is the Cube Created ?


    private void Start()
    {
        score = GameObject.Find("Data").GetComponent<TextMeshProUGUI>();
    }
    void Update ()
    {

        if (!isCreated) // Check to see if the cube is created
        {
            SpawnIndex = (int)Random.Range(0f, NoOfSpawnPoints);// Randomise the SpawnIndex
            //Debug.Log(SpawnIndex);
            CreateCube(); // Calls Create Method
            
        }
        

        if (Input.GetMouseButtonDown(0) && isCreated)
        {
            RaycastHit rayInfo; // Holds raycast Info

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out rayInfo)) //Shoots a Raycast from Mouse Position and Stores it in rayInfo
            {
                if(rayInfo.collider.gameObject.tag == "Cube") // Checking if we Hit the Cube or not 
                Destroy(rayInfo); // If we hit a Cube call the Destroy Method
            }

        }
	}


    public void CreateCube() // Method to Create Cube
    {


        cubes[i] = Instantiate(cube, SpawnPoints[SpawnIndex].localPosition, Quaternion.identity, SpawnPoints[SpawnIndex]); // Creating Cube at SpawnIndex
        anim = cubes[i].GetComponent<Animation>(); // Getting the Animation Component of the Cube
        anim["cubeAnimation"].speed = speed; // Change Cube Animation Speed
        isCreated = true;
    }

    public void Destroy(RaycastHit rayInfo) // Method to Destroys Cube 
    {
        BoxCollider bc = rayInfo.collider as BoxCollider;
        Vector3 pos = bc.gameObject.transform.position;

        Destroy(bc.gameObject); // Destroying Cube

        if (speed <= speedLimit)
        {

                speed += speedIncrease;

        }
        
        Particle(pos); // Calling Particle Method and passing the Position of the Destroyed Cube
        
        score.SetText((++scoreNum).ToString());
        isCreated = false;
    }

    public void Particle(Vector3 pos) // Method to Spawn Particles after Destroying Cube
    {
        particles[0] = Instantiate(particle, pos, Quaternion.identity);
        particles[1] = Instantiate(particle, pos, Quaternion.identity);
        particles[2] = Instantiate(particle, pos, Quaternion.identity);

        Destroy(particles[0].gameObject, destroyTime);
        Destroy(particles[1].gameObject, destroyTime);
        Destroy(particles[2].gameObject, destroyTime);

    }


}
