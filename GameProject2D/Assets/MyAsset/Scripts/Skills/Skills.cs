using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Skills : MonoBehaviour
{
    enum Type { Melee,Range}
    Type type;
    //readonly float damage;
    //float rate;
    public BoxCollider meleeArea;
    public GameObject SkillList;

    //private float firstDeal;
    //private float atkDeal;
    //private float lastDeal;
    [Space(10f)]
    [Header("Attack")]
    [SerializeField]
    Vector2 attackRange;
    [SerializeField]
    GameObject attackRange1;
    [SerializeField]
    Transform attackPoint;
    [SerializeField]
    LayerMask enemyLayers;


    Collider2D[] hitenemies;
    Player player;
    int damage;
    int knock;
    int dir;

    bool isCoolTime=false;
    float coolTime=0.5f;
    float coolTimer;
    private void Update()
    {
        if (isCoolTime == true)
        {
            coolTimer += Time.deltaTime;
            //Debug.Log(coolTimer);
            if (coolTimer >= coolTime)
            {
                isCoolTime = false;
                coolTimer = 0;
            }
        }
    }
    public bool GetIsCoolTime()
    {
        return isCoolTime;
    }
    public void Activate()
    {
        //attackRange1.transform.localScale
        hitenemies =
           Physics2D.OverlapBoxAll(attackRange1.transform.position
           , attackRange1.transform.localScale, enemyLayers);
        player = GetComponent<Player>();
        damage = player.GetDamage();
        knock = GetComponent<Player>().GetKnock();
        dir = GetComponent<Player>().GetDir();

        if (type == Type.Melee)
        {
            //근접공격 1
            if (isCoolTime == false)
            {
                NormalAttack_1();
                isCoolTime = true;
            }
            
        }
    }

    protected void NormalAttack_1()
    {
        //적에게 데미지 적용
        foreach (Collider2D enemy in hitenemies)
        {
                MeleeAttack(enemy);
        }
    }

    protected void MeleeAttack(Collider2D enemy)
    {
        
        if (enemy.gameObject.layer == 7)
        {
            float enemyCurHp = enemy.GetComponent<Monster>().GetCurHealth();
            enemy.GetComponent<Monster>().SetCurHealth(enemyCurHp - damage);
            enemy.GetComponent<Monster>().SetRigidVelocity(-dir, knock);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackRange1.transform.position
            , attackRange1.transform.localScale);
    }
}
