using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;

public class GameController : MonoBehaviour
{

    public GameObject               _scoreTxt,
                                    _allCubes,
                                    _vfx,
                                    _newCubeBell,
                                    _gameOver,
                                    _ground,
                                    _fonMusic,
                                    _newCube,
                                    _pausePlay;
    
    public Transform                _cubeToPlace;
    
    public float                    _cubeChangePlaceSpeed = 0.5f,
                                    _tiltSensitivity;

    public GameObject[]             _cubesToCreate,
                                    _canvasStartPage;

    private int                     _prevCountMaxHorizontal,
                                    _nowCountCubes = 0;

    private int[]                   _eventsGame = { 5, 10, 20, 30, 50, 70, 100, 130, 200};

    private float                   _camMoveToYPosition,
                                    _camMoveSpeed = 2f;

    private bool                    _isLose,
                                    _firstCube;

    private CubePos                 _nowCube = new CubePos(0, 1, 0);
    private Rigidbody               _allCubesRb;
    private Coroutine               _showCubePlace;
    private Transform               _mainCam;
    private List<GameObject>        _posibleCubesToCreate = new List<GameObject>();
    private List<int>               _addedCubes = new List<int>();

    private List<Vector3>           _allCubesPosition = new List<Vector3>
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

    private void Start()
    {
        int startResult = PlayerPrefs.GetInt("score");

        if (startResult < 5)
        {
            _posibleCubesToCreate.Add(_cubesToCreate[0]);
            _addedCubes.Add(0);
        }
        else
        {
            int i = 0;

            foreach (int res in _eventsGame)
            {
                if (startResult >= res)
                {
                    i++;
                    _posibleCubesToCreate.Add(_cubesToCreate[i]);
                    _addedCubes.Add(res);
                }
                else
                    break;
            }
        }

        //Debug.Log(_posibleCubesToCreate.Count);
        
        StartCoroutine(AddPosibleCubesToCreate());

        PlayerPrefs.SetInt("nowCountCubes", 0);
        _mainCam = Camera.main.transform;
        _camMoveToYPosition = 6f + _nowCube.y - 1f;

        _allCubesRb = _allCubes.GetComponent<Rigidbody>();
        _showCubePlace = StartCoroutine(ShowCubePlace());

        _scoreTxt.GetComponent<TextMeshPro>().text = "best: " + PlayerPrefs.GetInt("score") + "\npresent: 0";
    }

    private void Update()
    {
        if ((( Input.GetMouseButtonDown(0) || Input.touchCount > 0 ) && _cubeToPlace != null && _allCubes != null && !IsPointerOverUIObject() && !_pausePlay.GetComponent<Play_Pause>().GetStatusPause()) || _canvasStartPage[1].GetComponent<ClosedObjects>().GetClicked())
        {

#if !UNITY_EDITOR
            if (Input.GetTouch(0).phase != TouchPhase.Began)
                return;
#endif


            if (!_firstCube)
            {
                _firstCube = true;
                foreach (GameObject obj in _canvasStartPage)
                    obj.SetActive(false);
                if (PlayerPrefs.GetString("music").Equals("Yes"))
                    _fonMusic.GetComponent<AudioSource>().Play();
            }

            GameObject createCube = null;
            if (_posibleCubesToCreate.Count == 1)
                createCube = _posibleCubesToCreate[0];
            else
                createCube = _posibleCubesToCreate[UnityEngine.Random.Range(0, _posibleCubesToCreate.Count)];
            
            GameObject newCube = Instantiate(createCube, _cubeToPlace.position, Quaternion.identity) as GameObject;

            newCube.transform.SetParent(_allCubes.transform);
            _nowCube.setVector(_cubeToPlace.position);
            _allCubesPosition.Add(_nowCube.getVector());

            if (PlayerPrefs.GetString("sound").Equals("Yes"))
                _newCubeBell.GetComponent<AudioSource>().Play();

            GameObject newVfx = Instantiate(_vfx, _cubeToPlace.transform.position, Quaternion.identity);
            Destroy(newVfx, 1.5f);


            _allCubesRb.isKinematic = true;
            _allCubesRb.isKinematic = false;

            SpawnPosition();
            MoveCameraChangeBg();

            if (_canvasStartPage[1].GetComponent<ClosedObjects>().GetClicked())
                _canvasStartPage[1].GetComponent<ClosedObjects>().SetClicked(false);
        }

        if (!_isLose &&  _allCubesRb.velocity.magnitude > _tiltSensitivity)
        {           
            Destroy(_cubeToPlace.gameObject);
            _isLose = true;
            StopCoroutine(_showCubePlace);
            _gameOver.SetActive(true);
            _gameOver.GetComponent<GameOver>().SetNewResult();
            if (PlayerPrefs.GetString("sound").Equals("Yes"))
                _gameOver.GetComponent<AudioSource>().Play();
        }

        _mainCam.localPosition = Vector3.MoveTowards(_mainCam.localPosition, new Vector3(_mainCam.localPosition.x, _camMoveToYPosition, _mainCam.localPosition.z), _camMoveSpeed * Time.deltaTime);
    }

    IEnumerator AddPosibleCubesToCreate()
    {
        while (true)
        {
            int nowResult = _nowCountCubes;
            yield return new WaitForSeconds(2);

           foreach (int res in _eventsGame)
           {
                int i = 0;
                if (nowResult >= res && !_addedCubes.Contains(res))
                {
                    i++;
                    _posibleCubesToCreate.Add(_cubesToCreate[i]);
                    _addedCubes.Add(res);
                    if (nowResult == res)
                        _newCube.GetComponent<Animation>().Play("BestResult");
                }
            }
        }
    }

    IEnumerator ShowCubePlace()
    {
        while (true)
        {
            SpawnPosition();
            if (_nowCountCubes == 50)
                _cubeChangePlaceSpeed = 0.45f;
            else if (_nowCountCubes == 100)
                _cubeChangePlaceSpeed = 0.4f;
            else if (_nowCountCubes == 150)
                _cubeChangePlaceSpeed = 0.3f;
            else if (_nowCountCubes == 190)
                _cubeChangePlaceSpeed = 0.25f;
            yield return new WaitForSeconds(_cubeChangePlaceSpeed);
        }
    }

    private bool IsPointerOverUIObject()
    {        
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void SpawnPosition()
    {
        List<Vector3> position = new List<Vector3>();
        
        if (IsPositionEmpty(new Vector3(_nowCube.x + 1, _nowCube.y, _nowCube.z)) && _nowCube.x + 1 != _cubeToPlace.position.x)
            position.Add(new Vector3(_nowCube.x + 1, _nowCube.y, _nowCube.z));    
        
        if (IsPositionEmpty(new Vector3(_nowCube.x - 1, _nowCube.y, _nowCube.z)) && _nowCube.x - 1 != _cubeToPlace.position.x)
            position.Add(new Vector3(_nowCube.x - 1, _nowCube.y, _nowCube.z));

        if (IsPositionEmpty(new Vector3(_nowCube.x, _nowCube.y + 1, _nowCube.z)) && _nowCube.y + 1 != _cubeToPlace.position.y)
            position.Add(new Vector3(_nowCube.x, _nowCube.y + 1, _nowCube.z));

        if (IsPositionEmpty(new Vector3(_nowCube.x, _nowCube.y - 1, _nowCube.z)) && _nowCube.y - 1 != _cubeToPlace.position.y)
            position.Add(new Vector3(_nowCube.x, _nowCube.y - 1, _nowCube.z));

        if (IsPositionEmpty(new Vector3(_nowCube.x, _nowCube.y, _nowCube.z + 1)) && _nowCube.z + 1 != _cubeToPlace.position.z)
            position.Add(new Vector3(_nowCube.x, _nowCube.y, _nowCube.z + 1));

        if (IsPositionEmpty(new Vector3(_nowCube.x, _nowCube.y, _nowCube.z - 1)) && _nowCube.z - 1 != _cubeToPlace.position.z)
            position.Add(new Vector3(_nowCube.x, _nowCube.y, _nowCube.z - 1));


        if (position.Count > 1)
        {
            _cubeToPlace.position = position[UnityEngine.Random.Range(0, position.Count)];
        }

        if (position.Count == 1)
            _cubeToPlace.position = position[0];
        
        if (position.Count < 1)
        {
            Destroy(_cubeToPlace.gameObject);
            _isLose = true;
            StopCoroutine(_showCubePlace);
            _gameOver.SetActive(true);
            _gameOver.GetComponent<GameOver>().SetNewResult();
            if (PlayerPrefs.GetString("sound").Equals("Yes"))
                _gameOver.GetComponent<AudioSource>().Play();
        }

    }

    private bool IsPositionEmpty(Vector3 targetPos)
    {
        if (targetPos.y == 0)
            return false;

        foreach (Vector3 pos in _allCubesPosition)
        {
            if ((pos.x == targetPos.x) && (pos.y == targetPos.y) && (pos.z == targetPos.z))
                return false;
        }
        
        return true;
    }

    private void MoveCameraChangeBg()
    {
        int maxX = 0, maxY = 0, maxZ = 0, maxHor;

        foreach (Vector3 pos in _allCubesPosition)
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

        _scoreTxt.GetComponent<TextMeshPro>().text = "best: " + PlayerPrefs.GetInt("score") + "\npresent: " + (maxY - 1);
        _nowCountCubes = maxY - 1;
        PlayerPrefs.SetFloat("nowCountCubes", _nowCountCubes);

        _camMoveToYPosition = 6f + _nowCube.y - 1f;

        maxHor = maxX > maxZ ? maxX : maxZ;

        if (maxHor % 3 == 0 && _prevCountMaxHorizontal != maxHor)
        {
            _mainCam.localPosition -= new Vector3(0, 0, 2.5f);
            _prevCountMaxHorizontal = maxHor;
        }

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