using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {
    
    public static HUD instance;
    // Start is called before the first frame update
    [HideInInspector]
    public HealthBar healthBar;
    [HideInInspector]
    public HitOverlay hitOverlay;
    [HideInInspector]
    public WeaponInfo weaponInfo;
    [HideInInspector]
    public PickupPanel pickupPanel;




    void Awake() {
        //Singleton
        if (instance == null)
            instance = this;
        else{
            Destroy(gameObject);
        }

        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        hitOverlay = gameObject.GetComponentInChildren<HitOverlay>();
        weaponInfo = gameObject.GetComponentInChildren<WeaponInfo>();
        pickupPanel = gameObject.GetComponentInChildren<PickupPanel>();
        pickupPanel.CloseMessagePanel();

    }





  
}
