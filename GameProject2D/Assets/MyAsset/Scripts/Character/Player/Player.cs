using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
//using UnityEditor.Experimental.GraphView;
//using UnityEditor.U2D;
//using UnityEditor.UI;
using UnityEngine;

public class Player : Character
{
    float hAxis;
    float vAxis;
    bool jump;
    bool dash;
    
    bool weaAtk;
    bool weaSkil;
    bool stuck;

    bool isWalk;
    //bool isJump;
    bool isDash;
    bool isWallSlide = false;
    bool isAttack;
    bool isDamaged;

    //Skills skill;

    [Space(10f)]
    [Header("PlatformCheck")]
    [SerializeField]
    float floorDis;



    //Animator ani;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (curHealth > 0)
        {
            isDead = false;
            ani.SetBool("isDead", false);
            GetInput();
            WallSlide();
            Move();
            ContinuousAnimation();
        }
        else
        {
            isDead = true;
            ani.SetLayerWeight(ani.GetLayerIndex("Idle Layer"), 0f);
            ani.SetLayerWeight(ani.GetLayerIndex("Dead Layer"), 1f);
            this.gameObject.layer = 11;
            ani.SetBool("isDead", true);
        }
        //FixedWall();
    }
    private void Update()
    {
        //MomentaryAnimation();
        FloorCheck();
        WallCheck();
        Jump();
        WallJump();
        Dash();
        Attack();
        OffTimeSet();
    }
    void GetInput()
    {
        weaAtk=Input.GetMouseButtonDown(0);
        weaSkil=Input.GetMouseButtonDown(2);
    }
    public bool GetDead()
    {
        return isDead;
    }

    //Basic Direction is Left
    //   scale > 0 == left
    //   scale < 0 == right
    void Move()
    {
        if (!isWallSlide)
        {
            hAxis = Input.GetAxisRaw("Horizontal");
        }
        
        vAxis = Input.GetAxisRaw("Vertical");


        if ((offTime- offTimer >=0.5f)&& (isFloor||!isWall)&&!stuck&&!isDash||(curDashTime<=0))
        {
            if(hAxis == 1)
            {
                dir = -1;
                isWalk=true;
            }
            else if(hAxis == -1)
            {
                dir = 1;
                isWalk = true;
            }
            else
            {
                isWalk = false;
            }
            Direction();
            rigid.velocity = new Vector2(hAxis * stats.moveSpeed, rigid.velocity.y);
            

        }
    }

    void MomentaryAnimation()
    {
        
        if (weaAtk)
        {
            ani.SetTrigger("doAtk");
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Dash-Attack NoDust")){
                isAttack = true;
            }
            else
            {
                isAttack = false;
            }
        }
        
    }

    void ContinuousAnimation()
    {
        //if (hAxis != 0)
        //{
        //    ani.SetBool("Walk", true);
        //}
        //else
        //{
        //    ani.SetBool("Walk", false);
        //}
        //AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);

        //========================================Jump

        

        //========================================Walk
        ani.SetBool("isWalk", isWalk);

        //========================================Fall
        ani.SetBool("isFloor", isFloor);

        //========================================WallSlide
        

        //========================================Jump
        if (rigid.velocity.y > 3)
        {
            ani.SetBool("isJump", true);
            ani.SetBool("isFall", false);
        }
        else if (rigid.velocity.y < -1)
        {
            ani.SetBool("isJump", false);
            ani.SetBool("isFall", true);
        }

        //========================================Fall
    }

    
    void Jump()
    {

        jump = Input.GetKeyDown(KeyCode.Space);

        if (!isDash && !(curJumpCount <= 0) && jump)
        {
            
            //if (curJumpCount >= 2)
            //{
            //    rigid.velocity = new Vector2(rigid.velocity.x, stats.jumpPow);
            //}
            //else
            //{
            //    rigid.velocity = new Vector2(rigid.velocity.x, stats.jumpPow-1);
            //}

            rigid.velocity = Vector2.zero;
            rigid.velocity = new Vector2(rigid.velocity.x, stats.jumpPow);
            //rigid.AddForce(Vector2.up * stats.jumpPow);

            curJumpCount--;
            //isJump = true;
        }
    }
    void Dash()
    {
        dash = Input.GetKeyDown(KeyCode.C);
        if (!isDash && dash)
        {
            isDash = true;
            gameObject.layer = 11;
            //startYPos = transform.position.y;
            //if(transform.localScale.x < 0)
            //{
            //    rigid.velocity = Vector2.zero;
            //    rigid.velocity = transform.right * 10;
            //    curDashCount--;
            //    Debug.Log("Right Dash Act" + curDashCount + "/" + stats.dashPow);
            //}
            //else
            //{
            //    rigid.velocity = Vector2.zero;
            //    rigid.velocity = -transform.right * 10;
            //    curDashCount--;
            //    Debug.Log("Left Dash Act" + curDashCount + "/" + stats.dashPow);
            //}
            //Debug.Log(rigid.velocity);
            rigid.gravityScale = 0;
            if(!isFloor && isWall)
            {
                if (transform.localScale.x >= 0)
                {
                    
                    dir = -1;
                }
                else
                {
                    
                    dir = 1;
                }
            }
            Direction();
            rigid.velocity = new Vector2(-transform.localScale.x / stats.myScale * stats.dashPow, 0);

            ani.SetTrigger("doDash");
        }
        if (isDash)
        {
            curDashTime -= Time.deltaTime;
            
            if(curDashTime <= 0)
            {
                rigid.gravityScale = 1;
            }
        }
    }
    void WallSlide()
    {
        if (!isFloor && isWall)
        {
            ani.SetBool("isWallSlide", true);
        }
        else
        {
            ani.SetBool("isWallSlide", false);
        }
        
    }
    void Attack()
    {
        weaAtk = Input.GetKeyDown(KeyCode.X);
        Skills skill = GetComponent<Skills>();
        if (weaAtk&&!isDash)
        {
            //skill.SkillList.layer = 10;
            
            if (skill.GetIsCoolTime() == false)
            {
                ani.SetTrigger("doAtk");
                skill.Activate();
            }
        }
    }
    public void TakeDamage(Collider2D collision)
    {
        int damage;
        int knock;
        if (collision.gameObject.layer == 7|| collision.gameObject.layer == 12)
        {
            isOff = true;
            Monster other = collision.gameObject.GetComponent<Monster>();
            damage = other.GetDamage();
            knock = other.GetKnock();

            sprRend.color = new Color(1, 1, 1, 0.4f);
            rigid.AddForce(new Vector2(dir, 1) * knock, ForceMode2D.Impulse);
            offTimer = offTime;
            this.curHealth -= damage;
            //col = collision;
        }
    }
    void WallJump()
    {
        if (!isFloor && (isWall || isFixed)&&jump&&!isDash)
        {
            //ani.SetBool("WallSlide", true);
            //Vector2 wallJumpDir = wallNormal*stats.jumpPow*3;
            //Vector2 wolljd = (Vector2.up + Vector2.left * 3) * -dir * stats.jumpPow;
            //rigid.velocity = new Vector2(0, 0);
            rigid.velocity = Vector2.zero;
            rigid.velocity = new Vector2((transform.localScale.x / stats.myScale) * stats.jumpPow * 3, stats.jumpPow);
        }

    }
    
    void ChanceReset()
    {
        curJumpCount = stats.jumpCount;
        if (curDashTime <= 0)
        {
            gameObject.layer = 6;
            curDashTime = stats.dashTime;
            isDash = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChanceReset();
        if (collision.gameObject.layer == 9)
        {
            if (dir == -hAxis)
            {
                stuck = true;
            }
        }
        //isJump = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        ChanceReset();
        if (collision.gameObject.layer == 9)
        {
            if (dir == -hAxis)
            {
                stuck = true;
                if (!isWall)
                {
                    stuck = false;
                }
            }
            else
            {
                stuck = false;
            }
        }
        //isJump = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        stuck = false;
        if (curJumpCount > 0)
            curJumpCount--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage(collision);
        
    }
}
