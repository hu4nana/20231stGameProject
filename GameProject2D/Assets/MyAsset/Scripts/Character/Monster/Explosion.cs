using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Monster
{
    public AnimationClip aniClip; // 재생할 애니메이션 클립
    private Animator ani; // 애니메이터 컴포넌트 참조

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
    // Animation 이벤트로 호출될 함수
    
}
