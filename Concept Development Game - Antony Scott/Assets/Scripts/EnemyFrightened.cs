using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrightened : EnemyBehaviour
{
    public SpriteRenderer body;

    public SpriteRenderer eyes;

    public SpriteRenderer blue;

    public SpriteRenderer white;

    public bool malfunction;

    public override void Enable(float duration)
    {
        base.Enable(duration);

        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = true;

        Invoke(nameof(Flashing), duration / 2.0f);
    }

    public override void Disable()
    {
        base.Disable();

        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void Flashing()
    {
        if (!this.malfunction)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<SpriteAnimation>().Restart();
        }
    }

    private void Malfunctioned()
    {
        malfunction = true;

        Vector3 position = enemy.home.insideTransform.position;
        position.z = enemy.transform.position.z;
        enemy.transform.position = position;

        enemy.home.Enable(duration);

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void OnEnable()
    {
        enemy.movement.speedMultiplier = 0.5f; //change to faster later
        malfunction = false;
    }

    private void OnDisable()
    {
        enemy.movement.speedMultiplier = 1.0f;
        malfunction = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled)
            {
                Malfunctioned();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            // Finds the furthest direction from the player
            foreach (Vector2 possibleDirection in node.possibleDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(possibleDirection.x, possibleDirection.y);
                float distance = (enemy.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = possibleDirection;
                    maxDistance = distance;
                }
            }

            enemy.movement.SetDirection(direction);
        }
    }

}
