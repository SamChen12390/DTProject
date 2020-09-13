using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateArcher : MonoBehaviour
{
    public Transform[] ArcherPlace;
    public Transform[] BerserkerPlace;
    public GameObject archer;
    public GameObject berserker;
    private Transform archerTransform;
    private Transform berserkerTransform;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<6; ++i)
        {
            string num = "Archer" + i.ToString();
            archerTransform = GameObject.Find(num).transform;
            ArcherPlace[i] = archerTransform;
        }
        for(int i=0; i<3;++i)
        {
            string num1 = "gate" + i.ToString();
            berserkerTransform = GameObject.Find(num1).transform;
            BerserkerPlace[i] = berserkerTransform;
        }
        placeArmy(archer, ArcherPlace);
        randomSetEnemy(berserker, BerserkerPlace);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>=3.0f)
        {
            randomSetEnemy(berserker, BerserkerPlace);
            time = 0;
        }
    }
    void placeArmy(GameObject gameObject, Transform[] transformArray)
    {
        for(int i=0; i<transformArray.Length;++i)
        {
            Instantiate(gameObject, transformArray[i]);
        }
    }
    void randomSetEnemy(GameObject gameObject, Transform[] trans)
    {
        Instantiate(gameObject, trans[Random.Range(0, 3)]);
    }
}
