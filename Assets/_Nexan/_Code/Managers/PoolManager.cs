using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonParent<PoolManager> 
{
    [Header("List of objects to be pooled")]
    public List<PoolItem> poolItems = new List<PoolItem>();
    [SerializeField]private List<Transform> poolItemsList;

    private void Start()
    {
        InitializePool();
    }


    public GameObject GetPooledItem(string _tag)
    {
        foreach (var item in poolItemsList)
        {
            if(item.tag == _tag && item.gameObject.activeInHierarchy == false) 
            {
                return item.gameObject; 
            }
        }
        return null;
    }
    public void InitializePool()
    {
        foreach (var poolItem in poolItems)
        {
            for (int i = 0; i < poolItem.countOfObjectsInPool; i++)
            {

                var spawnedObj = Instantiate(poolItem.gameObjectToBePooled);
                spawnedObj.transform.SetParent(transform, false);
                spawnedObj.SetActive(false);
                poolItemsList.Add(spawnedObj.transform);
            }
        }
        if(poolItemsList.Count > 0)
        {
            foreach(var poolItem in poolItemsList)
            {
                if(poolItem.gameObject.activeInHierarchy == true)
                {
                    poolItem.gameObject.SetActive(false);   
                }
            }
        }

      
    }

    private void InitializePickupActiveColor(GameColors _initialColor)
    {
        //iterate through every pooled item
        //set their respective active color to whatever their mat
    }
}
