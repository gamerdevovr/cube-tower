using UnityEngine;

public class RorateCamera : MonoBehaviour
{
    public float speed = 5f;
    private Transform _rotator;

    private void Start()
    {
        _rotator = GetComponent<Transform>();
    }

    private void Update()
    {
        _rotator.Rotate(0, speed * Time.deltaTime, 0);
    }
}
