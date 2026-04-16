using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 현재 활성화된 씬을 다시 로드.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}