using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;



public class enemCont2 : MonoBehaviour
{private LevelManager lel;
    private enemyAnimaitor enemy_anim;
    private NavMeshAgent navAgent;
    public enemyState enState;
    private PlayerDamage damageScript1;
    private PlayerDamage damageScript2;
    public float runSpeed = 7f; // enemy run speed
    public float chaseDistance=20f; // how far the before the enemy will chase the player
    private float currentChaseDistance;
    public float attackDistance =1.8f; // how far before the enemy starts attacking
    public float chaseAfterAttackDistance =1f; // how far the player can move away before the enemy runs after them
 private UpgradePointController upgradePointController;
  public float damage = 10f;
     public float waitBeforeAttack=2f; 
    private float attackTimer;
    private GameObject PlayerTarget;
    private GameObject UpTarget;
    private GameObject target;
    public GameObject attackPoint;
     public GameObject attackPoint2;
        void Awake()
    {
        enemy_anim=GetComponent<enemyAnimaitor>();
        navAgent=GetComponent<NavMeshAgent>();
        PlayerTarget = GameObject.FindWithTag(Tags.PLAYER_TAG);
        UpTarget=GameObject.FindWithTag(Tags.Upgrade_Point);
        GameObject copy= gameObject;
        lel=GetComponent<LevelManager>();
        damageScript1=attackPoint.GetComponent<PlayerDamage>();
        damageScript2=attackPoint2.GetComponent<PlayerDamage>();
        GameObject upgradePointObj = GameObject.FindWithTag("UpgradePoint");
         upgradePointController = upgradePointObj.GetComponent<UpgradePointController>();

    }
    void Start(){
        enState= enemyState.chase;
        attackTimer=waitBeforeAttack;
        currentChaseDistance=chaseDistance;
        
    }
    // Update is called once per frame
    void Update()
    {
           if(Vector3.Distance(transform.position,PlayerTarget.transform.position)<=chaseDistance){
           target=PlayerTarget;
        }
        else{
          target=UpTarget;
        }

        switch(enState){
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


    void chase(){
        
             if(enState==enemyState.dead){
            dead();
        }

        navAgent.isStopped=false;
        navAgent.speed = runSpeed;

        navAgent.SetDestination(target.transform.position);
      
           if(navAgent.velocity.sqrMagnitude>0){
                enemy_anim.Run(true);
             }
        else{
            enemy_anim.Run(false);
        }
        
        if(UnityEngine.Vector3.Distance(transform.position,target.transform.position) <= attackDistance){

         enemy_anim.Run(false);
         enemy_anim.Walk(false);
         enState=enemyState.attack;

            if(chaseDistance != currentChaseDistance){
                chaseDistance = currentChaseDistance;
            
            }
       

        }
        /*
         else if(UnityEngine.Vector3.Distance(transform.position,PlayerTarget.position)>chaseDistance){

        
             if(chaseDistance != currentChaseDistance){
                chaseDistance = currentChaseDistance;
            
            }
         }
         */
      // enState=enemyState.alert;

    }
    void attack(){
       
        navAgent.velocity= UnityEngine.Vector3.zero;
        navAgent.isStopped=true;
        attackTimer += Time.deltaTime;
        if(attackTimer>waitBeforeAttack){  
            if(target==PlayerTarget){
                 damageScript1.layerMask = 1 << 8;
                 damageScript2.layerMask = 1 << 8;
                     enemy_anim.Attack();
            attackTimer=0f;
            }  
            else{
                 upgradePointController.TakeDamage(damage);
                 damageScript1.layerMask=0;
                 damageScript2.layerMask=0;
                 enemy_anim.Attack();

            attackTimer=0f;
            }
            
        }// play attack animation

        if(Vector3.Distance(transform.position, target.transform.position) > attackDistance +chaseAfterAttackDistance ){
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
    public void dead(){
        enemy_anim.Dead(true);
   
    }
   
        }
        
    




