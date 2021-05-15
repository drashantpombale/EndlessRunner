using UnityEngine;

public class SpawnEvent : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, 0, player.position.z-28);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9) PlatformPooler.platformSpawn.Invoke();
    }
}
