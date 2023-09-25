using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Die();
        WallCheck();
        FloorCheck();
        TraceCheck();
        RandomPattern();
        MonsterMover_1();
        //Debug.Log(target);
    }
    protected void MonsterMover_1()
    {
        if (rigid.velocity.x != 0)
        {
            ani.SetBool("isMove", true);
        }
        else
        {
            ani.SetBool("isMove", false);
        }
        if (isTrace && target != null)
        {

            if (target.transform.position.x - transform.position.x > 0)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
            Direction();
            if (isFloor)
            {
                rigid.velocity = new Vector2(-dir * stats.moveSpeed, rigid.velocity.y);
            }
            else
            {
                rigid.velocity = Vector2.zero;
            }

        }
        else
        {
            if (!isDamaged)
            {
                switch (curPattern)
                {
                    case 0:
                        dir = 0;
                        break;
                    case 1:
                        dir = -1;
                        Direction();
                        break;
                    case 2:
                        dir = 1;
                        Direction();
                        break;
                }

                if ((isWall || !isFloor))
                {
                    rigid.velocity = Vector2.zero;
                }
                else
                {
                    rigid.velocity = new Vector2(-dir * stats.moveSpeed, rigid.velocity.y);
                }

            }
            else if (rigid.velocity.x == 0)
            {
                isDamaged = false;
            }

        }
    }
}
