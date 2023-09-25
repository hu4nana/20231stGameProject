using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
//using UnityEditor.U2D;
//using UnityEditor.UI;
using UnityEngine;



public class Character : MonoBehaviour
{
    [System.Serializable]
    protected struct Stats
    {
        public float myScale;

        public float maxHealth;
        public float maxMana;

        public int damage;

        public float moveSpeed;

        public float jumpPow;
        public int jumpCount;
        public float jumpPos;

        public float dashPow;
        public float dashTime;

        public int knock;
    }

    [SerializeField]
    [Header("Character Stats")]
    protected Stats stats;

    [Space(10f)]
    [Header("Current Stats")]
    [SerializeField]
    protected float curHealth;
    [SerializeField]
    protected float curMana;
    [SerializeField]
    protected int curJumpCount;
    [SerializeField]
    protected float curDashTime;

    [Space(10f)]
    [SerializeField]
    [Header("Check")]
    protected Transform floorCheck;
    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected LayerMask f_Layer;
    [SerializeField]
    protected LayerMask w_Layer;
    [SerializeField]
    protected LayerMask fw_Layer;


    // Basic Direction is Left ( Left is 1, Right is -1 )
    protected int dir = -1;

    
    protected float offTimer = 0f;
    protected bool isOff = false;
    protected float offTime = 2f;
    protected Rigidbody2D rigid;
    protected Animator ani;
    protected SpriteRenderer sprRend;
    //protected Collider2D col = null;
    protected bool isWall;
    protected bool isFloor;
    protected bool isFixed;
    protected bool isMidAir;
    protected bool isDead = false;
    protected bool isDamaged = false;

    private void Awake()
    {
        curHealth = stats.maxHealth;
        curMana = stats.maxMana;
        curJumpCount=stats.jumpCount;
        curDashTime=stats.dashTime;
        ani = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprRend=GetComponentInChildren<SpriteRenderer>();
    }
    /*============================================================================================*/
    public int GetDir()
    {
        return dir;
    }
    public void SetDir(int value)
    {
        dir = value;
    }
    /*============================================================================================*/
    public float GetMaxHealth()
    {
        return stats.maxHealth;
    }
    public void SetMaxHealth(float value)
    {
        stats.maxHealth = value;
    }
    /*============================================================================================*/
    public float GetCurHealth()
    {
        return curHealth;
    }
    public void SetCurHealth(float value)
    {
        curHealth = value;
    }
    /*============================================================================================*/
    public int GetDamage()
    {
        return stats.damage;
    }
    public void SetDamage(int value)
    {
        stats.damage = value;
    }
    /*============================================================================================*/
    // 넉백 받는 정도
    public int GetKnock()
    {
        return stats.knock;
    }
    public void SetKnock(int value)
    {
        stats.knock = value;
    }
    /*============================================================================================*/
    protected void WallCheck()
    {
        isWall =
            Physics2D.Raycast(wallCheck.position,Vector2.left * transform.localScale,0.15f, w_Layer);

        isFixed
             = Physics2D.Raycast(wallCheck.position, Vector2.left * transform.localScale, 0.15f, fw_Layer);
    }

    protected void FloorCheck()
    {
        isFloor =
            (Physics2D.Raycast(floorCheck.position, Vector2.down,
            0.15f, f_Layer));
        //if (isFloor)
        //{
        //    Debug.Log("fdsfdsfdfdfds");
        //}
    }
    // SpriteDirection -1 is Left / 1 is Right
    protected void Direction()
    {
        if (dir != 0)
        {
            transform.localScale =
                 new Vector3(stats.myScale * dir, stats.myScale, stats.myScale);
        }
        
    }

    protected void Die()
    {
        if (curHealth <= 0)
        {
            isDead = true;
            Debug.Log(this.gameObject.name + "isDead : " + isDead);
            Destroy(gameObject);
        }
    }

    public Collider2D GetCollision(Collider2D collision)
    {
        isOff = true;
        return collision;
    }

   
    protected void OffTimeSet()
    {
            if (isOff)
            {
                gameObject.layer = 11;
                offTimer -= Time.deltaTime;
                
                if (offTimer <= 0f)
                {
                    isOff = false;
                    sprRend.color = new Color(1, 1, 1, 1f);
                    gameObject.layer = 6;
                }
            }
    }
    
}
