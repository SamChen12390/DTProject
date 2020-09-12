using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    public GameObject instantiatePool;
    public GameObject characterArcher;
    public GameObject characterBerserker;
    public GameObject defenseSpawnPoint;
    public GameObject defensePoints;
    public GameObject enemySpawnPoints;
    public Button archerButton;
    public Button berserkerButton;
    private int currentArcherAmount = 1;
    private int maxArcherAmount = 0;
    private int currentBerserkerAmount = 1;
    private int maxBerserkerAmount = 0;
    private int currentEnemyUnit = 1;
    private int maxEnemyUnit = 0;

    [Space]
    public Text coinsText;
    private int coins = 1000;
    private int archerCost;
    private int berserkerCost;


    private void Start()
    {
        maxEnemyUnit = enemySpawnPoints.GetComponentsInChildren<Transform>().Length;
        maxArcherAmount = defensePoints.GetComponentsInChildren<Transform>().Length;
        archerCost = characterArcher.GetComponent<Character>().cost;

        StartCoroutine(DelaySpawn(2f));
    }

    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        coinsText.text = coins.ToString();

        if (currentArcherAmount == maxArcherAmount)
        {
            SetButton(archerButton, false, "M A X", Color.red);
        }
        else
        {
            if (coins >= archerCost)
                SetButton(archerButton, true, archerCost.ToString(), Color.cyan);
            else
                SetButton(archerButton, false, archerCost.ToString(), Color.red);
        }

        if (currentBerserkerAmount == maxBerserkerAmount)
        {
            SetButton(berserkerButton, false, "M A X", Color.red);
        }
        else
        {
            if (coins >= berserkerCost)
                SetButton(berserkerButton, true, berserkerCost.ToString(), Color.cyan);
            else
                SetButton(berserkerButton, false, berserkerCost.ToString(), Color.red);
        }
    }

    void SetButton(Button button, bool interactable, string text, Color color)
    {
        button.interactable = interactable;
        button.GetComponentInChildren<Text>().text = text;
        button.GetComponentInChildren<Text>().color = color;
    }

    public void clickArcherButton()
    {
        if (currentArcherAmount < maxArcherAmount)
        {
            GenerateCharacterForBlueTeam(characterArcher, defenseSpawnPoint.transform.position, defensePoints.GetComponentsInChildren<Transform>()[currentArcherAmount]);
            currentArcherAmount++;
            coins -= archerCost;
        }
    }

    public void clickBerserkerButton()
    {

    }

    public void AddCoins(int value)
    {
        coins += value;
    }

    public void GenerateCharacterForBlueTeam(GameObject characterPrefabs, Vector3 spawnPosition, Transform destination)
    {
        GameObject newCharater = Instantiate(characterPrefabs, spawnPosition + Vector3.up, Quaternion.identity);
        newCharater.layer = LayerMask.NameToLayer("Blue team");
        newCharater.GetComponent<NavMeshController>().SetTarget(destination);
        newCharater.transform.SetParent(instantiatePool.transform);
    }
    public void GenerateCharacterForRedTeam(GameObject characterPrefabs, Vector3 position, Transform destination)
    {
        GameObject newCharater = Instantiate(characterPrefabs, position + Vector3.up, Quaternion.identity);
        newCharater.layer = LayerMask.NameToLayer("Red team");
        newCharater.GetComponent<NavMeshController>().SetTarget(destination);
        newCharater.transform.SetParent(instantiatePool.transform);
    }

    IEnumerator DelaySpawn(float delayTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            if (currentEnemyUnit < maxEnemyUnit)
            {
                GenerateCharacterForRedTeam(characterArcher, enemySpawnPoints.GetComponentsInChildren<Transform>()[currentEnemyUnit].position, defenseSpawnPoint.transform);
                currentEnemyUnit++;
            }
        }
    }
}