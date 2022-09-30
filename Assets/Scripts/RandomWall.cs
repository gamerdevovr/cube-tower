using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWall : MonoBehaviour
{
    public Material[] materialsWall;

    private GameObject top, wall_1, wall_2, wall_3, wall_4;


    void Start()
    {

        GameObject parrent = gameObject;

        top = parrent.transform.GetChild(0).gameObject;
        wall_1 = parrent.transform.GetChild(1).gameObject;
        wall_2 = parrent.transform.GetChild(2).gameObject;
        wall_3 = parrent.transform.GetChild(3).gameObject;
        wall_4 = parrent.transform.GetChild(4).gameObject;


        wall_1.GetComponent<MeshRenderer>().material = materialsWall[Random.Range(0,2)];
        wall_2.GetComponent<MeshRenderer>().material = materialsWall[Random.Range(2, 5)];
        wall_3.GetComponent<MeshRenderer>().material = materialsWall[Random.Range(2, 5)];
        wall_4.GetComponent<MeshRenderer>().material = materialsWall[Random.Range(2, 5)];

    }
}
