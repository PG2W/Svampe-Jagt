using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pickup : MonoBehaviour

{

    float maxDistance = 1500f;
    LineRenderer lr;

    // // Start is called before the first frame update
    void Start()
    {
        lr = transform.gameObject.AddComponent<LineRenderer>();
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
                int pickedObjectId = pickedObject.GetComponent<Pickupable>().Id;
                string pickedObjectName = pickedObject.GetComponent<Pickupable>().Name;

                Vector3[] positions = { playerPosition, positionOfShit };
                lr.SetPositions(positions);


                Item pickedItem = new Item(pickedObjectId, pickedObjectName);
                Debug.Log(pickedItem.Id);
                // Debug.Log(Inv.Items);


                pickedObject.GetComponent<Pickupable>().Picked();

            }


        }

    }
    void FixedUpdate()
    {


    }
}
