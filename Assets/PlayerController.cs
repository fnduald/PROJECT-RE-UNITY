using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 및 점프 설정")]
    public float speed = 6f;
    public float jumpForce = 12f;

    [Header("슈팅 설정 (5단계 전용)")]
    public GameObject bulletPrefab; // 발사체 프리팹 (총알)
    public Transform firePoint;     // 총알이 나갈 위치
    public bool canShoot = false;   // 인스펙터에서 체크해야 공격 가능

    private Rigidbody2D rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. 좌우 이동 로직
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // 2. 점프 로직 (Space 키)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // 3. 공격 로직 (마우스 왼쪽 클릭 또는 왼쪽 Ctrl)
        // 기존 Z키 대신 더 보편적인 Fire1(마우스 왼쪽)을 사용하도록 수정했습니다.
        if (canShoot && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // 총알 생성
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogWarning("총알 프리팹이나 FirePoint가 연결되지 않았습니다!");
        }
    }

    // 바닥 충돌 감지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}