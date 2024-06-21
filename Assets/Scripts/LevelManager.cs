using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int totalEnemies=20;
    public int totalKeys=3;
    public Text timerText;
    public Text objectivesText;
    public float levelTime = 300f; // 5 minutes in seconds

    private int enemiesKilled = 0;
    private int keysCollected = 0;
    private float timeRemaining;
    private bool levelCompleted = false;
    private bool playerAlive = true;

    void Awake(){
            enemiesKilled = 0;
            keysCollected = 0;
         makeInstance();
    }
    void makeInstance(){
    if(instance == null) {instance = this;}
}

    void Start()
    {
        timeRemaining = levelTime;
        UpdateObjectivesText();
        StartCoroutine(Timer());
    }

    void Update()
    {
        if (!levelCompleted && playerAlive)
        {
            print("enemiesKilled:"+enemiesKilled+ " total enemies:"+totalEnemies );
           // print("KeysCollectet:"+keysCollected +" total keys: "+ totalKeys);
            // Check if all objectives are completed
            if ((enemiesKilled >= totalEnemies) && (keysCollected >= totalKeys))
            {
               // print("false positive happened");
                CompleteLevel();
            }
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        //UpdateObjectivesText();
        
    }

    public void KeyCollected()
    {
        keysCollected++;
        UpdateObjectivesText();
        
    }

    public void PlayerDied()
    {
        playerAlive = false;
        EndLevel(false);
    }

    private void CompleteLevel()
    {
        levelCompleted = true;
        
        EndLevel(true);
    }

    private void EndLevel(bool success)
    {
        StopAllCoroutines();
        if (success)
        {
            // Show success message and proceed to next level or main menu
            Debug.Log("Level Completed Successfully!");
            // Implement your success logic here
        }
        else
        {
            // Show failure message and reset level or go to main menu
            Debug.Log("Level Failed!");
            print("dead!");
            Invoke("RestartGame",3f);
            // Implement your failure logic here
        }
    }
    void RestartGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

    private IEnumerator Timer()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }
        if (!levelCompleted)
        {
            EndLevel(false); // Fail the level if time runs out
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateObjectivesText()
    {
        objectivesText.text = $"Enemies Killed: {enemiesKilled}/{totalEnemies}\nKeys Collected: {keysCollected}/{totalKeys}";
    }
}
