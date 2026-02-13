using UnityEngine;

public class ResartGame : MonoBehaviour
{
    public void LoadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}