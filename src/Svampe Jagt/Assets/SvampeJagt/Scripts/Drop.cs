using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{


    Inventory _inv;
    public GameObject redSvamp;
    // Start is called before the first frame update
    void Start()
    {
        _inv = gameObject.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Q))
        {
            var pos = transform.position + transform.forward * 2;
            Instantiate(redSvamp, pos, Quaternion.identity);
        }

    }
}
