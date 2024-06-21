using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

        

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int totalEnemies=20;
    public int totalKeys=3;

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
        
    }

    public void KeyCollected()
    {
        keysCollected++;
    
        
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

}
