using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // 最大血量
    private int currentHealth;

    public Text healthText;

    public AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;  // 初始化血量
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " 剩餘血量：" + currentHealth);

        // 播放受傷音效
        audioSource.Play();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Update()
    {
        if (healthText != null)
        {
            healthText.text = "血量：" + currentHealth;
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " 已死亡！");
        // 可以添加停用控制、播放死亡動畫等邏輯
        gameObject.SetActive(false);  // 暫時停用玩家
    }
}
