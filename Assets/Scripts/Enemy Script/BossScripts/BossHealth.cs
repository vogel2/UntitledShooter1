using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    // Start is called before the first frame update
   private enemyAnimaitor enemyAnim;
    private NavMeshAgent navAgent;
    private BossScripts bossSci;
    private enemyManager enManager;
   
    public float health=20f;
    private bool isDead=false;
    private Transform target;
     public Slider HealthBar;
 

    // Start is called before the first frame update
    void Awake()
    {
        enemyAnim=GetComponent<enemyAnimaitor>();
        bossSci=GetComponent<BossScripts>();
        navAgent=GetComponent<NavMeshAgent>();
    }
    public void applyDamage(float damage){
       
     if(isDead==false){
        health-=damage;
        HealthBar.value=health;
        if(health<=0f){
            isDead=true;
            print("boss died");
         bossSci.bState=bossState.dead;
         FinalLevelManager.instance.BossDied();
        }}
    }
}
