using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
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
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<EnemyHome>();
        this.scatter = GetComponent<EnemyScatter>();
        this.chase = GetComponent<EnemyChase>();
        this.increasedSpeed = GetComponent<EnemyIncreasedSpeed>();
        this.frightened = GetComponent<EnemyFrightened>();
    }
    private void Start()
    {
        ResetState(); //reset state is called every time the game opens
    }
    public void ResetState()
    {
        this.gameObject.SetActive(true); //enemy game objects set to true
        this.movement.ResetState(); //enemy movement resets

        this.frightened.Disable(); //enemies never start frightened
        this.chase.Disable(); //enemies never start chasing
        this.scatter.Enable(); //enemies will start scattering
        
        if(this.home != this.initialBehaviour) //if home is not enemy initial behaviour...
        {
            this.home.Disable(); //then it is disabled
        }

        if(this.initialBehaviour != null) //if initial behaviour is not null
        {
            this.initialBehaviour.Enable(); //it is enabled
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman")) //if enemy collides with pacman
        {
            if (this.frightened.enabled) //and is frightened
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
