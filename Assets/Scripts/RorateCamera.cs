using UnityEngine;

public class RorateCamera : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
                        
    private Transform _rotator;

    private void Start()
    {
        _rotator = GetComponent<Transform>();
    }

    private void Update()
    {
        _rotator.Rotate(0, _speed * Time.deltaTime, 0);
    }
}
