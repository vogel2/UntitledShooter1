using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum enemyState{
patrol,
chase,
attack,
dead,
alert

}
public class enemyController : MonoBehaviour
{   
    private LevelManager lel;
    private enemyAnimaitor enemy_anim;
    private NavMeshAgent navAgent;
    public enemyState enState;
    public float walkSpeed = 2f; //enemy walk speed
    public float runSpeed = 7f; // enemy run speed
    public float chaseDistance=20f; // how far the before the enemy will chase the player
    private float currentChaseDistance;
    public float attackDistance =1.8f; // how far before the enemy starts attacking
    public float chaseAfterAttackDistance =1f; // how far the player can move away before the enemy runs after them
    public float patrolRadiusMin =10f, patrotRadiusMax=20f;// how far the enemy will patrol
    public float patrolDuration=5f; // patrol for this time
    public float patrolTimer; 
    public float waitBeforeAttack=2f; 
    private float attackTimer;
    private Transform target;


    public int entype;
    // Start is called before the first frame update
    
    public GameObject attackPoint;
     public GameObject attackPoint2;
        void Awake()
    {
        enemy_anim=GetComponent<enemyAnimaitor>();
        navAgent=GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
      //  GameObject copy= gameObject;
        lel=GetComponent<LevelManager>();

    }
    void Start(){
        enState= enemyState.patrol;
        patrolTimer=patrolDuration;

        attackTimer=waitBeforeAttack;
        currentChaseDistance=chaseDistance;


    }
    // Update is called once per frame
    void Update()
    {
        switch(enState){
            case enemyState.patrol:
              patrol();
            break;
            case enemyState.chase:
             chase();
            break;
            case enemyState.attack:
            attack();
            break;
            case enemyState.dead:
            dead();
            break;
      
        }
    }
    void patrol(){

             if(enState==enemyState.dead){
            dead();
        }
        navAgent.isStopped=false;//enemy is patrolling allow movement
        navAgent.speed=walkSpeed;// the movement speed is equal to the walkspeed
        navAgent.acceleration=4;
        patrolTimer += Time.deltaTime;// add time to the patrol timer
        
        if(patrolTimer > patrolDuration){
            setNewDest();
            patrolTimer=0f;
        }//set a new patrol location and reset the timer

        if(navAgent.velocity.sqrMagnitude>0){
            enemy_anim.Walk(true);
        }
        else{
            enemy_anim.Walk(false);
        }

        if(UnityEngine.Vector3.Distance(transform.position,target.position) <= chaseDistance){
            enemy_anim.Walk(false);
            enState= enemyState.chase;
          
        }
     
    }
    void setNewDest(){
        float randRadius =UnityEngine.Random.Range(patrolRadiusMin, patrotRadiusMax);
        UnityEngine.Vector3 randDir= UnityEngine.Random.insideUnitSphere*randRadius;
        randDir +=transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir,out navHit, randRadius,-1);//make sure the random location is navegationable 
        navAgent.SetDestination(navHit.position); //set the distenation
        
    }

    void chase(){
        
             if(enState==enemyState.dead){
            dead();
        }

        navAgent.isStopped=false;
        navAgent.speed = runSpeed;
        navAgent.SetDestination(target.position);
       
      
           if(navAgent.velocity.sqrMagnitude>0){
                enemy_anim.Run(true);
             }
        else{
            enemy_anim.Run(false);
        }

        if(UnityEngine.Vector3.Distance(transform.position,target.position) <= attackDistance){

         enemy_anim.Run(false);
         enemy_anim.Walk(false);
         enState=enemyState.attack;

            if(chaseDistance != currentChaseDistance){
                chaseDistance = currentChaseDistance;
            
            }
       

        }
         else if(UnityEngine.Vector3.Distance(transform.position,target.position)>chaseDistance){

            enemy_anim.Run(false);
        
            patrolTimer=patrolDuration;
                enState=enemyState.patrol;
             if(chaseDistance != currentChaseDistance){
                chaseDistance = currentChaseDistance;
            
            }
         }
      // enState=enemyState.alert;

    }
    void attack(){
             if(enState==enemyState.dead){
            dead();
        }
       
        navAgent.velocity= UnityEngine.Vector3.zero;
        navAgent.isStopped=true;
        attackTimer += Time.deltaTime;
        if(attackTimer>waitBeforeAttack){    
            enemy_anim.Attack();
            attackTimer=0f;
        }// play attack animation

        if(UnityEngine.Vector3.Distance(transform.position, target.position) > attackDistance +chaseAfterAttackDistance ){
            enState=enemyState.chase;
        } //is the player running away?
         //enState=enemyState.alert;
    }
     void Turn_ON_AttackPoint(){
        attackPoint.SetActive(true);
    }

    void Turn_Off_AttackPoint(){
        if(attackPoint.activeInHierarchy){
            attackPoint.SetActive(false);
        }
    }
    void Turn_ON_AttackPoint2(){
        attackPoint2.SetActive(true);
    }

    void Turn_Off_AttackPoint2(){
        if(attackPoint2.activeInHierarchy){
            attackPoint2.SetActive(false);
        }
    }
    void destroyModel(){
        if(gameObject.activeInHierarchy){
            Destroy(this.gameObject);
        }

    }
    public enemyState EnemyState{
        get;set;
    }
    void dead(){
          navAgent.acceleration=0f;
        navAgent.velocity=UnityEngine.Vector3.zero;
        navAgent.isStopped=true;

        enemy_anim.Dead(true);
   
    }
    public void alert(int i){
         enState=enemyState.chase;
          navAgent.speed = runSpeed;
            GameObject[] objects= GameObject.FindGameObjectsWithTag("Enemy");
            
     
     foreach(GameObject obj in objects){
            if(obj.GetComponent<enemyController>().entype==i)
          {
            obj.GetComponent<NavMeshAgent>().isStopped=false;
             obj.GetComponent<NavMeshAgent>().SetDestination(target.position); 
            obj.GetComponent<NavMeshAgent>().speed=runSpeed;
            obj.GetComponent<enemyController>().chaseDistance=50f;
            obj.GetComponent<enemyAnimaitor>().Run(true);
            if(UnityEngine.Vector3.Distance(obj.transform.position, target.position) <= attackDistance){
                obj.GetComponent<enemyController>().enState=enemyState.attack;
            }
            else if(UnityEngine.Vector3.Distance(obj.transform.position, target.position) <= chaseDistance){
                  obj.GetComponent<enemyController>().enState=enemyState.chase;
            }

            
             }
            }
        }
        }
        
    


