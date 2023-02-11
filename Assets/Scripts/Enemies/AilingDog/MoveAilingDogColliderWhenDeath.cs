using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAilingDogColliderWhenDeath : Enemy
{
    [SerializeField] private BoxCollider2D _boxCollider;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        _boxCollider.offset = new Vector2(-0.2216978f, 0.2526783f);
    }
}
