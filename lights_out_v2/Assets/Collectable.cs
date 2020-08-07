using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    heart,
    soul
}
public class Collectable : MonoBehaviour
{
    public CollectableType collectable;
    public float lifeAdded;
    public GameObject effect;
    public Inventory playerInventory;
    public Item item;
    public float attractDistance;
    private Transform _target;
    private Rigidbody2D _rb;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if(Vector2.Distance(_target.position, transform.position) <= attractDistance)
        {
            Vector2 direction = ((Vector2)_target.position - _rb.position).normalized;
            Vector2 force = direction * 100f * Time.deltaTime;
            _rb.AddForce(force);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            switch (collectable)
            {
                case CollectableType.soul:
                    playerInventory.AddItem(item);
                    break;
                case CollectableType.heart:
                    collision.GetComponent<PlayerHealth>().AddHealth(lifeAdded);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
