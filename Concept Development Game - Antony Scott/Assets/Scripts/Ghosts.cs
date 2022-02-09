using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour //base script for the enemies
{
    //variables are declared
    public Movement movement;
    public EnemyHome home;
    public EnemyScatter scatter;
    public EnemyChase chase;
    public EnemyIncreasedSpeed increasedSpeed;
    public EnemyFrightened frightened;

    public EnemyBehaviour initialBehaviour;
    public Transform target;

    public int points = 200;

    private void Awake()
    {
        //component references for all variables
        movement = GetComponent<Movement>();
        home = GetComponent<EnemyHome>();
        scatter = GetComponent<EnemyScatter>();
        chase = GetComponent<EnemyChase>();
        increasedSpeed = GetComponent<EnemyIncreasedSpeed>();
        frightened = GetComponent<EnemyFrightened>();
    }
    private void Start()
    {
        ResetState(); //reset state is called every time the game opens
    }
    public void ResetState()
    {
        gameObject.SetActive(true); //enemy game objects set to true
        movement.ResetState(); //enemy movement resets

        frightened.Disable(); //enemies never start frightened
        chase.Disable(); //enemies never start chasing
        scatter.Enable(); //enemies will start scattering
        
        if(home != initialBehaviour) //if home is not enemy initial behaviour...
        {
            home.Disable(); //then it is disabled
        }

        if(initialBehaviour != null) //if initial behaviour is not null
        {
            initialBehaviour.Enable(); //it is enabled
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman")) //if enemy collides with pacman
        {
            if (frightened.enabled) //and is frightened
            {
                FindObjectOfType<GameManager>().DestroyedEnemy(this); //enemy is destroyed
            }
            else
            {
                FindObjectOfType<GameManager>().DestroyedPacMan(); //otherwise pacman is killed
            }
        }
    }
}
