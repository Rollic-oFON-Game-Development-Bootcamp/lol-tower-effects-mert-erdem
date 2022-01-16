using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPlayer : MonoBehaviour
{
    private float towerRange => SettingsManager.GameSettings.TowerRange;

    public void GetHitByEnemy(TeamPlayer teamPlayer)
    {
        var tower = GetTowerInRange();

        tower.Complain(teamPlayer);
    }

    private LolTower GetTowerInRange()
    {
        LolTower result = null;

        var allTowers = TowersManager.AllTowers;
        result = allTowers.OrderBy(o => (transform.position - o.transform.position).sqrMagnitude)
            .FirstOrDefault(o => (transform.position - o.transform.position).sqrMagnitude <= towerRange * towerRange);

        return result;
    }
}
