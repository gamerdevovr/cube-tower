using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform camTransform;
    private float shakeDur = 1f, shakeAmount = 0.08f, decreaseFactor = 2f;

    private Vector3 originPos;

    private void Start()
    {
        camTransform = GetComponent<Transform>();
        originPos = camTransform.localPosition;
    }

    private void Update()
    {
        if (shakeDur > 0)
        {
            camTransform.localPosition = originPos + Random.insideUnitSphere * shakeAmount;
            shakeDur -= Time.deltaTime * decreaseFactor;
        }
        else 
        {
            shakeDur = 0;
            camTransform.localPosition = originPos;
        }
    }
}
