using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScatter : EnemyBehaviour
{

    private void OnDisable()
    {
        this.enemy.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.enemy.frightened.enabled) //if node is not empty, this script is enabled and frightened script is disabled
        {
            int index = Random.Range(0, node.possibleDirections.Count); //stores an index of available directions for the enemies

            if (node.possibleDirections[index] == -this.enemy.movement.direction && node.possibleDirections.Count > 1) //if direction index is opposite of enemy direction and available direction count > 1
            {
                index++; //increments index by 1, changes direction

                if(index >= node.possibleDirections.Count) //if index is greater or equal to direction count
                {
                    index = 0; //index reset to 0
                }
            }
            this.enemy.movement.SetDirection(node.possibleDirections[index]);
        }
        
    }
}
