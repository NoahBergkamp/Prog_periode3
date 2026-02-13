using UnityEngine;

public class FLag : MonoBehaviour
{

    public GameObject winUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        winUI.SetActive(true);
        Time.timeScale = 0;
        
    }
}
