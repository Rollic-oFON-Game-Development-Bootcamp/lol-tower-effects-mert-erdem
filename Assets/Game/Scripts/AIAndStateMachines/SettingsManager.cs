using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager instance;
    public static SettingsManager Instance => instance ??= FindObjectOfType<SettingsManager>();

    [SerializeField] private GameSettings gameSettings;

    public static GameSettings GameSettings => Instance.gameSettings;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
