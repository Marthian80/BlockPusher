using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MainGameMenuUI : MonoBehaviour
{
    public ColorPicker ColorPicker;    
    public Button StartGameButton;

    private Color[] gameColors;    
    private Color playerColor;
    
    public void TeamSelected(Color color)
    {
        playerColor = color;        
        
        GameDataManager.Instance.SetColors(playerColor);
        StartGameButton.gameObject.SetActive(true);        
    }
        
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += TeamSelected;

        if (GameDataManager.Instance != null)
        {
            gameColors = GameDataManager.Instance.gameColors;
            ColorPicker.SelectColor(GameDataManager.Instance.PlayerColor);
        }
    }
}