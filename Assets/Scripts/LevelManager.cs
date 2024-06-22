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
        public float gameDuration = 10 * 60f; // Total game duration in seconds
    private float remainingTime;
    private bool levelCompleted = false;
    private bool playerAlive = true;
    private int SceneNum;
    private bool BossDead;
    public bool upgradeDone;
    public bool upgradeDead;


    void Awake(){
            enemiesKilled = 0;
            keysCollected = 0;
         makeInstance();
         BossDead=false;
         upgradeDone = false;

    }
    void makeInstance(){
    if(instance == null) {instance = this;}
}

    void Start()
    {
       makeInstance();
                   remainingTime = gameDuration;
    }

    void Update()
    {
        if (!levelCompleted && playerAlive)
        {
           SceneNum= SceneManager.GetActiveScene().buildIndex;
           //print(SceneNum);
            switch(SceneNum)
            {case 1:
              //  print("enemiesKilled:"+enemiesKilled+ " total enemies:"+totalEnemies );
           // print("KeysCollectet:"+keysCollected +" total keys: "+ totalKeys);
            // Check if all objectives are completed
            if ((enemiesKilled >= totalEnemies) && (keysCollected >= totalKeys))
            {
               // print("false positive happened");
                CompleteLevel();
            }
            break;
            case 2:
            print("switch working"+SceneNum);
                    if (upgradeDone==true){
                        CompleteLevel();
                    }
                    if (upgradeDead==true){
                        EndLevel(false);
                    }
            break;
            case 3:
                if (BossDead){CompleteLevel();}
            break;

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
    public void upgradeDied(){
        upgradeDead=true;
    }
    public void upgradeFinished(){
        upgradeDone=true;
    }
    public void BossDied(){
    BossDead=true;
}

    public void PlayerDied()
    {
        print("also invoked");
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
        print("made it to endLEvel");
        StopAllCoroutines();
        if (success)
        {   
           // int SceneNum= SceneManager.GetActiveScene().buildIndex;
            switch(SceneNum)
            {case 1:
             UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
             break;
             case 2:
               UnityEngine.SceneManagement.SceneManager.LoadScene("BossFight");
             break;
             case 3:
               UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
             break;}
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
            SceneManager.LoadScene(SceneNum);          
    }

}
