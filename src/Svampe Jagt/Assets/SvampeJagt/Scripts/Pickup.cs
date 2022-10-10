using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pickup : MonoBehaviour

{

    float maxDistance = 1500f;
    LineRenderer lr;
    Inventory _inv;

    // // Start is called before the first frame update
    void Start()
    {
        lr = transform.gameObject.AddComponent<LineRenderer>();
        _inv = gameObject.GetComponent<Inventory>();

    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {

            Vector3 playerPosition = Camera.main.transform.position;
            Vector3 forwardDirection = Camera.main.transform.forward;

            if (Physics.Raycast(playerPosition, forwardDirection, out RaycastHit hit, maxDistance) && hit.collider != null && hit.collider.gameObject.CompareTag("Pickupable"))
            {

                Debug.Log("hello im here");
                var positionOfShit = hit.collider.gameObject.transform.position;

                var pickedObject = hit.collider.gameObject;
                string pickedObjectName = pickedObject.GetComponent<Pickupable>().Name;

                Vector3[] positions = { playerPosition, positionOfShit };
                lr.SetPositions(positions);



                var prefab = pickedObject.GetComponent<Pickupable>().Picked();
                Item pickedItem = new Item(pickedObjectName, prefab);
                _inv.AddItem(pickedItem);
            }



        }

    }
    void FixedUpdate()
    {


    }
}
