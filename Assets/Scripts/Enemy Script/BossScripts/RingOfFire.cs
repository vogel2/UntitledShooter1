using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfFire : MonoBehaviour
{
   public float damage=5f;
   public float radius=1f;
   public LayerMask layerMask;//change or assign what layer this object interacts with

    // most likely won't be used as it's for melee

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,radius,layerMask);//check using the physics class if the sphere of radius = "radius" has
                                                                                         //touched a surface from the layer given by the layer mask and store it
        if(colliders.Length>0){  //check if collisions where detected
            if(colliders[0].transform.tag=="Player"){

                print("hit detected " + colliders[0].gameObject.tag);//experemental to make sure things work correctly, returns the tag of the gameobject the attack point collided with
                gameObject.SetActive(false);
                colliders[0].gameObject.GetComponent<PlayerHealth>().applyDamage(damage);
            }
            else if(colliders[0].transform.tag=="Enemy")
            { print("hit detected " + colliders[0].gameObject.tag);
                colliders[0].gameObject.GetComponent<enemyHealth>().applyDamage(damage);
                //experemental to make sure things work correctly, returns the tag of the gameobject the attack point collided with
            }
                 
           
        }
    }
}
