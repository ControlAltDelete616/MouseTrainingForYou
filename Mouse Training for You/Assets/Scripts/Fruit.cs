using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;
    public float explosionforce = 5;

    public void CreateSlicedFruit()
    {
        GameObject inst = Instantiate(slicedFruitPrefab, transform.position, transform.rotation);

        Rigidbody[] rbOnSliced = inst.transform.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody r in rbOnSliced)
        {
            r.transform.rotation = Random.rotation;
            r.AddExplosionForce(Random.Range(500, 1000), transform.position, explosionforce);
        }

        FindObjectOfType<GameManager>().IncreaseScore(1);

        Destroy(inst.gameObject, 4);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Blade b = collision.GetComponent<Blade>();

        if (!b)
        {
            return;
        }
        CreateSlicedFruit();
    }


    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateSlicedFruit();
        }
    }*/
}
