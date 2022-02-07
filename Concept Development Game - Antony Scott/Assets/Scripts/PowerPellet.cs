using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellets
{
    public float duration = 8f;

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }
}
