using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCharacter : MonoBehaviour
{
    public List<NavMeshController> characters;

    private void Update()
    {
        characters.Clear();
        for (int i = 0; i < transform.GetComponentsInChildren<NavMeshController>().Length; i++)
        {
            if (!characters.Contains(transform.GetComponentsInChildren<NavMeshController>()[i]))
            {
                characters.Add(transform.GetComponentsInChildren<NavMeshController>()[i]);
            }
        }
    }

    public void OnFound()
    {
        foreach (var character in characters)
        {
            if (character != null)
            {
                character.Stop = false;
            }
        }
    }

    public void OnLost()
    {
        foreach (var character in characters)
        {
            if (character != null)
            {
                character.Stop = true;
            }
        }
    }
}
