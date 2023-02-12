using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRatCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;
    private SpriteRenderer _sprite;
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (_sprite.flipX == true)
            _boxCollider.offset = new Vector2(-0.2216978f, 0.2526783f);
        else
            _boxCollider.offset = new Vector2(0.7117f, 0.2526783f);
    }
}
