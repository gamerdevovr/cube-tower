using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform           _camTransform;
    private float               _shakeDur = 1f,
                                _shakeAmount = 0.08f, 
                                _decreaseFactor = 2f;

    private Vector3             _originPos;

    private void Start()
    {
        _camTransform = GetComponent<Transform>();
        _originPos = _camTransform.localPosition;
    }

    private void Update()
    {
        if (_shakeDur > 0)
        {
            _camTransform.localPosition = _originPos + Random.insideUnitSphere * _shakeAmount;
            _shakeDur -= Time.deltaTime * _decreaseFactor;
        }
        else 
        {
            _shakeDur = 0;
            _camTransform.localPosition = _originPos;
        }
    }
}
