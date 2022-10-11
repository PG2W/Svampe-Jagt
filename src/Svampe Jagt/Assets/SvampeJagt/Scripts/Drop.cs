using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{

    Inventory _inv;

    void Start()
    {
        _inv = gameObject.GetComponent<Inventory>();
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Q))
        {
            DropItemFirstItem();
        }

    }

    private void DropItemFirstItem()
    {
        if(_inv.Items.Count <= 0) return;

        var pos = transform.position + transform.forward * 2;
        Instantiate(_inv.Items[0].Prefab, pos, Quaternion.identity);
        _inv.DecrementItemCount(_inv.Items[0].Name);
    }
}
