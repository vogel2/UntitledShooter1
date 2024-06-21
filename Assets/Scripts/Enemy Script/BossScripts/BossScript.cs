using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Security.Cryptography;
using TreeEditor;

//read explaination
   public enum bossState{
chase,
attack,
dead,
SpecialAttack

}

public class BossScripts : MonoBehaviour{


    private bossAnimator bossAnim;
    private NavMeshAgent navAgent;
    public bossState bState;
    public float walkSpeed = 4f; //enemy walk speed
    public float runSpeed = 9f; // enemy run speed
    public float chaseDistance=20f; // how far the before the enemy will chase the player
    private float currentChaseDistance;
    public float attackDistance =1.8f; // how far before the enemy starts attacking
    public float chaseAfterAttackDistance =2f; // how far the player can move away before the enemy runs after them
    public float waitBeforeAttack=5f; 
    private float attackTimer;
    private float specialTimer;
    private Transform target;
    public float SpecialCooldown= 3f;
public bool isdead=false;
    public GameObject Ring;
    public float ringRadius=15f; 
    public GameObject attackPoint;
    private float expectedEndSpecial;
    private bool inAttack=false;
        void Awake()
    {
        bossAnim=GetComponent<bossAnimator>();
        navAgent=GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
 
     //  navAgent.isStopped=true;

    }
    void Start(){
        attackTimer=waitBeforeAttack;
        currentChaseDistance=chaseDistance;
        InvokeRepeating("SpecialStateChange",SpecialCooldown,SpecialCooldown);
    }
    void Update()
    {
       // Invoke("changeState",SpecialCooldown);
      //  specialTimer += Time.deltaTime;
       
        switch(bState){
            case bossState.chase:
             chase();
            break;
            case bossState.attack:
            attack();
            break;
            case bossState.dead:
            dead();
            break;
            case bossState.SpecialAttack:
            SpecialAttacks();
            break;
            
      
        }
    }
    

    void chase(){
        /*
             if(bState==bossState.dead){
            dead();
        }
        */
        navAgent.speed = runSpeed;
        navAgent.isStopped=false;
        navAgent.acceleration= 4f;
        navAgent.SetDestination(target.position);
       
      
           if(navAgent.velocity.sqrMagnitude>0){
                bossAnim.Run(true);
                
             }
        else{
            bossAnim.Run(false);
               //navAgent.isStopped=false;
        }

        if(UnityEngine.Vector3.Distance(transform.position,target.position) <= attackDistance){
         bossAnim.Run(false);
         bossAnim.Walk(false);
         bState=bossState.attack;

            if(chaseDistance != currentChaseDistance){
                chaseDistance = currentChaseDistance;

            }
    
        }

    }
    void attack(){
        /*
             if(bState==bossState.dead){
                dead();
               }
    */
        navAgent.velocity= UnityEngine.Vector3.zero;
        navAgent.acceleration= 0f;
        navAgent.isStopped=true;
        attackTimer += Time.deltaTime;
         
        if(attackTimer>waitBeforeAttack){   
            bossAnim.Attack();
            attackTimer=0f;
        }// play attack animation
 
        if(UnityEngine.Vector3.Distance(transform.position, target.position) > attackDistance +chaseAfterAttackDistance ){
            bossAnim.resetAttack();
            bState=bossState.chase;
        } 
    }
    void SpecialStateChange(){
        
        bState=bossState.SpecialAttack;
        print("got invoked");
        
       
    }

    void SpecialAttacks(){

            if(!isdead){
               
                if(inAttack==false){
                    navAgent.velocity= UnityEngine.Vector3.zero;
                    navAgent.isStopped=true;
                    navAgent.acceleration= 0f;
                    bossAnim.SpecialAttack();
                    if(UnityEngine.Vector3.Distance(transform.position,target.transform.position) <= ringRadius){
                        bossAnim.inrange(true);
                    }
                    inAttack=true;

                    expectedEndSpecial=Time.time+3f;
                    }

                    if(Time.time>expectedEndSpecial){
                 //       print("what?");
                                inAttack=false;
                                bossAnim.inrange(false); 
                                bState=bossState.attack;
                                bossAnim.resetSpecial();
                        
                    }}
                    
    }   
    
     void Turn_ON_AttackPoint(){
        attackPoint.SetActive(true);
    }

    void Turn_Off_AttackPoint(){
        if(attackPoint.activeInHierarchy){
            attackPoint.SetActive(false);
        }
    }
    void destroyModel(){
        if(gameObject.activeInHierarchy){
            Destroy(this.gameObject);
        }

    }
    public bossState bossState{
        get;set;
    }
    void dead(){
        navAgent.isStopped = true;
        navAgent.velocity= UnityEngine.Vector3.zero;
        navAgent.acceleration=0f;
        bossAnim.Dead(true);
        isdead=true;
    }
    }
        
    


