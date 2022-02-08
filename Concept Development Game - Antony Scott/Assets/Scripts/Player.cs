using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    public Movement movement; //movement var declared

    private void Awake()
    {
        this.movement = GetComponent<Movement>(); //movement is assigned a movement component
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) //if player presses W or up arrow
        {
            this.movement.SetDirection(Vector2.up); //player moves up
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) //if player presses S or down arrow
        {
            this.movement.SetDirection(Vector2.down); //player moves down
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) //if player presses A or left arrow
        {
            this.movement.SetDirection(Vector2.left); //player moves left
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) //if player presses D or down arrow
        {
            this.movement.SetDirection(Vector2.right); //player moves right
        }

        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        this.movement.ResetState(); //movement resets
        this.gameObject.SetActive(true); //player gameobject is turned back on

    }
}
