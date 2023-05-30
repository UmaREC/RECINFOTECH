using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }
    public string sceneName;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
       /* if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }*/
       if(HasRemainingPellets() && (lives<=0))
        {
            Invoke(nameof(NewGame),2.0f);                // NEW GAME
           // Invoke("NewGame", 2f);
           
        }
    }
    
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }


    private void NewRound()                                     
    {
       foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
    Debug.Log("Reset state");
        ResetGhostMultiplier();

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }
        this.pacman.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
    }
    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);

        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);
        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
           
            Debug.Log("hi");
            Debug.Log("sceneName to load: " +sceneName);
            // Invoke(nameof(NewRound), 3.0f);
            SceneManager.LoadScene(sceneName);  
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        
        //To Do : Changing Ghost State
        for (int i = 0; i < this.ghosts.Length; i++)
        {
           
            //this.pacman.gameObject.SetActive(true);
            this.ghosts[i].frightened.Enable(pellet.duration);
        }
        
        
        PelletEaten(pellet);
        
        CancelInvoke();
        
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
        
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
Debug.Log("This is game manager script");
}



