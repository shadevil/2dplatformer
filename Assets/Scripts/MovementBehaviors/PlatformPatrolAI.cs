using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatformPatrolAI : MonoBehaviour
{
	[SerializeField] protected float speed = 1f;

	protected Rigidbody2D rigidbody2D;

	protected Animator animator;
	protected virtual void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>(); 
		animator = GetComponent<Animator>();
	}

	protected virtual void Update()
	{
		if (IsFacingRight())
		{
			animator.SetTrigger(Names.Run);
			rigidbody2D.velocity = new Vector2(speed, 0f);
		}
		else
		{
            animator.SetTrigger(Names.Run);
            rigidbody2D.velocity = new Vector2(-speed, 0f);
        }
	}

	protected virtual bool IsFacingRight()
	{
		return transform.localScale.x > Mathf.Epsilon;
	}

	protected void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer(Names.Wall) || (collision.gameObject.layer == LayerMask.NameToLayer(Names.Ground)))
		transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)), transform.localScale.y);
	}


}