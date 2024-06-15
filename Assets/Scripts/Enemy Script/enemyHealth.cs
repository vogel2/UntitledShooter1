using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{   public enum enemyType{
    armoured,
    notArmoured,
    boss
}
    private enemyAnimaitor enemyAnim;
    private NavMeshAgent navAgent;
    private enemyController enemController;
    private enemyManager enManager;
    private LevelManager lel;
    public float health=20f;
    private bool isDead;
    public enemyType enType;
    private Transform target;

    // Start is called before the first frame update
    void Awake()
    {
       
        enemyAnim=GetComponent<enemyAnimaitor>();
        enemController=GetComponent<enemyController>();
        navAgent=GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
       // lel=GetComponent<LevelManager>();

    }
    public void applyDamage(float damage){
        health-=damage;
        if(health<=0f){
        isDead=true;
       int num=this.gameObject.GetComponent<enemyController>().entype;
      // print("type is "+num);
       enemController.enState=enemyState.dead;
      enemyManager.instance.enemyDied(num);
       LevelManager.instance.EnemyKilled();
         
            return;}
        //print(health);
        if(enemController.EnemyState == enemyState.patrol){
            navAgent.SetDestination(target.position);
            

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
