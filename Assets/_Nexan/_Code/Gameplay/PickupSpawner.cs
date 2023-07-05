using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : SingletonParent<PickupSpawner>
{
    public string[] m_PickupsTags;
    public float m_LeftSpawnExtent,m_RightSpawnExtent;
    [Range(5f,15f)] public float m_minFallSpeed,m_maxFallSpeed;
    public float spawnRate, spawnInterval;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        GameManager.Instance.OnPickupSpawnRateChange += HandlePickupSpawnRateChange;
        StartSpawningPickups();
    }

    private void HandlePickupSpawnRateChange(float obj)
    {
        CancelInvoke();
        spawnRate = obj;
        StartSpawningPickups();
       
    }

    public void StartSpawningPickups()
    {
        InvokeRepeating("SpawnRandomPickups", spawnInterval, spawnRate);//ToDo: magic numbers!
    }
    private void SpawnRandomPickups()
    {
        var _randomXPos = Random.Range(m_LeftSpawnExtent, m_RightSpawnExtent);
        var _randomSpawnVector = new Vector3(_randomXPos, transform.position.y, transform.position.z);

        var _randomPickup = Random.Range(0, m_PickupsTags.Length);
        GameObject spawnedPickup;
        spawnedPickup = PoolManager.Instance.GetPooledItem("pickup");
        spawnedPickup.SetActive(true);
        spawnedPickup.transform.SetPositionAndRotation(_randomSpawnVector, Quaternion.identity);
        //set a random fall speed
        spawnedPickup.GetComponent<Rigidbody>().isKinematic = true;
        spawnedPickup.GetComponent<PickupBehaviour>().SetFallSpeed(Random.Range(m_minFallSpeed, m_maxFallSpeed));

        
        GameColors m_ActiveColor = GetInitialRandomColor();
        spawnedPickup.GetComponent<PickupBehaviour>().SetActiveMaterial(m_ActiveColor);

    }

    private GameColors GetInitialRandomColor()
    {
        int randomColorInt = UnityEngine.Random.Range(0, 4);
        GameColors m_ActiveColor = GameColors.BLUE;
        switch (randomColorInt)
        {
            case 0:
                m_ActiveColor = GameColors.RED;
                break;
            case 1:
                m_ActiveColor = GameColors.BLUE;
                break;
            case 2:
                m_ActiveColor = GameColors.GREEN;
                break;
            case 3:
                m_ActiveColor = GameColors.YELLOW;
                break;
        }

        return m_ActiveColor;
    }
}
