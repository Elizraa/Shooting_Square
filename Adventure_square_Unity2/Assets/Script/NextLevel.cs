using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int loadIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (loadIndex< SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(loadIndex);
            else
                SceneManager.LoadScene(0);
        }
    }
}
