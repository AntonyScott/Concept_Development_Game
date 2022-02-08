using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrightened : EnemyBehaviour
{
    public SpriteRenderer body;

    public SpriteRenderer eyes;

    public SpriteRenderer blue;

    public SpriteRenderer white;

    public bool malfunction { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = true;

        Invoke(nameof(Flash), duration / 2.0f);
    }

    public override void Disable()
    {
        base.Disable();

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void Flash()
    {
        if (!this.malfunction)
        {
            this.blue.enabled = false;
            this.white.enabled = true;
            this.white.GetComponent<SpriteAnimation>().Restart();
        }
    }

    private void Malfunctioned()
    {
        this.malfunction = true;

        Vector3 position = this.enemy.home.insideTransform.position;
        position.z = this.enemy.transform.position.z;
        this.enemy.transform.position = position;

        this.enemy.home.Enable(this.duration);

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void OnEnable()
    {
        this.enemy.movement.speedMultiplier = 0.5f; //change to faster later
        this.malfunction = false;
    }

    private void OnDisable()
    {
        this.enemy.movement.speedMultiplier = 1.0f;
        this.malfunction = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled)
            {
                Malfunctioned();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            // Find the available direction that moves farthest from pacman
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                // If the distance in this direction is greater than the current
                // max distance then this direction becomes the new farthest
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (this.enemy.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            this.enemy.movement.SetDirection(direction);
        }
    }

}
