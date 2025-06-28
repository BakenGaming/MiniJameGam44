using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject enterHouseMenu;
    [SerializeField] private bool isMainMenu;

    [Header("INVENTORY")]
    [SerializeField] private GameObject inventoryPanel;

    private void OnEnable()
    {
        if (isMainMenu) Initialize();
    }
    private void OnDisable()
    {

    }
    public void Initialize()
    {
        GetComponent<VolumeSettings>().Initialize();
        if (!isMainMenu) pauseMenu.SetActive(false);
        else creditsScreen.SetActive(false);

        settingsMenu.SetActive(false);
        GetComponent<UIInventoryController>().Initialize();
    }

    private void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        GameManager.i.PauseGame();
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        GameManager.i.UnPauseGame();
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        GetComponent<VolumeSettings>().SettingsMenuOpened();
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    public void OpenCreditsScreen()
    {
        creditsScreen.SetActive(true);
    }

    public void CloseCreditsScreen()
    {
        creditsScreen.SetActive(false);
    }

    public void StartGame()
    {
        SceneController.StartGame();
    }

    public void RestartGame()
    {
        SceneController.StartGame();
    }
    public void BackToMainMenu()
    {
        SceneController.LoadMainMenu();
    }

    public void ExitGame()
    {
        SceneController.ExitGame();
    }

    private void OpenEnterHouseMenu()
    { 
        enterHouseMenu.SetActive(true);
    }

    private void CloseEnterHouseMenu()
    {
        enterHouseMenu.SetActive(false);
    }

}
