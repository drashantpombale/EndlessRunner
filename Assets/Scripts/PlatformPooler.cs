using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformPooler : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> platforms;
    public Pool pool;
    public Transform spawnPoint;
    public static UnityEvent platformSpawn;
    [System.Serializable]
    public class Pool {
        public GameObject platform;
        public int size;
    }

    private int k =0;
    void Start()
    {
        platforms = new List<GameObject>();
        for (int i = 0; i < pool.size; i++) {
            GameObject obj = Instantiate(pool.platform);
            obj.transform.position = spawnPoint.position;
            spawnPoint.position = new Vector3(0, 0, spawnPoint.position.z + 55);
            platforms.Add(obj);
        }

        if (platformSpawn == null)
        {
            platformSpawn = new UnityEvent();
            platformSpawn.AddListener(spawn);
        }
    }

    private void spawn()
    {
        platforms[k].transform.position = new Vector3(0, 0, platforms[k].transform.position.z + (55 * 5));
        ObjectSpawner obj = platforms[k].GetComponent<ObjectSpawner>();
        obj.RandomEnableObjects();
        k = (k + 1) % 5;
    }


    // Update is called once per frame
    void Update()
    {
    }
}
