using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public int maxHp_;
    [SerializeField] public int currentHp_;
    [SerializeField] public float invincibility_time_;
    SpriteRenderer sr_;
    SpriteRenderer sr_halo_;
    bool is_invincible_;
    float invincibility_start_time_;

  

    // Start is called before the first frame update
    void Awake()
    {
        currentHp_ = maxHp_;
        sr_ = GetComponent<SpriteRenderer>();
        sr_halo_ = transform.GetChild(1).GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        is_invincible_ = isInvincible();
        bool prev_is_invincible = is_invincible_;
        if(is_invincible_)
        {
            sr_.color = new Color(sr_.color.r, sr_.color.g, sr_.color.b, 0.5f);
            sr_halo_.color = new Color(sr_halo_.color.r, sr_halo_.color.b, sr_halo_.color.g, 0.5f);
        }
        else
        {
            sr_.color = new Color(sr_.color.r, sr_.color.g, sr_.color.b, 1);
            sr_halo_.color = new Color(sr_halo_.color.r, sr_halo_.color.b, sr_halo_.color.g, 1);
        }

        prev_is_invincible = is_invincible_;
        
    }
  
    public void TakeDamage(int damage)
    {
        if (currentHp_ > 0 && !isInvincible())
        {
            currentHp_ -= damage;
            invincibility_start_time_ = Time.time;
        }
    }
    public bool isInvincible()
    {
       if(Time.time - invincibility_start_time_ > invincibility_time_)
        {
            //sr_.color = new Color(sr_.color.r, sr_.color.b, sr_.color.g, 1);
            //sr_halo_.color = new Color(sr_halo_.color.r, sr_halo_.color.b, sr_halo_.color.g,1);
            return false;
        }
        else
        {
            //sr_.color = new Color(sr_.color.r, sr_.color.b, sr_.color.g, 0.5f);
            //sr_halo_.color = new Color(sr_halo_.color.r, sr_halo_.color.b, sr_halo_.color.g, 0.5f);
            return true;
        }
    }
    public void HealHp(int healing)
    {
        currentHp_ += healing;
    }

    public int getCurrentHp()
    {
        return currentHp_;
    }

    public int getMaxHp()
    {
        return maxHp_;
    }

    public void addMaxHp(int additionalMaxHp)
    {
        maxHp_ += additionalMaxHp;
    }

    public void subtractMaxHp(int negatedMaxHp)
    {
        if(negatedMaxHp >= maxHp_)
        {
            negatedMaxHp = maxHp_ - 2;
        }
        maxHp_ -= negatedMaxHp;
        if(currentHp_>maxHp_)
        {
            currentHp_ = maxHp_;
        }
    }
}
