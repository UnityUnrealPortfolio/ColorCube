using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public string[] m_PickupsTags;
    public float m_LeftSpawnExtent,m_RightSpawnExtent;
    [Range(5f,15f)] public float m_minFallSpeed,m_maxFallSpeed;
    public float spawnRate, spawnInterval;

  
    private void Start()
    {
       
        InvokeRepeating("SpawnRandomPickups", spawnInterval, spawnRate);//ToDo: magic numbers!
    }
    private void SpawnRandomPickups()
    {
        var _randomXPos = Random.Range(m_LeftSpawnExtent,m_RightSpawnExtent);
        var _randomSpawnVector = new Vector3(_randomXPos,transform.position.y,transform.position.z);

        var _randomPickup = Random.Range(0,m_PickupsTags.Length);
        GameObject spawnedPickup;
        spawnedPickup = PoolManager.Instance.GetPooledItem(m_PickupsTags[_randomPickup]);
        spawnedPickup.SetActive(true);
        spawnedPickup.transform.SetPositionAndRotation(_randomSpawnVector, Quaternion.identity);
        //set a random fall speed
        spawnedPickup.GetComponent<Rigidbody>().isKinematic = true;
        spawnedPickup.GetComponent<PickupBehaviour>().SetFallSpeed(Random.Range(m_minFallSpeed,m_maxFallSpeed));
        spawnedPickup.GetComponent<PickupBehaviour>().SetActiveMaterial(GameManager.Instance.GetRandomMaterial());

    }


}
