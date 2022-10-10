using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{

    [SerializeField] public float rarity = 0.1f;
    [SerializeField] public string Name = "red-shroom";

    // // Start is called before the first frame update
    void Start()
    {


    }


    // Update is called once per frame
    void Update()
    {
    }

    public GameObject Picked()
    {
        Debug.Log("a");
        var go = transform.parent.gameObject;
        var sut = Instantiate(go); //TODO: FUCK ME HARD AND REMOVE ME AFTER OTHER PRACTICES ARE IN USE!!!
        Destroy(go);
        return sut;
    }
}
