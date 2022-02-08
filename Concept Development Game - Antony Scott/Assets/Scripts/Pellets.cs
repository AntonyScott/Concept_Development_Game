using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellets : MonoBehaviour
{
    public int points = 10;

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PelletEaten(this); //calls pelletEaten function from GameManager
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pacman")) //if pacman collides with pellet
        {
            Eat(); //eat is called
        }
    }

    
}
