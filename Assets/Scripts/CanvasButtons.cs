using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasButtons : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
    }

    public void LoadFacebook()
    {
        Application.OpenURL("https://facebook.com");
    }
}
