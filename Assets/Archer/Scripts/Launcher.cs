using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject characterArcher;
    public GameObject defenseSpawnPoint;
    public GameObject defensePoints;
    public GameObject enemySpawnPoints;
    private int currentDefenseUnit = 1;
    private int maxDefenseUnit = 0;
    private int currentEnemyUnit = 1;
    private int maxEnemyUnit = 0;

    private void Start()
    {
        maxEnemyUnit = enemySpawnPoints.GetComponentsInChildren<Transform>().Length;
        maxDefenseUnit = defensePoints.GetComponentsInChildren<Transform>().Length;

        StartCoroutine(DelaySpawn(2f, "Red team"));
        StartCoroutine(DelaySpawn(2f, "Blue team"));
    }

    public void GenerateCharacterForBlueTeam(GameObject characterPrefabs, Vector3 spawnPosition, Transform destination)
    {
        GameObject newCharater = Instantiate(characterPrefabs, spawnPosition + Vector3.up, Quaternion.identity);
        newCharater.layer = LayerMask.NameToLayer("Blue team");
        newCharater.GetComponent<NavMeshController>().SetTarget(destination);
    }
    public void GenerateCharacterForRedTeam(GameObject characterPrefabs, Vector3 position, Transform destination)
    {
        GameObject newCharater = Instantiate(characterPrefabs, position + Vector3.up, Quaternion.identity);
        newCharater.layer = LayerMask.NameToLayer("Red team");
        newCharater.GetComponent<NavMeshController>().SetTarget(destination);
    }

    IEnumerator DelaySpawn(float delayTime, string teamName)
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            if (teamName.Contains("Red team") && currentEnemyUnit < maxEnemyUnit)
            {
                GenerateCharacterForRedTeam(characterArcher, enemySpawnPoints.GetComponentsInChildren<Transform>()[currentEnemyUnit].position, defenseSpawnPoint.transform);
                currentEnemyUnit++;
            }
            else if (teamName.Contains("Blue team") && currentDefenseUnit < maxDefenseUnit)
            {
                GenerateCharacterForBlueTeam(characterArcher, defenseSpawnPoint.transform.position, defensePoints.GetComponentsInChildren<Transform>()[currentDefenseUnit]);
                currentDefenseUnit++;
            }
        }
    }
}
