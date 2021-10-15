using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] EnemyLocations, TreesLocations, MiscLocations;
    public GameObject Enemies, Trees, Misc;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(Enemies, EnemyLocations[Random.Range(0, 5)].transform.position += new Vector3(0, 0.75f), 
        //    Quaternion.identity);
        Instantiate(Trees, TreesLocations[Random.Range(0, 5)].transform.position += new Vector3(0, 0.75f), 
            Quaternion.identity);
        //Instantiate(Misc, MiscLocations[Random.Range(0, 5)].transform.position += new Vector3(0, 0.75f), 
        //   Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
