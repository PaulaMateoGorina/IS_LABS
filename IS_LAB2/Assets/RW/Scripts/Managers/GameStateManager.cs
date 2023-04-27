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
    
    [HideInInspector]
    public int sheepDropped;

    public int sheepDroppedToFail;
    public SheepSpawner sheepSpawner;

    void Awake()
    {
        Instance = this;   
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }
    

    public void SavedSheep()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();
    }

    public void DroppedSheep()
    {
        sheepDropped++;
        UIManager.Instance.UpdateSheepDropped();

        if (sheepDropped == sheepDroppedToFail)
        {
            GameOver();
            UIManager.Instance.ShowGameOverWindow();
        }
    }

    private void GameOver()
    {
        sheepSpawner.canSpawn = false;
        sheepSpawner.DestroyAllSheep();
    }
}
