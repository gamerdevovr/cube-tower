using UnityEngine;

public class ExplodeCubes : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject gameOver, explosion;
    private bool _collisionSet;

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
            restartButton.SetActive(true);
            gameOver.SetActive(true);
            Camera.main.transform.localPosition -= new Vector3(0, 0, 10f);
            //Camera.main.transform.localPosition = new Vector3(0, 45f, 0f);
            //Camera.main.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            Camera.main.gameObject.AddComponent<CameraShake>();

            GameObject newExplosion = Instantiate(explosion, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, collision.contacts[0].point.z), Quaternion.identity);
            Destroy(newExplosion, 2.5f);


            if (PlayerPrefs.GetString("music") != "No")
                GetComponent<AudioSource>().Play();

            Destroy(collision.gameObject);
            _collisionSet = true;
        }
    }
}
