using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePointController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // Make sure this is correctly assigned
    public float maxHealth = 100f;
    public float currHealth = 100f;
    public Slider healthBar;
    public float gameDuration = 10 * 60f; // Total game duration in seconds
    private float remainingTime;

    void Start()
    {
        currHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currHealth;
        remainingTime = gameDuration;
        UpdateTimerUI();
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            remainingTime = 0;
        }
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TakeDamage(float amount)
    {
        currHealth -= amount;
        healthBar.value = currHealth;
        if (currHealth <= 0)
        {
            if (remainingTime <= 5 * 60f) // 5 minutes in seconds
            {
                LoadMainBossScene();
            }
            else
            {
                RestartLevel();
            }
        }
    }

    void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
    }

    void LoadMainBossScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainBoss");
    }
}
