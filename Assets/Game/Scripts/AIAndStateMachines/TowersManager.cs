using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersManager : MonoBehaviour
{
    private static TowersManager instance;
    public static TowersManager Instance => instance ??= FindObjectOfType<TowersManager>();

    [SerializeField] private List<LolTower> allTowers;

    public static List<LolTower> AllTowers => Instance.allTowers;

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
