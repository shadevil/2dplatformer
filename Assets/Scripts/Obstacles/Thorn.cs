using UnityEngine;

public class Thorn : Obstacle
{
    [SerializeField] private Sprite normalThorn;
    [SerializeField] private Sprite bloodThorn;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = normalThorn;
    }

    internal override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        spriteRenderer.sprite = bloodThorn;
    }
}
