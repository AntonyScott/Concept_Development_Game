using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghosts[] ghosts; //ghosts/androids are set as an array

    public Player pacman; //pacman declared as public game object

    public Transform pellets; //pellets declared as public transform

    public int score;
    public int enemyMultiplier = 1;
    public int lives;

    private void Start()
    {
        NewGame(); //new game is called when game is started 
    }
    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Space)) //if lives are equal to or greater than 0 and player presses space
        {
            NewGame(); //new game is called which resets score and level
        }
        if (Input.GetKeyDown(KeyCode.Escape)) //if escape is pressed
        {
            Debug.Log("Quit!");
            Application.Quit(); //the game quits to desktop
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
        foreach(Transform pellet in pellets) //turns on all pellets in game
        {
            pellet.gameObject.SetActive(true); //pellets are set to true
        }
        ResetState(); //reset state is called which turns on all ghosts and pac man in the level
    }

    private void ResetState()
    {
        ResetEnemyMultiplier();
        for (int i = 0; i < ghosts.Length; i++) //counts how many ghosts are present in level
        {
            ghosts[i].ResetState(); //turns on ghosts gameobject
            //this.ghosts[i].ResetState();
        }
        pacman.ResetState(); //turns on pacman gameobject
    }
    private void GameOver()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false); //deactivates all enemy gameobjects present in level
        }
        pacman.gameObject.SetActive(false); //turns off pacman gameobject in level
    }
    private void SetScore(int score) //score is set as an int
    {
        this.score = score;
    }

    private void SetLives(int lives) //lives are set an as int
    {
        this.lives = lives;
    }

    public void DestroyedEnemy(Ghosts enemy) 
    {
        int points = enemy.points * enemyMultiplier;
        SetScore(score + points);
        enemyMultiplier++;
    }

    public void DestroyedPacMan()
    {
        pacman.gameObject.SetActive(false); //turns off pacman gameobject

        SetLives(lives - 1); //a life is deducted 
        FindObjectOfType<AudioManager>().Play("PlayerDeath");

        if(lives > 0) //if lives are greater than 0...
        {
            Invoke(nameof(ResetState), 3); //invoke resetstate function after 3 seconds
        }
        else
        {
            GameOver(); //otherwise the game ends and gameover is called
        }
    }
    public void PelletEaten(Pellets pellet)
    {
        pellet.gameObject.SetActive(false); //pellets are deactivated when they collide with pacman
        SetScore(score + pellet.points); //score increased for each pellet eaten

        if (!RemainingPellets()) //if no pellets remain
        {
            pacman.gameObject.SetActive(false); //pacman is deactivated
            Invoke(nameof(NewRound), 3.0f); //new round begins after 3 seconds
        }
    }

    public void PowerPelletEaten(PowerPellet powerPellet)
    {
        // change enemy state
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].frightened.Enable(powerPellet.duration); //if power pellet is eaten then enemies become frightened
        }
        FindObjectOfType<AudioManager>().Play("PlayerPowerup");
        PelletEaten(powerPellet); //powerpellet added to score
        CancelInvoke(); //all invokes are cancelled
        Invoke(nameof(ResetEnemyMultiplier), powerPellet.duration); //invokes score multiplier for every enemy eaten during power pellet duration
        
    }

    private bool RemainingPellets()
    {
        foreach(Transform pellets in pellets)
        {
            if (pellets.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetEnemyMultiplier()
    {
        enemyMultiplier = 1; //enemy multiplier is reset to 1
    }
}
