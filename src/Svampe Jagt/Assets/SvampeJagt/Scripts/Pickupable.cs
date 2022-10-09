using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // Update is called once per frame
    void Update()
    {
    }

    public void Picked()
    {
        Debug.Log("a");
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
