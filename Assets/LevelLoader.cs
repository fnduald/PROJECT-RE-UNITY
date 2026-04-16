using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 꼭 필요합니다.

public class LevelLoader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 닿은 물체의 태그가 "Player" 인지 확인합니다.
        if (collision.CompareTag("Player"))
        {
            // 현재 활성화된 씬의 번호를 가져옵니다.
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // 다음 번호의 씬을 불러옵니다.
            // 마지막 스테이지(5단계)가 아닐 때만 다음으로 넘어갑니다.
            if (currentSceneIndex < 4)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                Debug.Log("게임 클리어!");
                // 여기에 엔딩 크레딧 씬을 넣거나 타이틀로 보낼 수 있습니다.
            }
        }
    }
}