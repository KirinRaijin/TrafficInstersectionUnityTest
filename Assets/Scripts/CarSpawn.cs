using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    public GameObject[] Cars;

    int ram = 0;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Spawn()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            ram = Random.Range(0, Cars.Length);
            Cars[ram].GetComponent<Car_AI>().currentTrafficRoute = this.gameObject;
            Cars[ram].GetComponent<Car_AI>().currentwapointNumber = i;
            Instantiate(Cars[ram], transform.GetChild(i).transform.position, transform.GetChild(i).transform.rotation);

        }
    }
}
