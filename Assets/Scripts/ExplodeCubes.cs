using UnityEngine;

public class ExplodeCubes : MonoBehaviour
{

    [SerializeField] private GameObject     _gameOver,
                                            _explosion;

    private bool                            _collisionSet;

    private float                           _distanceMoveCamera;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cube" && !_collisionSet)
        {
            for (int i = collision.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = collision.transform.GetChild(i);
                child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(70f, Vector3.up, 5f);
                child.SetParent(null);
            }
            _gameOver.SetActive(true);
            _gameOver.GetComponent<GameOver>().SetNewResult();
            
            if (PlayerPrefs.GetString("sound").Equals("Yes"))
                _gameOver.GetComponent<AudioSource>().Play();

            if (PlayerPrefs.GetFloat("nowCountCubes") < 7f)
                _distanceMoveCamera = 7f;
            else
                _distanceMoveCamera = PlayerPrefs.GetFloat("nowCountCubes");

            Camera.main.transform.localPosition -= new Vector3(0, 0, _distanceMoveCamera);
            Camera.main.gameObject.AddComponent<CameraShake>();

            GameObject newExplosion = Instantiate(_explosion, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, collision.contacts[0].point.z), Quaternion.identity);
            Destroy(newExplosion, 2.5f);

            Destroy(collision.gameObject);
            _collisionSet = true;

            transform.localScale = new Vector3(3f, 0.5f, 3f);
        }
    }

}
