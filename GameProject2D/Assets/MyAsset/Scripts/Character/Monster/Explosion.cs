using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Monster
{
    public AnimationClip aniClip; // ����� �ִϸ��̼� Ŭ��
    private Animator ani; // �ִϸ����� ������Ʈ ����

    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Destroy(this.gameObject);
        }
    }
    // Animation �̺�Ʈ�� ȣ��� �Լ�
    
}
