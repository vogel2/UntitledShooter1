using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
//read explaination down
public class enemyManager2 : MonoBehaviour
{
    public static enemyManager2 instance;
    [SerializeField]//serializedField makes it possible to change private and protected values in the UI
    private GameObject enemy1_prefab;
    [SerializeField]
    private GameObject enemy2_prefab;
    public Transform[] key1Locations, key2Locations,key3Locations;
    public Transform armouredEnemy1Loc,armouredEnemy2Loc,armouredEnemy3Loc;
    [SerializeField]
    private int enInt1,enInt2,enInt3,enInt4,enInt5,enInt6;
    private int enemy1Num,enemy2Num,enemy3Num,enemy4Num,enemy5Num,enemy6Num;
    public float waitTime=20f;
   // public 

   void Awake(){
    makeInstance();

   }
    void Start(){
       enemy1Num=enInt1;
        enemy2Num=enInt2;
        enemy3Num=enInt3;
        enemy4Num=enInt4;
        enemy5Num=enInt5;
        enemy6Num=enInt6;
        spawnEnemiesInt() ;
        StartCoroutine("checkRespawnTimer");

    }
 void  spawnEnemiesInt(){
    spawnEn1();
    spawnEn2();
    spawnEn3();
    spawnEn4();
    spawnEn5();
    spawnEn6();
}
    IEnumerator checkRespawnTimer(){ //this makes a timer for the enemy respawn
        yield return new WaitForSeconds(waitTime);
        spawnEnemiesInt();
        StartCoroutine("checkRespawnTimer");
    }
void makeInstance(){
    if(instance == null) {instance = this;}
}



void spawnEnemies(GameObject enemyPrefab, Transform[] enemyLocations, ref int enemyNum, int enType)
    {
        int index = 0;
        for (int i = 0; i < enemyNum; i++)
        {
            if (index >= enemyLocations.Length)
            {
                index = 0;
            }
            GameObject copy = Instantiate(enemyPrefab, enemyLocations[index].position, Quaternion.identity);
            enemyController scriptCopy = copy.GetComponent<enemyController>();
            scriptCopy.entype = enType;
            index++;
        }
        enemyNum = 0; // Reset enemy count after spawning
    }

    void spawnEnemy(GameObject enemyPrefab, Transform enemyLocation, ref int enemyNum, int enType)
    {
        if (enemyNum != 0)
        {
            GameObject copy = Instantiate(enemyPrefab, enemyLocation.position, Quaternion.identity);
            enemyController scriptCopy = copy.GetComponent<enemyController>();
            scriptCopy.entype = enType;
            enemyNum = 0; // Reset enemy count after spawning
        }
    }
void spawnEn1(){
    int counter=0;
     GameObject[] objects= GameObject.FindGameObjectsWithTag("Enemy");
     
     foreach(GameObject obj in objects){
        if(obj.GetComponent<enemyController>().entype==1){
            counter++;
        }
     }
     int num =counter + enemy1Num;
     //print("counter is " + counter +" num1 before is "+ enemy1Num+ " total = "+ num );
     if(counter+enemy1Num<=4){int index = 0;
        {for (int i = 0; i < enemy1Num; i++)
        {
            if (index >= key1Locations.Length)
            {
                index = 0;
            }
            GameObject copy = Instantiate(enemy1_prefab, key1Locations[index].position, Quaternion.identity);
            enemyController scriptCopy = copy.GetComponent<enemyController>();
            scriptCopy.entype = 1;
            index++;
        }enemy1Num = 0; }
        // Reset enemy count after spawning}
  //  spawnEnemies( enemy1_prefab, key1Locations, ref enemy1Num,1);
     //print("num1 after is "+enemy1Num);
    counter=0;
     }
}
void spawnEn2(){

     spawnEnemies( enemy1_prefab, key2Locations, ref enemy2Num,2);
}
void spawnEn3(){
spawnEnemies( enemy1_prefab, key3Locations, ref enemy3Num,3);
    
}
void spawnEn4(){

    spawnEnemy(enemy2_prefab, armouredEnemy1Loc, ref enemy4Num,4);
}
void spawnEn5(){

   spawnEnemy(enemy2_prefab, armouredEnemy2Loc, ref enemy5Num,5);
   
}
void spawnEn6(){

    spawnEnemy(enemy2_prefab, armouredEnemy3Loc, ref enemy6Num, 6);
}

    public void enemyDied(int enemyType) {
     // print("made it to manager");
        switch (enemyType) {
            case 1:
                enemy1Num++;
                   
                if (enemy1Num > enInt1) {
                    enemy1Num = enInt1;
                }
                break;
            case 2:
                enemy2Num++;
                if (enemy2Num > enInt2) {
                    enemy2Num = enInt2;
                }
                break;
            case 3:
                enemy3Num++;

                if (enemy3Num > enInt3) {
                    enemy3Num = enInt3;
                }
                break;
            case 4:
                enemy4Num++;

                if (enemy4Num > enInt4) {
                    enemy4Num = enInt4;
                }
                break;
            case 5:
                enemy5Num++;
                if (enemy5Num > enInt5) {
                    enemy5Num = enInt5;
                }
                break;
            case 6:
                enemy6Num++;
                if (enemy6Num > enInt6) {
                    enemy6Num = enInt6;
                }
                break;
                default:
               // print("unexpected error");
                break;
        
    }
}
    public void stopRespawn(){
        StopCoroutine("checkRespawnTimer");
    }
}

//this scripts is responsible for spawning the enemies in the desired locations
//it does so by taking the enemies you want to spawn in (in this cas enemy1 and enemy 2)(change as we see fit later)
//it creates an array of type transform for each enemy (transfrom is the location in 3d)
//the arrays take an argument in unity for the size, and are filled with the coordinates 
//the script also takes the number of each enemy to spawn intially
//the script keeps track of enemies that die and spawn in an enemy of the same type after a while
// the respawn will be more useful in the defence mission
//could also be slightly modified to make spawning in enemies easier or to potentially 