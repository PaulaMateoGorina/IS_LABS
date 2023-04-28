using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public Text sheepSavedText;
    public Text sheepDroppedText;
    public Text hayAmmoText;

    public GameObject gameOverWindow;
    public GameObject gamePausedWindow;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;   
    }

    void Start()
    {
        sheepDroppedText.text = GameStateManager.Instance.sheepDroppedToFail.ToString();
        hayAmmoText.text = GameStateManager.Instance.hayAmmo.ToString();
    }

    public void UpdateSheepSaved()
    {
        sheepSavedText.text = GameStateManager.Instance.sheepSaved.ToString();
    }

    public void UpdateSheepDropped()
    {
        sheepDroppedText.text = GameStateManager.Instance.sheepDroppedToFail.ToString();
    }

    public void UpdateHayAmmo()
    {
        hayAmmoText.text = GameStateManager.Instance.hayAmmo.ToString();
    }

    public void ShowGameOverWindow()
    {
        gameOverWindow.SetActive(true);
    }

    public void ShowGamePausedWindow()
    {
        gamePausedWindow.SetActive(true);
    }

     public void HideGamePausedWindow()
    {
        gamePausedWindow.SetActive(false);
    }

}
