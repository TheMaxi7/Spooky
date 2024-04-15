using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawner : MonoBehaviour, IInteractable
{
    public GameObject allyPrefab;
    public Transform spawnAllyPoint;
    public void Interact()
    {
        SpawnAlly();
    }

    public void SpawnAlly()
    {
        if (UiManager.soulsNumber >= UiManager.soulsNeeded && GameManager.bossSpawned)
        {
            Instantiate(allyPrefab, spawnAllyPoint.position, Quaternion.identity);
            UiManager.soulsNumber =0;
        }
    }

    public string GetDescription()
    {
        return "Summon";
    }

}
