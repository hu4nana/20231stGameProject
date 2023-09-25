using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Boss : Monster
{
    public float blinkDis;
    public float range;
    public Transform player;
    public GameObject meteor;
    public float nockTimer = 0.5f;
    float nockTime = 0;
    SpriteRenderer spr;
    // Update is called once per frame
    float currentAlpha = 1f;
    float fadeSpeed=0.5f;
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (curHealth>0)
        {
            if (player.GetComponent<Player>().GetDead() != true)
            {
                RandomPattern();
                PlayerTrace();
                if (isEnd)
                {
                    BossMeteor();
                }
            }
            else
            {
                rigid.velocity = Vector2.zero;
            }
            
        }
        else
        {
            rigid.velocity = Vector2.zero;


            if (currentAlpha > 0f)
            {
                currentAlpha -= fadeSpeed * Time.deltaTime;
                currentAlpha = Mathf.Clamp01(currentAlpha);

                Color currentColor = spr.color;
                currentColor.a = currentAlpha;
                spr.color = currentColor;
            }
            else
            {
                // 알파 값이 0 이하가 되면 오브젝트를 삭제합니다.
                Destroy(gameObject);
            }
        }        
    }
   
    protected void PlayerTrace()
    {
        if (player != null)
        {
            // 타겟의 방향으로 힘을 가하여 이동
            Vector2 direction = player.gameObject.transform.position - transform.position;
            direction.Normalize();
            if (!isDamaged)
            {
                rigid.velocity = direction * stats.moveSpeed;
            }
            else
            {
                sprRend.color = new Color(1, 1, 1, 0.4f);
                nockTime += Time.deltaTime;
                if (nockTime >= nockTimer)
                {
                    sprRend.color = new Color(1, 1, 1, 1);
                    rigid.velocity = Vector2.zero;
                    isDamaged = false;
                    nockTime = 0;
                }
            }
            
        }
    }
    protected void BossMeteor()
    {
        Vector3 pos = new Vector3(player.transform.position.x, 7, 1);
        
        meteor.GetComponent<Meteor>().SetType(1);
        Instantiate(meteor, pos, Quaternion.identity);

        
    }
}
