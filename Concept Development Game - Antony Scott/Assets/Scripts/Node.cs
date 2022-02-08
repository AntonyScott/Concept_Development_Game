using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer; //layermask for obstacle layer

    public List<Vector2> possibleDirections; //stores a list of possible directions for the player

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, obstacleLayer);

        if (hit.collider == null)
        {
            possibleDirections.Add(direction);
        }
    }
    private void Start()
    {
        possibleDirections = new List<Vector2>();

        //checks all directions to see if it can move
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    
}
