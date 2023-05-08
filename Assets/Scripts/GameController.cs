using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject _allCubes;
    [SerializeField] private GameObject _vfx;
    [SerializeField] private GameObject _newCubeBell;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _victory;
    [SerializeField] private GameObject _ground;
    [SerializeField] private GameObject _fonMusic;
    [SerializeField] private GameObject _newCube;
    [SerializeField] private GameObject _pausePlay;
    [SerializeField] private TextMeshPro _scoreTxt;
    [SerializeField] private Image _newCubeImage;
    [SerializeField] private Transform _cubeToPlace;
    [SerializeField] private float _cubeChangePlaceSpeed = 0.5f;
    [SerializeField] private float _tiltSensitivity;
    [SerializeField] private GameObject[] _cubesToCreate;
    [SerializeField] private GameObject[] _canvasStartPage;
    [SerializeField] private Sprite[] _cubesImages;

    private int _prevCountMaxHorizontal;
    private int _nowCountCubes = 0;
    private int _resultVictory = 200;

    private int[] _eventsGame = { 5, 10, 20, 30, 50, 70, 100, 130, 200};

    private float _camMoveToYPosition;
    private float _camMoveSpeed = 2f;

    private bool _isLose;
    private bool _firstCube;

    private CubePos _nowCube = new CubePos(new Vector3(0, 1, 0));
    private Rigidbody _allCubesRb;
    private Coroutine _showCubePlace;
    private Transform _mainCam;
    private List<GameObject> _posibleCubesToCreate = new List<GameObject>();
    private List<int> _addedCubes = new List<int>();

    private List<Vector3> _allCubesPosition = new List<Vector3>
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
    new Vector3(1, 0, -1)
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
        _camMoveToYPosition = 6f + _nowCube.Position.y - 1f;

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
            
            _nowCube.Position = _cubeToPlace.position;
            
            _allCubesPosition.Add(_nowCube.Position);

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
        }

        _mainCam.localPosition = Vector3.MoveTowards(_mainCam.localPosition, new Vector3(_mainCam.localPosition.x, _camMoveToYPosition, _mainCam.localPosition.z), _camMoveSpeed * Time.deltaTime);
    }

    IEnumerator AddPosibleCubesToCreate()
    {
        int i = 0;
        while (true)
        {
            int nowResult = _nowCountCubes;
            yield return new WaitForSeconds(1);
  
            foreach (int res in _eventsGame)
            {
                if (nowResult >= res && !_addedCubes.Contains(res))
                {
                    i++;
                    _posibleCubesToCreate.Add(_cubesToCreate[i]);
                    _addedCubes.Add(res);
                    
                    if (nowResult == res)
                    {
                        
                        _newCubeImage.GetComponent<Image>().sprite = _cubesImages[i];
                        _newCube.GetComponent<Animation>().Play("BestResult");
                        
                        if (nowResult == _resultVictory)
                        {
                            _victory.SetActive(true);
                            _victory.GetComponent<AudioSource>().Play();
                        }

                    }
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

        Vector3[] directions = new Vector3[] {
            Vector3.right,
            Vector3.left,
            Vector3.up,
            Vector3.down,
            Vector3.forward,
            Vector3.back
        };

        var emptyPositions = directions.Select(d => _nowCube.Position + d).Where(p => IsPositionEmpty(p) && p != _cubeToPlace.position);

        position.AddRange(emptyPositions);


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

        _camMoveToYPosition = 6f + _nowCube.Position.y - 1f;

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
    public Vector3 Position { get; set; }

    public CubePos(Vector3 position)
    {
       Position = position;
    }
}