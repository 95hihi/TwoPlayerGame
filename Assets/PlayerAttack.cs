using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public KeyCode attackKey; // 攻擊按鍵（可在 Inspector 設定）
    public float attackRange = 1.0f; // 攻擊範圍
    public float attackCooldown = 0.5f; // 攻擊冷卻時間
    public LayerMask enemyLayer; // 目標圖層，例如「敵人」圖層

    private float nextAttackTime = 0f;

    void Update()
    {
        // 攻擊輸入檢測與冷卻時間
        if (Input.GetKeyDown(attackKey) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown; // 設定冷卻時間
        }
    }

    void Attack()
    {
        // 根據角色朝向 (localScale.x) 決定攻擊方向
        float direction = transform.localScale.x > 0 ? 1f : -1f;

        // 計算攻擊位置，根據角色朝向進行偏移
        Vector3 attackPosition = transform.position + Vector3.right * direction * 0.5f;

        // 在攻擊範圍內檢測敵人
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name); // 打印出擊中的目標
            // 呼叫目標的 TakeDamage 函數
            enemy.GetComponent<PlayerHealth>()?.TakeDamage(10);  // 假設傷害值為 10
        }
    }

    // 在 Scene 視窗中顯示攻擊範圍 (方便調整)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // 考慮角色朝向 (localScale.x 正負值來決定方向)
        float direction = transform.localScale.x > 0 ? 1f : -1f;
        Vector3 attackPosition = transform.position + Vector3.right * direction * 0.5f;

        Gizmos.DrawWireSphere(attackPosition, attackRange);
    }
}
