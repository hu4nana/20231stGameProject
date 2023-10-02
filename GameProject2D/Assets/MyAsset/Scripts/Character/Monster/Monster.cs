using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : Character
{
    // ���� 1~patternCount
    [SerializeField]
    protected int maxPattern = 3;
    protected int curPattern;
    //�ּ� 1 �ִ� 5 ��
    protected float patternTimer;
    protected int patternTime;
    // Start is called before the first frame update

    protected GameObject target=null;

    // ������ �����°�?
    protected bool isEnd = false;
    // ����� ���� ���ΰ�?
    protected bool isTrace = false;
    protected float traceTimer = 0;
    protected int traceTime = 0;

    //Pattern = 0 is Stop / 1 is Left / 2 is Right
    protected void RandomPattern()
    {
        if (!isTrace)
        {
            if (isEnd)
            {
                curPattern = Random.Range(0, maxPattern);
                patternTime = Random.Range(1, 6);
                isEnd = false;
                //Debug.Log(curPattern);


            }
            else
            {
                patternTimer += Time.deltaTime;
                if (patternTimer >= patternTime)
                {
                    patternTimer = 0;
                    isEnd = true;
                }
            }
        }
        
    }
    protected void TraceCheck()
    {
        if (isTrace)
        {
            isTrace = true;
            traceTimer += Time.deltaTime;
            if(traceTimer >= traceTime)
            {
                target = null;
                isTrace=false;
            }
        }
    }
    // �ܼ� �¿� �̵�
    
    public void SetRigidVelocity(int dir,float knock)
    {
        //    rigid.AddForce(new Vector2
        //(dir, 0) * knock/2, ForceMode2D.Impulse);

        rigid.velocity = new Vector2(dir* knock,rigid.velocity.y);
        this.dir = dir;
        Direction();
        target= GameObject.Find("Player");
        traceTimer = 0;
        //isTrace = true;
        isDamaged = true;

    }
}
