using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContext : MonoBehaviour
{
    [SerializeField] private List<GameObject> parents;

    public List<GameObject> PARENTS => parents;

    public void removeElem(GameObject parent)
    {
        parents.Remove(parent);
    }
}