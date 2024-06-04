using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//read explaination down
public class enemyManager : MonoBehaviour
{
    public static enemyManager instance;
    [SerializeField]//serializedField makes it possible to change private and protected values in the UI
    private GameObject enemy1_prefab;
    [SerializeField]
    private GameObject enemy2_prefab;
    public Transform[] enemy1Locations, enemy2Locations;
    [SerializeField]
    private int enemy1Initial,enemy2Initial;
    private int enemy1Num,enemy2Num;
    public float waitTime=10f;

   void Awake(){
    makeInstance();

   }
    void Start(){
       enemy1Num=enemy1Initial;
        enemy2Num=enemy2Initial;
        spawnEnemies() ;
        StartCoroutine("checkRespawnTimer");

    }
 
    IEnumerator checkRespawnTimer(){ //this makes a timer for the enemy respawn
        yield return new WaitForSeconds(waitTime);
        spawnEn1();
        spawnEn2();

        StartCoroutine("checkRespawnTimer");
    }
void makeInstance(){
    if(instance == null) {instance = this;}
}
void  spawnEnemies(){
    spawnEn1();
    spawnEn2();
}
void spawnEn1(){
    int index=0;
    for(int i=0;i<enemy1Num;i++){
        if(index>=enemy1Locations.Length){index=0;}
        Instantiate(enemy1_prefab, enemy1Locations[index].position, Quaternion.identity);//this clones the prefab at the desired location
        index++;
    }
    enemy1Num=0;// this variable counts how many enemies to be spawned, when all enemies are spawned is resets to zero since all of them were spawned

}
void spawnEn2(){

     int index=0;
    for(int i=0;i<enemy2Num;i++){
        if(index>=enemy2Locations.Length){index=0;}
        Instantiate(enemy2_prefab, enemy2Locations[index].position, Quaternion.identity);// clones the other prefab
        index++;
    }
    enemy2Num=0;// same as enemy1num
}

    public void enemyDied(bool isDead,int enemyType){
        if(isDead==true){
            if(enemyType==1){
                enemy1Num++ ;
                if(enemy1Num>enemy1Initial){
                    enemy1Num=enemy1Initial;
                }
            }
            else if(enemyType==2){
                    enemy2Num++ ;
                if(enemy2Num>enemy2Initial){
                    enemy2Num=enemy2Initial;
                }}
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