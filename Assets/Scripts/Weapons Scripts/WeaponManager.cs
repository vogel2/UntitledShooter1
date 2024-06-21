using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // An array that holds all the available weapons
    [SerializeField]
    private WeaponHandler[] weapons;

    // A variable that holds the index of the current weapon in the weapon array
    private int Current_Weapon_Index;

    // Start is called before the first frame update
    void Start()
    {
        // Assigns the default weapon and sets it active
        Current_Weapon_Index = 0;
        weapons[Current_Weapon_Index].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    // Assigns the number keys to each weapon.
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            TurnOnSelectedWeapon(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            TurnOnSelectedWeapon(1);
        }
    }

    
    void TurnOnSelectedWeapon(int weaponIndex){
        weapons[Current_Weapon_Index].gameObject.SetActive(false);
        weapons[weaponIndex].gameObject.SetActive(true);
        Current_Weapon_Index = weaponIndex;
    }

    public WeaponHandler GetCurrentSelectedWeapon(){
        return weapons[Current_Weapon_Index];
    }
}
