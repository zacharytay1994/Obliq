using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineToPos
{
    public LineRenderer lr_;
    public Vector2 to_pos_;
    public bool reached_;
    public Vector2 start_store_;
    public bool destroyed_ = false;

    public LineToPos() {}

    public void Update(float speed)
    {
        if (!reached_)
        {
            // distance to pos
            Vector2 direction = (Vector2)lr_.GetPosition(1) - (Vector2)lr_.GetPosition(0);
            if (Vector2.Dot((to_pos_ - (Vector2)lr_.GetPosition(1)).normalized, ((Vector2)lr_.GetPosition(1) - (Vector2)lr_.GetPosition(0)).normalized) > 0.0f)
            {
                lr_.SetPosition(1, lr_.GetPosition(1) + (Vector3)direction * speed);
            }
            else
            {
                lr_.SetPosition(1, to_pos_);
                start_store_ = lr_.GetPosition(0);
                reached_ = true;
            }
        }
    }

    public void DestroyLine(float speed)
    {
        if (!destroyed_)
        {
            // distance to pos
            Vector2 direction = (Vector2)lr_.GetPosition(1) - (Vector2)lr_.GetPosition(0);
            if (Vector2.Dot((to_pos_ - start_store_).normalized, ((Vector2)lr_.GetPosition(1) - (Vector2)lr_.GetPosition(0)).normalized) > 0.0f)
            {
                lr_.SetPosition(0, lr_.GetPosition(0) + (Vector3)direction * speed);
            }
            if (direction.magnitude < 0.1f)
            {
                destroyed_ = true;
                lr_.enabled = false;
            }
        }
    }
}

public class BossBody : MonoBehaviour
{
    GameObject player_;
    [SerializeField]
    GameObject boss_head_;
    [SerializeField]
    GameObject boss_lines_;
    BossHead bh1_;
    BossHead bh2_;
    BossHead bh3_;

    float rotation_angle = 0.0f;
    [SerializeField]
    float spin_rate_ = 10.0f;
    [SerializeField]
    float distance_from_body_ = 2.0f;

    [SerializeField]
    float detect_player_range_ = 5.0f;
    bool activated_ = false;
    bool setuped_ = false;
    Vector2 immob_player_pos_ = Vector2.zero;

    List<LineToPos> line_list_ = new List<LineToPos>();
    [SerializeField]
    float line_draw_speed_ = 5.0f;

    [SerializeField]
    float outer_size_ = 10.0f;
    float inner_size_ = 8.0f;

    LineToPos nw;
    LineToPos ne;
    LineToPos se;
    LineToPos sw;
    LineToPos player_line_;

    float left_;
    float right_;
    float top_;
    float bottom_;

    bool ready_to_attack_ = false;

    float phase_ = 0;

    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.Find("Player");
        CreateAllHeads();
    }

    // Update is called once per frame
    void Update()
    {
        SpinHeads();

        DetectPlayerWithinRange();

        if (activated_)
        {
            SetupBoss();
        }

        UpdateLineList();

        if (ready_to_attack_)
        {
            LockPlayerIn();
        }
    }

    void CreateAllHeads()
    {
        if (boss_head_ == null)
        {
            Debug.Log("Boss Head prefab not attached");
            return;
        }
        // spawn and assign three heads
        bh1_ = GameObject.Instantiate(boss_head_).GetComponent<BossHead>();
        bh1_.Init(this);
        bh2_ = GameObject.Instantiate(boss_head_).GetComponent<BossHead>();
        bh2_.Init(this);
        bh3_ = GameObject.Instantiate(boss_head_).GetComponent<BossHead>();
        bh3_.Init(this);
    }

    void SpinHeads()
    {
        rotation_angle = rotation_angle < 6.283f ? rotation_angle + spin_rate_ * Time.deltaTime : 0.0f;
        Vector2 initial_vec = new Vector2(0.0f, 1.0f);
        if (bh1_.Attached())
        {
            // position with offset
            Vector2 offset1 = GF.RotateVector(initial_vec, rotation_angle);
            bh1_.gameObject.transform.position = (Vector2)gameObject.transform.position + (Vector2)offset1.normalized * distance_from_body_;
            // calculate angle
            bh1_.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_angle * Mathf.Rad2Deg);
        }
        if (bh2_.Attached())
        {
            // position with offset
            Vector2 offset2 = GF.RotateVector(initial_vec, rotation_angle + 2.094f);
            bh2_.gameObject.transform.position = (Vector2)gameObject.transform.position + (Vector2)offset2.normalized * distance_from_body_;
            bh2_.transform.rotation = Quaternion.Euler(0.0f, 0.0f, (rotation_angle + 2.094f) * Mathf.Rad2Deg);
        }
        if (bh3_.Attached())
        {
            // position with offset
            Vector2 offset3 = GF.RotateVector(initial_vec, rotation_angle + 4.189f);
            bh3_.gameObject.transform.position = (Vector2)gameObject.transform.position + (Vector2)offset3.normalized * distance_from_body_;
            bh3_.transform.rotation = Quaternion.Euler(0.0f, 0.0f, (rotation_angle + 4.189f) * Mathf.Rad2Deg);
        }
    }

    void DetectPlayerWithinRange()
    {
        if (player_ == null)
        {
            Debug.Log("No Player detected");
            return;
        }
        // if player within range activate
        if (((Vector2)player_.transform.position - (Vector2)gameObject.transform.position).magnitude < detect_player_range_ && !activated_)
        {
            immob_player_pos_ = player_.transform.position;
            // draw line to player
            player_line_ = DrawBossLine(gameObject.transform.position, player_.transform.position);
            // draw line to four corners
            // nw
            Vector2 this_pos = (Vector2)gameObject.transform.position;
            Vector2 nw_corner = new Vector2(this_pos.x - outer_size_, this_pos.y + outer_size_);
            nw = DrawBossLine(gameObject.transform.position, nw_corner);
            // ne
            Vector2 ne_corner = new Vector2(this_pos.x + outer_size_, this_pos.y + outer_size_);
            ne = DrawBossLine(gameObject.transform.position, ne_corner);
            // se
            Vector2 se_corner = new Vector2(this_pos.x + outer_size_, this_pos.y - outer_size_);
            se = DrawBossLine(gameObject.transform.position, se_corner);
            // sw
            Vector2 sw_corner = new Vector2(this_pos.x - outer_size_, this_pos.y - outer_size_);
            sw = DrawBossLine(gameObject.transform.position, sw_corner);

            // borders
            float left_border = gameObject.transform.position.x - inner_size_;
            float right_border = gameObject.transform.position.x + inner_size_;
            float top_border = gameObject.transform.position.y + inner_size_;
            float bottom_border = gameObject.transform.position.y - inner_size_;
            left_ = left_border;
            right_ = right_border;
            top_ = top_border;
            bottom_ = bottom_border;

            activated_ = true;
        }
    }

    void SetupBoss()
    {
        if (!setuped_)
        {
            // immobilize player
            player_.transform.position = immob_player_pos_;
            if (ne.reached_)
            {
                Vector2 this_pos = (Vector2)gameObject.transform.position;
                // draw outerparameter
                Vector2 nw_corner_o = new Vector2(this_pos.x - outer_size_, this_pos.y + outer_size_);
                Vector2 ne_corner_o = new Vector2(this_pos.x + outer_size_, this_pos.y + outer_size_);
                Vector2 se_corner_o = new Vector2(this_pos.x + outer_size_, this_pos.y - outer_size_);
                Vector2 sw_corner_o = new Vector2(this_pos.x - outer_size_, this_pos.y - outer_size_);

                DrawBossLine(nw_corner_o, ne_corner_o);
                DrawBossLine(ne_corner_o, se_corner_o);
                DrawBossLine(se_corner_o, sw_corner_o);
                DrawBossLine(sw_corner_o, nw_corner_o);

                // draw innerparameter
                Vector2 nw_corner_i = new Vector2(this_pos.x - inner_size_, this_pos.y + inner_size_);
                Vector2 ne_corner_i = new Vector2(this_pos.x + inner_size_, this_pos.y + inner_size_);
                Vector2 se_corner_i = new Vector2(this_pos.x + inner_size_, this_pos.y - inner_size_);
                Vector2 sw_corner_i = new Vector2(this_pos.x - inner_size_, this_pos.y - inner_size_);

                DrawBossLine(nw_corner_i, sw_corner_i);
                DrawBossLine(sw_corner_i, se_corner_i);
                DrawBossLine(se_corner_i, ne_corner_i);
                DrawBossLine(ne_corner_i, nw_corner_i);

                setuped_ = true;
            }
        }
        else
        {
            nw.DestroyLine(line_draw_speed_ / 2.0f * Time.deltaTime);
            ne.DestroyLine(line_draw_speed_ / 2.0f * Time.deltaTime);
            sw.DestroyLine(line_draw_speed_ / 2.0f * Time.deltaTime);
            se.DestroyLine(line_draw_speed_ / 2.0f * Time.deltaTime);
            player_line_.DestroyLine(line_draw_speed_ / 2.0f * Time.deltaTime);
            if (!ready_to_attack_)
            {
                ready_to_attack_ = true;
                SetPhase(1);
            }
        }
    }
    
    LineToPos DrawBossLine(Vector2 from, Vector2 to)
    {
        GameObject temp = GameObject.Instantiate(boss_lines_, transform);
        LineRenderer lr = temp.GetComponent<LineRenderer>();
        lr.SetPosition(0, from);
        Vector2 direction = (to - from).normalized;
        lr.SetPosition(1, from + direction * 0.5f);
        LineToPos ltp = new LineToPos();
        ltp.lr_ = lr;
        ltp.to_pos_ = to;
        ltp.reached_ = false;
        line_list_.Add(ltp);
        return ltp;
    }

    void UpdateLineList()
    {
        foreach(LineToPos ltp in line_list_)
        {
            if (!ltp.destroyed_)
            {
                ltp.Update(line_draw_speed_ * Time.deltaTime);
            }
        }
    }

    void LockPlayerIn()
    {
        Vector3 player_position_ = player_.transform.position;
        if (player_.transform.position.x < left_)
        {
            player_.transform.position = new Vector3(left_, player_position_.y, player_position_.z);
        }
        if (player_.transform.position.x > right_)
        {
            player_.transform.position = new Vector3(right_, player_position_.y, player_position_.z);
        }
        if (player_.transform.position.y > top_)
        {
            player_.transform.position = new Vector3(player_position_.x, top_, player_position_.z);
        }
        if (player_.transform.position.y < bottom_)
        {
            player_.transform.position = new Vector3(player_position_.x, bottom_, player_position_.z);
        }
    }

    public void SetPhase(float i)
    {
        switch (i)
        {
            case 1:
                if (bh1_ != null)
                {
                    bh1_.SetBounds(left_, right_, top_, bottom_);
                    bh1_.Activate();
                }
                break;
            case 2:
                if (bh2_ != null && bh3_ != null)
                {
                    bh2_.SetBounds(left_, right_, top_, bottom_);
                    bh3_.SetBounds(left_, right_, top_, bottom_);
                    bh2_.Activate();
                    bh3_.Activate();
                }
                break;
        }
    }
}
