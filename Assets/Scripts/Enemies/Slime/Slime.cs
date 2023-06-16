using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyPlatformPatrolAI
{
    private float localRotationZ;
    private float rotationZ;

    protected override void Start()
    {
        base.Start();
        rotationZ = transform.rotation.eulerAngles.z;
    }
    protected override void Update()
    {
        Run();
        //switch (transform.localRotation.z)
        //{
        //    case 0f: rigidbody2D.velocity = new Vector2(speed, 0f); break;
        //    case -90f: rigidbody2D.velocity = new Vector2(0f, -speed); break;
        //    case -180f: rigidbody2D.velocity = new Vector2(-speed, 0f); break;
        //    case -270f: rigidbody2D.velocity = new Vector2(0, speed); break;
        //}
    }
    protected override void Turn()
    {
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90f);
        transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90f);
        localRotationZ = transform.localRotation.eulerAngles.z;
        rotationZ = transform.rotation.eulerAngles.z;
        Debug.Log("qwerty");
    }

    protected override void Run()
    {
        animator.SetTrigger(Names.Run);

        if (localRotationZ == 0f) rigidbody2D.velocity = new Vector2(speed, 0f);
        if (localRotationZ == 270f)
        {
        rigidbody2D.velocity = new Vector2(0f, -speed);
        
        
        }
        if (localRotationZ == 180f) rigidbody2D.velocity = new Vector2(-speed, 0f);
        if (localRotationZ == 90f) rigidbody2D.velocity = new Vector2(0, speed);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Names.Wall) || (collision.gameObject.layer == LayerMask.NameToLayer(Names.Ground)))
        { 
            Turn();
            
        }
    }
}

