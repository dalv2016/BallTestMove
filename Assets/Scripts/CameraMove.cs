using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    [SerializeField]
    private float x,y,z;

    void Start()
    {
        transform.position = new Vector3(player.transform.position.x+x, player.transform.position.y + y, player.transform.position.z + z);
        offset = transform.position - player.transform.position;
        
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
