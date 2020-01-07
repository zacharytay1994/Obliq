using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] GameObject damage_popup_prefab;
    private TextMeshPro text_mesh_;
    public GameObject Create(GameObject damaged_object, int damage_amt, bool is_crit)
    {
       
            GameObject damage_Popup = Instantiate(damage_popup_prefab, (Vector2)damaged_object.transform.position, Quaternion.identity);
            if (damaged_object.GetComponent<ImAProjectile>() != null)
            {
                damage_Popup.GetComponent<DamagePopupCosmetics>().origin = damaged_object.GetComponent<ImAProjectile>().movement_heading_;
                damage_Popup.GetComponent<DamagePopupCosmetics>().is_crit = is_crit;
            }
            else
            {
                damage_Popup.GetComponent<DamagePopupCosmetics>().origin = new Vector2(1, 2);
                damage_Popup.GetComponent<DamagePopupCosmetics>().is_crit = is_crit;
            }
            text_mesh_ = damage_Popup.GetComponent<TextMeshPro>();
            text_mesh_.SetText(damage_amt.ToString());
            return damage_Popup;
        
     
      
       
       
    }
   
}
