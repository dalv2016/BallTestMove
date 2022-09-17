using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float _speed;
    private float _distance;
    public static int _health;
    private Animator _animator;
    private Vector3 _normalizeVector;
    private Vector3 _direction;
    private Vector3 _firsstposition;
    [SerializeField]
    private float rotationlerp;    
    [SerializeField]
    private GameObject effect;
    public static Action _openMenu;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Health"))
            PlayerPrefs.SetInt("Health", _health);
    }
    void Start()
    {
        _firsstposition = transform.position;
        _health = 3;
        _animator = GetComponent<Animator>();

    }
    private void Update()
    {
        PlayerPrefs.GetInt("Health");
        Move();
        Rotation();
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint cp = collision.contacts[0];
        _normalizeVector = Vector3.Reflect(_normalizeVector, cp.normal);
        if (collision.gameObject.tag=="Wall")
        {
            var c = Instantiate(effect, transform.position, Quaternion.identity);
            c.transform.eulerAngles = collision.transform.eulerAngles;
            _animator.SetBool("Idle", false);
            _animator.SetTrigger("Beat");
        }
        if (collision.gameObject.tag == "Thorn")
        {
            TakeDamage();
        }
    }
   private void Rotation()
    {
        Quaternion rotation = Quaternion.LookRotation(_normalizeVector, Vector3.up);
        transform.rotation = rotation;
        transform.Rotate(-_speed * 200, 0, 0, Space.Self);
    }

    private void Move()
    {
        Vector3 pos = transform.position + _normalizeVector * _speed * 2 * Time.deltaTime;
        _speed = Mathf.Lerp(_speed, 0, Time.deltaTime * rotationlerp);
        transform.position = new Vector3(pos.x, 0, pos.z);
    }   
    public void CalculateSpeed(Vector3 startpos, Vector3 endpos)
    {
        _direction = endpos - startpos;
        _distance = _direction.magnitude;
       
    }
    public void Vectornormalized()
    {
        _speed = _distance;
        _normalizeVector = _direction / _distance;
    }
    public void TakeDamage()
    {
       
        if (_health > 0)
        {
            _health--;
            PlayerPrefs.SetInt("Health", _health);
        }       
         if(_health <= 0)
        {
            _openMenu?.Invoke();
            transform.position = _firsstposition;
            _speed = 0;
            _health = 3;
        }
    }
    private void OnEnable()
    {
        InputController._speedCalc += CalculateSpeed;
        InputController._normalVect3 += Vectornormalized;
    }

    private void OnDisable()
    {
        InputController._speedCalc -= CalculateSpeed;
        InputController._normalVect3 -= Vectornormalized;
    }
}
