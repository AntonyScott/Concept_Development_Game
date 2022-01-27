using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] ghosts; //ghosts are set as an array

    public GameObject[] androids;

    public GameObject pacman; //pacman declared as public game object

    public Transform pellets; //pellets declared as public transform

    public int score { get; private set; } 

    public int lives { get; private set; }

    private void Start()
    {
        NewGame(); //new game is called when game is started 
    }
    private void Update()
    {
        if (this.lives <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        //when new game is called...
        SetScore(0); //score is set to 0
        SetLives(3); //lives are set to 3
        NewRound(); //new round is called which turns on the pellets and resets level state
    }
    private void NewRound()
    {
        foreach(Transform pellet in this.pellets) //turns on all pellets in game
        {
            pellet.gameObject.SetActive(true); //pellets are set to true
        }
        ResetState(); //reset state is called which turns on all ghosts and pac man in the level
    }

    private void ResetState()
    {
        for (int i = 0; i < this.ghosts.Length; i++) //counts how many ghosts are present in level
        {
            this.ghosts[i].gameObject.SetActive(true); //turns on ghosts gameobject
        }
        this.pacman.gameObject.SetActive(true); //turns on pacman gameobject
    }
    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false); //deactivates all ghosts gameobjects present in level
        }
        this.pacman.gameObject.SetActive(false); //turns off pacman gameobject in level
    }
    private void SetScore(int score) //score is set as an int
    {
        this.score = score;
    }

    private void SetLives(int lives) //lives are set an as int
    {
        this.lives = lives;
    }

    public void DestroyedGhost(Ghosts ghost) 
    {
        SetScore(this.score + ghost.points); //when ghosts are destroyed, points are added to overall score
    }

    public void DestroyedPacMan()
    {
        this.pacman.gameObject.SetActive(false); //turns off pacman gameobject

        SetLives(this.lives = -1); //a life is deducted 

        if(this.lives > 0) //if lives are greater than 0...
        {
            Invoke(nameof(ResetState), 3); //invoke resetstate function after 3 seconds
        }
        else
        {
            GameOver(); //otherwise the game ends and gameover is called
        }
    }
}
