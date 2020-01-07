using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] public int damage_;
    DamagePopup damage_popup;
    // Start is called before the first frame update
    void Start()
    {
        damage_popup = GameObject.Find("World").GetComponent<DamagePopup>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HealthComponent>() != null)
        {
            damage_popup.Create(collision.gameObject, damage_, false);
            collision.gameObject.GetComponent<HealthComponent>().TakeDamage(damage_);
        }
    }
}
