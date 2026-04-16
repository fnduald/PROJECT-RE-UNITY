using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;

    void Start()
    {
        // 총알이 생성되자마자 앞으로 날아갑니다.
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
        // 화면 밖으로 나갈 수 있으니 3초 뒤엔 자동으로 삭제합니다.
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // 1. 보스의 태그가 "Enemy"인지 확인합니다.
        if (hitInfo.CompareTag("Enemy"))
        {
            // 2. 보스 오브젝트에서 BossAI 컴포넌트를 가져옵니다.
            BossAI boss = hitInfo.GetComponent<BossAI>();

            if (boss != null)
            {
                // 3. 보스의 TakeDamage 함수를 실행합니다!
                boss.TakeDamage(1);
            }

            // 4. 총알은 보스에 닿았으니 사라집니다
            Destroy(gameObject);
        }
    }
}