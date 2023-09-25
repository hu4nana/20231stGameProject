using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Monster
{
    //type == 0 Vertical / type == 1 Diagonal
    public GameObject explosion;
    int type;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void SetType(int type)
    {
        this.type = type;
    }
    
    public void InstinateMeteor(int type,int dir)
    {
        this.type = type;
        this.dir = dir;
        Instantiate(this);

    }
    
    private void FixedUpdate()
    {
        MeteorMovement();
    }
    void MeteorMovement()
    {
        Vector2 direction;
        if (type == 0)
        {
            direction = Vector2.down;
            rigid.velocity = direction * stats.moveSpeed;
        }
        else if (type == 1)
        {
            direction = new Vector2(dir, -1);
            rigid.velocity.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 회전값을 Quaternion으로 변환
            Quaternion rotation = Quaternion.AngleAxis(180-angle, Vector3.forward);

            // transform의 rotation을 할당하여 회전 적용
            transform.rotation = rotation;
            rigid.velocity = direction * stats.moveSpeed;
        }
    }

    void OnDestroy()
    {
        Instantiate(explosion, this.transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CompositeCollider2D>() != null)
        {
            Debug.Log("TriggerEnter2D");
            Destroy(this.gameObject);
        }
    }
}
