using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 position = player.position + new Vector3(0, 10, -15);
        Vector3 fineposition = Vector3.Lerp(transform.position, position, 1f);
        transform.position = fineposition;
        transform.rotation = Quaternion.Euler(-20, 0, 0);

        transform.LookAt(player);
    }   
}
