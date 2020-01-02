using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackExecuter : MonoBehaviour
{
    bool initialized_ = false;
    bool type_initialized_ = false;
    string type_ = "";
    bool destroy_ = false;
    Collider2D player_collider_ = null;

    /*_________________________________________________*/
    // PULSE WAVE VARIABLES
    /*_________________________________________________*/
    float pulse_rate_ = 0.0f;
    float pulse_rate_counter_ = 0.0f;
    int pulse_amount_ = 0;
    float pulse_thickness_ = 0.0f;
    float pulse_speed_ = 0.0f;
    [SerializeField]
    GameObject circle1;
    [SerializeField]
    GameObject circle2;
    class PulseWavePair {
        private GameObject circle1;
        private GameObject circle2;
        private float second_wave_wait_;

        public void SetCircle1(GameObject circle)
        {
            circle1 = circle;
        }
        public GameObject GetCircle1()
        {
            return circle1;
        }
        public void SetCircle2(GameObject circle)
        {
            circle2 = circle;
        }
        public GameObject GetCircle2()
        {
            return circle2;
        }
        public float GetWait()
        {
            return second_wave_wait_;
        }
        public void SetWait(float wait)
        {
            second_wave_wait_ = wait;
        }

        public bool Collide(Collider2D collider)
        {
            bool one = collider.IsTouching(circle1.GetComponent<CircleCollider2D>());
            bool two = collider.IsTouching(circle2.GetComponent<CircleCollider2D>());
            if ((one && two) || (!one && !two))
            {
                return false;
            }
            return true;
        }
    }
    List<PulseWavePair> pulse_wave_pairs_ = new List<PulseWavePair>();

    /*_________________________________________________*/
    // CONAL LASER VARIABLES
    /*_________________________________________________*/

    private void Start()
    {
        
    }

    private void Update()
    {
        if (initialized_ && type_initialized_)
        {
            switch(type_)
            {
                case "pulsewave":
                    PulseWaveLogic();
                    break;
                case "conallaser":
                    break;
            }
        }
    }

    public void Init(string type, Collider2D player)
    {
        type_ = type;
        player_collider_ = player;
        initialized_ = true;
    }

    /*_________________________________________________*/
    // PULSE WAVE FUNCTIONS
    /*_________________________________________________*/
    public void InitPulse(float pulserate, int pulseamount, float pulsethickness, float pulsespeed)
    {
        pulse_rate_ = pulserate;
        pulse_amount_ = pulseamount;
        pulse_thickness_ = pulsethickness;
        pulse_speed_ = pulsespeed;
        type_initialized_ = true;
    }

    private void PulseWaveLogic()
    {
        if (pulse_amount_ <= 0 && !destroy_)
        {
            destroy_ = true;
        }

        if (destroy_)
        {
            FadePulse();
        }

        if (pulse_rate_counter_ < pulse_rate_)
        {
            pulse_rate_counter_ += Time.deltaTime;
        }
        else
        {
            // spawn another pulsepair
            pulse_rate_counter_ = 0.0f;
            if (!destroy_)
            {
                SpawnPulseWavePair();
            }
            pulse_amount_--;
        }

        // move pulsewaves
        for (int i = 0; i< pulse_wave_pairs_.Count;i++)
        {
            // if second wave isnt ready to move
            if (pulse_wave_pairs_[i].GetWait() > 0.0f)
            {
                float test = pulse_wave_pairs_[i].GetWait() - Time.deltaTime;
                pulse_wave_pairs_[i].SetWait(test);
                float temp = pulse_wave_pairs_[i].GetWait();
            }
            else
            {
                Vector3 original_scale2 = pulse_wave_pairs_[i].GetCircle2().transform.localScale;
                // scale circle2 up
                pulse_wave_pairs_[i].GetCircle2().transform.localScale = original_scale2 + new Vector3(pulse_speed_, pulse_speed_, 0.0f);
            }
            Vector3 original_scale1 = pulse_wave_pairs_[i].GetCircle1().transform.localScale;
            // scale circle1 up
            pulse_wave_pairs_[i].GetCircle1().transform.localScale = original_scale1 + new Vector3(pulse_speed_, pulse_speed_, 0.0f);
            original_scale1 = pulse_wave_pairs_[i].GetCircle1().transform.localScale;

            // check if ring collides with player
            if (pulse_wave_pairs_[i].Collide(player_collider_)) {
                // Put DMG LOGIC HERE OR CHECK I FRAMES OR WHATEVER
            }
        }
    }

    private void SpawnPulseWavePair()
    {
        PulseWavePair pair = new PulseWavePair();
        pair.SetCircle1(Object.Instantiate(circle1, transform));
        pair.SetCircle2(Object.Instantiate(circle2, transform));
        pair.SetWait(pulse_thickness_);
        pulse_wave_pairs_.Add(pair);
    }

    public void FadePulse()
    {
        float fade_rate = 1.0f * Time.deltaTime;
        foreach (PulseWavePair p in pulse_wave_pairs_)
        {
            Color temp1 = p.GetCircle1().GetComponent<SpriteRenderer>().color;
            if (temp1.a <= 0.0f)
            {
                GameObject.Destroy(gameObject);
            }
            temp1.a -= fade_rate;
            p.GetCircle1().GetComponent<SpriteRenderer>().color = temp1;
            Color temp2 = p.GetCircle2().GetComponent<SpriteRenderer>().color;
            temp2.a -= fade_rate;
            p.GetCircle2().GetComponent<SpriteRenderer>().color = temp2;
        }
    }

    /*_________________________________________________*/
    // CONAL LASER FUNCTIONS
    /*_________________________________________________*/
    public void InitConal(float coneangle, float coneradius, Vector3 direction)
    {
    }
}
