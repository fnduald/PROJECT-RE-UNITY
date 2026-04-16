using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("보스 체력 설정")]
    public int health = 3;

    [Header("이동 패턴 설정")]
    public float hoverSpeed = 2f;    // 위아래 부유 속도
    public float hoverHeight = 1.5f; // 위아래 이동 범위
    public float dashSpeed = 5f;     // 플레이어 추격 속도

    public Transform player;         // 플레이어 위치 (Inspector에서 드래그)

    private float startY;
    private bool isDashing = false;
    private float patternTimer = 0f;
    private MeshRenderer mr;         // 3D 메쉬 렌더러 부품

    void Start()
    {
        startY = transform.position.y;

        // 플레이어를 태그로 찾음
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }

        // 3D 부품인 MeshRenderer를 가져옵니다.
        mr = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        patternTimer += Time.deltaTime;

        // 5초마다 패턴 변경 (부유 vs 추격)
        if (patternTimer >= 5f)
        {
            isDashing = !isDashing;
            patternTimer = 0f;
        }

        if (isDashing)
            DashAtPlayer();
        else
            Hovering();
    }

    // 패턴 1: 위아래로 부유
    void Hovering()
    {
        float newY = startY + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // 패턴 2: 플레이어 추격
    void DashAtPlayer()
    {
        if (player != null)
        {
            Vector2 target = new Vector2(player.position.x, player.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, dashSpeed * Time.deltaTime);
        }
    }

    // 총알에 맞았을 때 호출 (Bullet.cs에서 호출함)
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("보스 잔여 체력: " + health);

        // 3D 재질 색상을 빨간색으로 변경 (피격 피드백)
        if (mr != null)
        {
            mr.material.color = Color.red;
            Invoke("ResetColor", 0.1f);
        }

        // 체력이 0 이하면 제거
        if (health <= 0)
        {
            Die();
        }
    }

    void ResetColor()
    {
        if (mr != null) mr.material.color = Color.white;
    }

    void Die()
    {
        Debug.Log("보스 처치 완료! 게임 클리어!");
        Destroy(gameObject);
    }

    // 플레이어와 충돌 시재시작
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}