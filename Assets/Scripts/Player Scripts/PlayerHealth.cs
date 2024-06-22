using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;//max health
    public float regenValue=10f;// regeneration rat
    public float regenRate=1f;
    public float currHealth=100f; // player health at any given moment
    public float regenDelay=5f; //time before regen restart when taking damage
    private enemyAnimaitor enemyAnim;
    private NavMeshAgent navAgent;
    private enemyController enemController;
    private enemCont2 enCont2;
     private int SceneNum;
    // Start is called before the first frame update
    public Slider HealthBar;
    void Awake()
    {
         enemyAnim=GetComponent<enemyAnimaitor>();
        enemController=GetComponent<enemyController>();
        navAgent=GetComponent<NavMeshAgent>();
        enCont2=GetComponent<enemCont2>();
    }

    public void applyDamage(float damage){
        currHealth-=damage;
       
        CancelInvoke("Regen");
        print(currHealth);
    
        if(currHealth<=0f){
           print("invoked");
           GameObject[] enemies=  GameObject.FindGameObjectsWithTag("Enemy");
           switch(SceneNum){
            case 1:
            foreach(GameObject obj in enemies){
                obj.GetComponent<enemyController>().enabled=false;
            }
            GetComponent<PlayerMovement>().enabled=false;
            GetComponent<PlayerAttack>().enabled=false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
            break;
            case 2:
            foreach(GameObject obj in enemies){
                obj.GetComponent<enemCont2>().enabled=false;
            }
            GetComponent<PlayerMovement>().enabled=false;
            GetComponent<PlayerAttack>().enabled=false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
            break;
           }
            LevelManager.instance.PlayerDied();
                return;
                
        }
        Invoke("Regen",regenDelay);
        

    }


    public void Regen(){
        
        if (currHealth < health)
        {
            currHealth += regenValue;
            print(currHealth);
            if (currHealth > health)
            {
                currHealth = health;
            }
            else{
                 Invoke("Regen", regenRate);
            }
        }
    }
    void Update(){
        SceneNum= SceneManager.GetActiveScene().buildIndex;
         HealthBar.value=currHealth;
    }
}