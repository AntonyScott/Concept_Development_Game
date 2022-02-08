using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //requires rigidbody component to be attached to gameobject
public class Movement : MonoBehaviour
{
    //variables declared
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public Rigidbody2D rb;
    public Vector2 direction;
    public Vector2 nextDirection;
    public Vector3 startingPosition;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>(); //rb is assigned a rigidbody component
        this.startingPosition = this.transform.position; //starting position is current position of gameobject
    }

    private void Start()
    {
        ResetState(); //reset state called on start
    }

    public void ResetState()
    {
        //reset to their default values
        this.speedMultiplier = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rb.isKinematic = false;
        this.enabled = true;
    }

    private void Update()
    {
        if (nextDirection != Vector2.zero) //if next direction is not (0,0)
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position; //set to rigidbody position
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        rb.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        // If no collider is hit then there is no obstacle in that direction
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }

    public void IncreaseSpeed() //increases speedMultiplier
    {
        speedMultiplier = 1.5f;
    }

}
