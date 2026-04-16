using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    private bool movingRight = true;
    public Transform groundCheck; // 적 발앞에 위치시킬 빈 오브젝트.

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // 바닥 체크 (낭떠러지 감지)
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 2f);
        if (groundInfo.collider == false)
        {
            if (movingRight) { transform.eulerAngles = new Vector3(0, -180, 0); movingRight = false; }
            else { transform.eulerAngles = new Vector3(0, 0, 0); movingRight = true; }
        }
    }
}