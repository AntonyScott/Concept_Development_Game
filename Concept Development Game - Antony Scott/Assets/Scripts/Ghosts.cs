using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public Movement movement { get; private set; }
    public EnemyHome home { get; private set; }
    public EnemyScatter scatter { get; private set; }
    public EnemyChase chase { get; private set; }
    public EnemyFrightened frightened { get; private set; }

    public EnemyBehaviour initialBehaviour;
    public Transform target;

    public int points = 200;

    private void Awake()
    {
        //references for all variables
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<EnemyHome>();
        this.scatter = GetComponent<EnemyScatter>();
        this.chase = GetComponent<EnemyChase>();
        this.frightened = GetComponent<EnemyFrightened>();
    }
    private void Start()
    {
        ResetState();
    }
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable(); //never start frightened
        this.chase.Disable(); //never start chasing
        this.scatter.Enable(); //will start scattering
        
        if(this.home != this.initialBehaviour)
        {
            this.home.Disable();
        }

        if(this.initialBehaviour != null)
        {
            this.initialBehaviour.Enable();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().DestroyedGhost(this);
            }
            else
            {
                FindObjectOfType<GameManager>().DestroyedPacMan();
            }
        }
    }
}
