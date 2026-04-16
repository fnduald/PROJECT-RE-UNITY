using UnityEngine;

public class BossHP : MonoBehaviour
{
    public int health = 10; // 보스 체력 (인스펙터에서 조절 가능)

    // 총알이랑 부딪히면 실행될 함수
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("보스 남은 체력: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("보스 처치 완료!");
        Destroy(gameObject); // 보스 삭제
        // 여기서 승리 텍스트를 띄우거나 다음 스테이지로 넘기는 코드를 넣으면 됩니다.
    }
}