using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Volumes for each movement type are assgined to randomise the stepsounds in general
// and different steps are assigned to differentiate between each movement type
public class PlayerSprintAndCrouch : MonoBehaviour
{

    private PlayerMovement playerMovement;

    // Assigning the speed of sprinting, normal moving and crouch moving
    public float sprint_Speed = 10f; 
    public float move_Speed = 5f; 
    public float crouch_Speed = 2f; 


    private Transform look_Root;
    private float stand_Height = 1.6f;
    private float crouch_Height = 1f;

    private bool is_Crouching; 

    private PlayerFootsteps player_Footsteps;

    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.1f;
    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;
    private float sprint_Step_Distance = 0.25f;  
    private float crouch_Step_Distance = 0.4f;  
    private float walk_Step_Distance = 0.5f;  


    
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();


        // Gets the first child of the transform, the index of GetChild is the index of the desired child. In this case it is look root.
        look_Root = transform.GetChild(0); 

        player_Footsteps = GetComponentInChildren<PlayerFootsteps>();
    }

    void Start(){
        player_Footsteps.volume_Min = walk_Volume_Min;
        player_Footsteps.volume_Max = walk_Volume_Max;
        player_Footsteps.step_Distance = walk_Step_Distance;
    }

       // Update is called once per frame
    void Update() {
        Sprint();
        Crouch();
    }

    void Sprint() {

    
        if (Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching){
            playerMovement.speed = sprint_Speed;

            player_Footsteps.step_Distance = sprint_Step_Distance;
            player_Footsteps.volume_Min = sprint_Volume;
            player_Footsteps.volume_Max = sprint_Volume;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching){
            playerMovement.speed = move_Speed;

            player_Footsteps.step_Distance = walk_Step_Distance;
            player_Footsteps.volume_Min = walk_Volume_Min;
            player_Footsteps.volume_Max = walk_Volume_Max;
        }
    }// Sprint

    void Crouch() {

        /* This function checks if the player is crouching or not
         If they're crouching the left ctrl button will change 
         the y value of the local position to a standing height.
         If not the y value would be the crouching height, imitating the normal crouching.
        */ 
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            
            if (is_Crouching){
                // The local position of look root with respect to the player.
                look_Root.localPosition = new Vector3(0f,stand_Height,0f);
                playerMovement.speed = move_Speed;

                player_Footsteps.step_Distance = walk_Step_Distance;
                player_Footsteps.volume_Min = walk_Volume_Min;
                player_Footsteps.volume_Max = walk_Volume_Max;

                is_Crouching = false; 

            } else{
                look_Root.localPosition = new Vector3(0f,crouch_Height,0f);
                playerMovement.speed = crouch_Speed;

                player_Footsteps.step_Distance = crouch_Step_Distance;
                player_Footsteps.volume_Min = crouch_Volume;
                player_Footsteps.volume_Max = crouch_Volume;

                is_Crouching = true; 
            }
        }
    }// Crouch
}

 
