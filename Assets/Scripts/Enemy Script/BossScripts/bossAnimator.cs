using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnimator : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim=GetComponent<Animator>();
    }
   public void Walk(bool walk){

        anim.SetBool(AnimationTags.WALK_PARAMETER,walk);

    }
    public void Run(bool run){

        anim.SetBool(AnimationTags.RUN_PARAMETER,run);
    }
    public void Attack(){

        anim.SetTrigger(AnimationTags.ATTACK_TRIGGER);
    }
    public void Dead(bool Dead){

        anim.SetBool(AnimationTags.DEAD_PARAMETER,Dead);
    }
    public void SpecialAttack(){
        anim.SetTrigger(AnimationTags.SPECIALATTACK_TRIGGER);
        
    }
    public void resetSpecial(){
        anim.ResetTrigger(AnimationTags.SPECIALATTACK_TRIGGER);
    }
    public void resetAttack(){
        anim.ResetTrigger(AnimationTags.ATTACK_TRIGGER);
    }
    
    public void inrange(bool inRange){
        anim.SetBool(AnimationTags.INRANGE_PARAMETER,inRange);
    }
     public void jump(bool jump){
        //anim.SetBool(AnimationTags.JUMP_PARAMETER,jump);
    }
         
    }
    

