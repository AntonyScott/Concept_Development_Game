using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))] //requires a gameobject with sprite renderer for script to be attached
public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] sprites; //array of sprites
    public SpriteRenderer spriteRenderer; //spriterenderer var

    public float animationTime = 0.25f; //anim time is quarter of a second
    public int animationFrame;
    public bool loop = true; //anims are looping

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>(); //sprite renderer is assigned sprite renderer component
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime); //advance is repeated by at animtime then by every animtime afterwards
    }

    private void Advance()
    {
        if (!this.spriteRenderer.enabled) //if sprite render not enabled
        {
            return;
        }

        this.animationFrame++; //increment anim time
        if (this.animationFrame >= this.sprites.Length && this.loop) //if anim frame is greater than sprite length and loop
        {
            this.animationFrame = 0; //anim frame resets
        }
        if(this.animationFrame >= 0 && this.animationFrame < this.sprites.Length) //if frame time equal to or greater than 0 and anim frame is less than sprite length
        {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

    public void Restart()
    {
        this.animationFrame = -1; //anim frame is -1
        Advance(); //advance is called
    }
}
