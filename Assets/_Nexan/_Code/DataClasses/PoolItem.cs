using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem 
{
   [field:SerializeField] public GameObject gameObjectToBePooled { get; private set; }
   [field:SerializeField]public int countOfObjectsInPool { get; private set; }
}
