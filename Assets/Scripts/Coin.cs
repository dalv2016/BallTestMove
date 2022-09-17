using UnityEngine;
using System;

public class Coin : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem CoinDestroy;
    public static Action _cionAdd;
    public static int _value = 10;
    private void Update()
    {
        transform.Rotate(new Vector3(0,0.3f,0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {   
            Instantiate(CoinDestroy, transform.position, Quaternion.identity);
            Destroy(gameObject);
            _cionAdd?.Invoke();
        }
    }
}
