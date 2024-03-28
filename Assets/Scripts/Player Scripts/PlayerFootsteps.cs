using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footstep_Sound;

    /*This step is done to enable the manipulation of the AudioClip array in the unity inspector window. */
    [SerializeField]
    private AudioClip[] footstep_Clip;

    // This reference is created to know if the player is on the ground, moving etc 
    private CharacterController character_Controller;

    //     
    [HideInInspector]
    public float volume_Min, volume_Max; 

    /* This variable is created to keep track of the distance the player could move in any state
    e.g. walking, sprinting, before the sound is played*/
    private float accumulated_Distance; 

    [HideInInspector]
    // This variable is created to determine the size of the player's step 
    public float step_Distance;


    // Start is called before the first frame update
    void Awake() {
        footstep_Sound = GetComponent<AudioSource>();
        character_Controller = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update(){

        CheckToPlayFootstepSound();
        
      
        void CheckToPlayFootstepSound(){

            // The return in this void function is written to exit function
            if(!character_Controller.isGrounded)
                return;

            // To determine if the player is moving
            if(character_Controller.velocity.sqrMagnitude > 0){
                
                accumulated_Distance += Time.deltaTime;

                // To check if the distance move is equal to the assigned step distance
                if(accumulated_Distance > step_Distance){
                    
                    // To play a randomised footstep sound effect with a random volume
                    footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                    footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                    footstep_Sound.Play();

                    accumulated_Distance = 0f;
                }
            } else {
                accumulated_Distance = 0f;
            }
        };

        
    }
}
