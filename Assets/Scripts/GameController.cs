using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    private CubePos nowCube = new CubePos(0, 1, 0);
    public float cubeChangePlaceSpeed = 0.5f;
    public Transform cubeToPlace;
    private float camMoveToYPosition, camMoveSpeed = 2f;

    public Text scoreTxt;

    public GameObject[] cubesToCreate;

    public GameObject allCubes, vfx, newCubeBell;
    public GameObject[] canvasStartPage;
    private Rigidbody allCubesRb;

    public Color[] bgColors;

    private bool IsLose, firstCube;

    private List<Vector3> allCubesPosition = new List<Vector3>
    {
        new Vector3(0, 0, 0),
        new Vector3(1, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3(0, 1, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 0, -1),
        new Vector3(1, 0, 1),
        new Vector3(-1, 0, -1),
        new Vector3(-1, 0, 1),
        new Vector3(1, 0, -1),
    };

    private int prevCountMaxHorizontal, nowCountCubes = 0;
    private Coroutine showCubePlace;
    private Transform mainCam;
    private Color toCameraColor;

    private List<GameObject> posibleCubesToCreate = new List<GameObject>();

    private void Start()
    {
        
        if (PlayerPrefs.GetInt("score") < 5)
            posibleCubesToCreate.Add(cubesToCreate[0]);
        if (PlayerPrefs.GetInt("score") >= 5)
            posibleCubesToCreate.Add(cubesToCreate[1]);
        if (PlayerPrefs.GetInt("score") >= 10)
            posibleCubesToCreate.Add(cubesToCreate[2]);
        if (PlayerPrefs.GetInt("score") >= 20)
            posibleCubesToCreate.Add(cubesToCreate[3]);
        if (PlayerPrefs.GetInt("score") >= 30)
            posibleCubesToCreate.Add(cubesToCreate[4]);
        if (PlayerPrefs.GetInt("score") >= 50)
            posibleCubesToCreate.Add(cubesToCreate[5]);
        if (PlayerPrefs.GetInt("score") >= 70)
            posibleCubesToCreate.Add(cubesToCreate[6]);
        if (PlayerPrefs.GetInt("score") >= 100)
            posibleCubesToCreate.Add(cubesToCreate[7]);
        if (PlayerPrefs.GetInt("score") >= 130)
            posibleCubesToCreate.Add(cubesToCreate[8]);
        if (PlayerPrefs.GetInt("score") >= 200)
            posibleCubesToCreate.Add(cubesToCreate[9]);


        PlayerPrefs.SetFloat("nowCountCubes", 0);
        toCameraColor = Camera.main.backgroundColor;
        mainCam = Camera.main.transform;
        camMoveToYPosition = 6f + nowCube.y - 1f;

        allCubesRb = allCubes.GetComponent<Rigidbody>();
        showCubePlace = StartCoroutine(ShowCubePlace());

        scoreTxt.text = "<color='#E06055'>best result: " + PlayerPrefs.GetInt("score") + "</color>\npresent result: 0";
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && cubeToPlace != null && allCubes != null && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {

#if !UNITY_EDITOR
            if (Input.GetTouch(0).phase != TouchPhase.Began)
                return;
#endif


            if (!firstCube)
            {
                firstCube = true;
                foreach (GameObject obj in canvasStartPage)
                    Destroy(obj);
            }

            GameObject createCube = null;
            if (posibleCubesToCreate.Count == 1)
                createCube = posibleCubesToCreate[0];
            else
                createCube = posibleCubesToCreate[UnityEngine.Random.Range(0, posibleCubesToCreate.Count)];
            
            GameObject newCube = Instantiate(createCube, cubeToPlace.position, Quaternion.identity) as GameObject;

            newCube.transform.SetParent(allCubes.transform);
            nowCube.setVector(cubeToPlace.position);
            allCubesPosition.Add(nowCube.getVector());

            if (PlayerPrefs.GetString("music") != "No")
                newCubeBell.GetComponent<AudioSource>().Play();

            GameObject newVfx = Instantiate(vfx, cubeToPlace.transform.position, Quaternion.identity);
            Destroy(newVfx, 1.5f);


            allCubesRb.isKinematic = true;
            allCubesRb.isKinematic = false;

            SpawnPosition();
            MoveCameraChangeBg();
        }

        if (!IsLose &&  allCubesRb.velocity.magnitude > 0.1f)
        {
            Destroy(cubeToPlace.gameObject);
            IsLose = true;
            StopCoroutine(showCubePlace);
        }

        mainCam.localPosition = Vector3.MoveTowards(mainCam.localPosition, new Vector3(mainCam.localPosition.x, camMoveToYPosition, mainCam.localPosition.z), camMoveSpeed * Time.deltaTime);

        if (Camera.main.backgroundColor != toCameraColor)
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, toCameraColor, Time.deltaTime / 1.5f);
    }

    IEnumerator ShowCubePlace()
    {
        while (true)
        {
            SpawnPosition();

            yield return new WaitForSeconds(cubeChangePlaceSpeed);
        }
    }

    private void SpawnPosition()
    {
        List<Vector3> position = new List<Vector3>();
        
        if (IsPositionEmpty(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z)) && nowCube.x + 1 != cubeToPlace.position.x)
            position.Add(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z));    
        
        if (IsPositionEmpty(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z)) && nowCube.x - 1 != cubeToPlace.position.x)
            position.Add(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z));

        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z)) && nowCube.y + 1 != cubeToPlace.position.y)
            position.Add(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z));

        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z)) && nowCube.y - 1 != cubeToPlace.position.y)
            position.Add(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z));

        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1)) && nowCube.z + 1 != cubeToPlace.position.z)
            position.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1));

        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1)) && nowCube.z - 1 != cubeToPlace.position.z)
            position.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1));


        if (position.Count > 1)
        {
            cubeToPlace.position = position[UnityEngine.Random.Range(0, position.Count)];
            if (nowCountCubes >= 1 && PlayerPrefs.GetString("music") != "No")
                GetComponent<AudioSource>().Play();
        }
        else if (position.Count == 0)
        {
            Destroy(cubeToPlace.gameObject);
            IsLose = true;
            StopCoroutine(showCubePlace);
        }
        else
            cubeToPlace.position = position[0];

    }

    private bool IsPositionEmpty(Vector3 targetPos)
    {
        if (targetPos.y == 0)
            return false;

        foreach (Vector3 pos in allCubesPosition)
        {
            if ((pos.x == targetPos.x) && (pos.y == targetPos.y) && (pos.z == targetPos.z))
                return false;
        }
        
        return true;
    }

    private void MoveCameraChangeBg()
    {
        int maxX = 0, maxY = 0, maxZ = 0, maxHor;

        foreach (Vector3 pos in allCubesPosition)
        {
            if (Mathf.Abs(Convert.ToInt32(pos.x)) > maxX)
                maxX = Convert.ToInt32(pos.x);

            if (Convert.ToInt32(pos.y) > maxY)
                maxY = Convert.ToInt32(pos.y);

            if (Mathf.Abs(Convert.ToInt32(pos.z)) > maxZ)
                maxZ = Convert.ToInt32(pos.z);
        }

        if (PlayerPrefs.GetInt("score") < maxY - 1)
            PlayerPrefs.SetInt("score", maxY - 1);

        scoreTxt.text = "<color='#E06055'>best result: " + PlayerPrefs.GetInt("score") + "</color>\npresent result: " + (maxY - 1);
        nowCountCubes = maxY - 1;
        PlayerPrefs.SetFloat("nowCountCubes", nowCountCubes);

        camMoveToYPosition = 6f + nowCube.y - 1f;

        maxHor = maxX > maxZ ? maxX : maxZ;

        if (maxHor % 3 == 0 && prevCountMaxHorizontal != maxHor)
        {
            mainCam.localPosition -= new Vector3(0, 0, 2.5f);
            prevCountMaxHorizontal = maxHor;
        }

        if (maxY >= 15)
            toCameraColor = bgColors[4];
        else if (maxY >= 12)
            toCameraColor = bgColors[3];
        else if (maxY >= 9)
            toCameraColor = bgColors[2];
        else if (maxY >= 6)
            toCameraColor = bgColors[1];
        else if (maxY >= 3)
            toCameraColor = bgColors[0];
    }

}

struct CubePos 
{
    public int x, y, z;

    public CubePos(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 getVector()
    {
        return new Vector3(x, y, z);
    }

    public void setVector(Vector3 pos)
    {
        x = Convert.ToInt32(pos.x);
        y = Convert.ToInt32(pos.y);
        z = Convert.ToInt32(pos.z);
    }
}