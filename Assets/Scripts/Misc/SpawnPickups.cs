using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    public Pickups[] pickupsPrefabArray;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(pickupsPrefabArray[Random.Range(0, 3)], transform.position, transform.rotation);
    }

    void Update()
    {
    }

}
