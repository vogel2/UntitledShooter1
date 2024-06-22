using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth2 : MonoBehaviour
{
        public float health=20f;
    private bool isDead=false;
    private enemCont2 enCon2;

    // Start is called before the first frame update
    void Awake(){
         enCon2=GetComponent<enemCont2>();
    }
   
    public void applyDamage(float damage){
        health-=damage;
        if(health<=0f&& isDead==false){
        isDead=true;
        enCon2.dead();
        }}

    // Update is called once per frame
    void Update()
    {
        
    }
}
