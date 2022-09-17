using UnityEngine;

public class IsEnable : MonoBehaviour
{
    public int needToUnlock;
    public Material blachMaterial;

    private void Start()
    {
        if (PlayerPrefs.GetInt("score") < needToUnlock)
            GetComponent<MeshRenderer>().material = blachMaterial;
    }

}
