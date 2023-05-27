using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]

public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float animationTime = 0.25f;
    public int animationframe { get; private set; }
    public bool loop = true;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }

    private void Advance()
    {
        if (!this.spriteRenderer.enabled)
        {
            return;
        }
        this.animationframe++;
        if (this.animationframe >= this.sprites.Length && this.loop)
        {
            this.animationframe = 0;
        }
        if (this.animationframe >= 0 && this.animationframe < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.animationframe];
        }
    }

    public void Restart()
    {
        this.animationframe = -1;
        Advance();
    }
}
