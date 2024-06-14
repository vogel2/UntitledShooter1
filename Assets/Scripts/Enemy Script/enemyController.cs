using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum enemyState{
patrol,
chase,
attack

}
public class enemyController : MonoBehaviour
{   
    private enemyAnimaitor enemy_anim;
    private NavMeshAgent navAgent;
    private enemyState enState;
    public float walkSpeed = 0.5f; //enemy walk speed
    public float runSpeed = 4f; // enemy run speed
    public float chaseDistance=7f; // how far the before the enemy will chase the player
    private float currentChaseDistance;
    public float attackDistance =1.8f; // how far before the enemy starts attacking
    public float chaseAfterAttackDistance =2f; // how far the player can move away before the enemy runs after them
    public float patrolRadiusMin =20f, patrotRadiusMax=60f;// how far the enemy will patrol
    public float patrolDuration=15f; // patrol for this time
    public float patrolTimer; 
    public float waitBeforeAttack=2f; 
    private float attackTimer;
    private Transform target;

    public int type;
    // Start is called before the first frame update
    
    public GameObject attackPoint;
     public GameObject attackPoint2;
        void Awake()
    {
        enemy_anim=GetComponent<enemyAnimaitor>();
        navAgent=GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
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
        }
    }
    void patrol(){

        navAgent.isStopped=false;//enemy is patrolling allow movement
        navAgent.speed=walkSpeed;// the movement speed is equal to the walkspeed
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

        if(Vector3.Distance(transform.position,target.position) <= chaseDistance){
            enemy_anim.Walk(false);
            enState= enemyState.chase;
          
        }

    }
    void setNewDest(){
        float randRadius =UnityEngine.Random.Range(patrolRadiusMin, patrotRadiusMax);
        Vector3 randDir= UnityEngine.Random.insideUnitSphere*randRadius;
        randDir +=transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir,out navHit, randRadius,-1);//make sure the random location is navegationable 
        navAgent.SetDestination(navHit.position); //set the distenation
        
    }

    void chase(){
        navAgent.isStopped=false;
        navAgent.speed = runSpeed;
        navAgent.SetDestination(target.position);

           if(navAgent.velocity.sqrMagnitude>0){
                enemy_anim.Run(true);
             }
        else{
            enemy_anim.Run(false);
        }

        if(Vector3.Distance(transform.position,target.position) <= attackDistance){

         enemy_anim.Run(false);
         enemy_anim.Walk(false);
         enState=enemyState.attack;

            if(chaseDistance != currentChaseDistance){
                chaseDistance = currentChaseDistance;
            
            }
       

        }
         else if(Vector3.Distance(transform.position,target.position)>chaseDistance){

            enemy_anim.Run(false);
        
            patrolTimer=patrolDuration;
                enState=enemyState.patrol;
             if(chaseDistance != currentChaseDistance){
                chaseDistance = currentChaseDistance;
            
            }
         }
        

    }
    void attack(){
        navAgent.velocity= Vector3.zero;
        navAgent.isStopped=true;
        attackTimer += Time.deltaTime;
        if(attackTimer>waitBeforeAttack){
            
            enemy_anim.Attack();
            attackTimer=0f;
        }// play attack animation

        if(Vector3.Distance(transform.position, target.position) > attackDistance +chaseAfterAttackDistance ){
            enState=enemyState.chase;
        } //is the player running away?

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
}

