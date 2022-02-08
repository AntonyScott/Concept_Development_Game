using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>(); //node is declared as a node component

        if (node != null && this.enabled && !this.enemy.frightened.enabled) //if node not null, this script enabled, and frightened enabled...
        {
            Vector2 direction = Vector2.zero; //direction set to (0,0)
            float minimumDistance = float.MaxValue;

            foreach(Vector2 availableDirection in node.possibleDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.enemy.target.position - newPosition).sqrMagnitude;

                if(distance < minimumDistance)
                {
                    direction = availableDirection;
                    minimumDistance = distance;
                }
            }

            this.enemy.movement.SetDirection(direction);
        }
    }
    private void OnDisable()
    {
        this.enemy.increasedSpeed.Enable(); //when this script disables, it enables the increasedSpeed script
    }
}
