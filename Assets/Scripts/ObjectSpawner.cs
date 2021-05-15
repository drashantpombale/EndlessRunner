using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject coin;
    public GameObject obstacle;
    List<GameObject> coins;
    List<GameObject> obstacles;
    // Start is called before the first frame update
    private int coinSpawns = 5;
    private int obstacleSpawns = 5;
    private int[] positions = { -3, 0, 3 };
    private float[] boxheight = { 1.5f, 0 };
    void Start()
    {
        coins = new List<GameObject>();
        obstacles = new List<GameObject>();

        for (int i = 0; i < coinSpawns; i++) {
            GameObject obj = Instantiate(coin, transform);
            obj.transform.position = new Vector3(positions[Random.Range(0,positions.Length)], 1.5f, Random.Range(transform.position.z - 28, transform.position.z + 28));
            coins.Add(obj);
        }
        for (int i = 0; i < obstacleSpawns; i++)
        {
            GameObject obj = Instantiate(obstacle, transform);
            obj.transform.position = new Vector3(positions[Random.Range(0, positions.Length)], boxheight[Random.Range(0, boxheight.Length)], Random.Range(transform.position.z - 28, transform.position.z + 28));
            obstacles.Add(obj);
        }
    }

    public void RandomEnableObjects() {
        for (int i = 0; i < coins.Count; i++) {
            coins[i].SetActive(true);
            coins[i].transform.position= new Vector3(positions[Random.Range(0, positions.Length)], 1.5f, Random.Range(transform.position.z - 28, transform.position.z + 28));
        }
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].SetActive(true);
            obstacles[i].transform.position = new Vector3(positions[Random.Range(0, positions.Length)], boxheight[Random.Range(0, boxheight.Length)], Random.Range(transform.position.z - 28, transform.position.z + 28));
            obstacles[i].layer = 13;
        }
    }

    public List<GameObject> GetObstacles()
    {
        return obstacles;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
