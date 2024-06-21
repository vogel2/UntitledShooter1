using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    // Reference to the WeaponManager component
    private WeaponManager weapon_Manager;

    // Fire rate of the weapon
    public float fireRate = 15f;
    // Time until the next fire is allowed
    private float nextTimeToFire;
    // Damage inflicted by the weapon
    public float damage = 20f;

    // Reference to the Animator for the zoom camera
    private Animator zoomCameraAnim;

    // Boolean to track if the player is zoomed in
    private bool zoomed;

    // Reference to the main camera
    private Camera mainCam;

    // Reference to the crosshair GameObject
    private GameObject crosshair;

    // Boolean to track if the player is aiming
    private bool is_Aiming;

    // Reference to the PlayerMovement component
    private PlayerMovement playerMovement;

    // Awake is called when the script instance is being loaded
    void Awake() {
        // Get the WeaponManager component attached to the player
        weapon_Manager = GetComponent<WeaponManager>();

        // Find and get the Animator component for the zoom camera
        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();

        // Find the crosshair GameObject by tag
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

        // Get the PlayerMovement component attached to the player
        playerMovement = GetComponent<PlayerMovement>();

        // Get the main camera
        mainCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
         if (PauseMenu.GameIsPaused)
         {
            return;
         }
        WeaponShoot();
        ZoomInAndOut();
    }

    // Method to handle weapon shooting logic
    void WeaponShoot() {

        // Check if the current weapon belongs to multiple shots fire type
          // Single shot fire type
            // If the left mouse button is clicked once
            if(Input.GetMouseButtonDown(0)) {
                // Handle single shot for melee weapon like an axe
                if(weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG) {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                // Handle single shot for bullet type weapons
                if(weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET) {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                    BulletFired();
                } else { // Handle aiming specific shooting
                    if(is_Aiming) {
                        weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                    }
                }
        }
    }

    // Method to handle zooming in and out
    void ZoomInAndOut() {
        // Check if the weapon supports aiming
        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM) {
            // If the right mouse button is pressed down, start zooming in
            if(Input.GetMouseButtonDown(1)) {
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crosshair.SetActive(false);
            }
            // If the right mouse button is released, start zooming out
            if(Input.GetMouseButtonUp(1)) {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crosshair.SetActive(true);
            }
        }
        
    }
   void BulletFired(){
        RaycastHit hit;
        if(Physics.Raycast(mainCam.transform.position,mainCam.transform.forward,out hit)){ 
            if(hit.transform.tag==Tags.ENEMY_TAG){
                 hit.transform.GetComponent<enemyHealth>().applyDamage(damage);
            //print("hit " + hit.transform.gameObject.tag);
            }
            else if(hit.transform.tag==Tags.BOSS_TAG){
                 hit.transform.GetComponent<BossHealth>().applyDamage(damage);
            //print("hit " + hit.transform.gameObject.tag);
            }
           
        }
    }
}
