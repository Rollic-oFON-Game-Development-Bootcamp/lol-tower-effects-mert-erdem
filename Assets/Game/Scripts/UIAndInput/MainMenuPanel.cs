using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : UIPanelBase
{
    [SerializeField] private InGamePanel inGamePanel;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera mainMenuCamera;

    public void OnStartButtonPressed()
    {
        canvasGroup.blocksRaycasts = canvasGroup.interactable = false;
        canvasGroup.alpha = 0f;

        Debug.Log("Start Game Now");

        inGamePanel.EnablePanel();
        mainMenuCamera.enabled = false;
    }
}
