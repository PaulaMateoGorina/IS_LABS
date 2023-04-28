using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    // This attribute makes it so Unity won't show the variable it's assigned to in the editor 
    // Used for public variables completely managed by scripts
    [HideInInspector]
    public int sheepSaved; 

    public int sheepBetweenPhases;
    public int maxPhases;

    public int sheepDroppedToFail;
    public int maxHayAmmo;
    [HideInInspector]
    public int hayAmmo;
    public float timeToReload;
    
    public SheepSpawner sheepSpawner;

    private int phase;
    private float spawnTimeDecrease;

    private bool gamePaused;
    void Awake()
    {
        Instance = this;  
        hayAmmo = maxHayAmmo; 
        phase = 1;
        gamePaused = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {   
            SoundManager.Instance.PlayRealoadClip();
            Invoke("Reloaded", timeToReload);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            ProcessPause();
        }
    }
    

    public void SavedSheep()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();

        if(sheepSaved == sheepBetweenPhases * phase)
        {  
            if(phase == 1)
                spawnTimeDecrease = sheepSpawner.timeBetweenSpawns / maxPhases;
            sheepSpawner.timeBetweenSpawns = Mathf.Max(1.0f, sheepSpawner.timeBetweenSpawns - spawnTimeDecrease);
        }

    }

    public void DroppedSheep()
    {
        sheepDroppedToFail--;
        UIManager.Instance.UpdateSheepDropped();

        if (sheepDroppedToFail == 0)
        {
            GameOver();
            UIManager.Instance.ShowGameOverWindow();
        }
    }

    public void HayShot()
    {
        hayAmmo--;
        UIManager.Instance.UpdateHayAmmo();
    }

    private void Reloaded()
    {
        hayAmmo = maxHayAmmo;
        UIManager.Instance.UpdateHayAmmo();
    }

    private void ProcessPause()
    {
        if(gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
            UIManager.Instance.HideGamePausedWindow();
        }
        else
        {
            Time.timeScale = 0;
            gamePaused = true;
            UIManager.Instance.ShowGamePausedWindow();
        }
    }

    private void GameOver()
    {
        sheepSpawner.canSpawn = false;
        sheepSpawner.DestroyAllSheep();
    }
}
