using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{


    public ItemDictionaryScriptableObject itemDictionaryScriptableObject;
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

        if (Input.GetKeyUp(KeyCode.Q) && _inv.Items.Count > 0)
        {
            var pos = transform.position + transform.forward * 2;
            Instantiate(_inv.Items[0].Prefab, pos, Quaternion.identity);
            _inv.DecrementItemCount(_inv.Items[0].Name);
        }

    }
}
