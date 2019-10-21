using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTester : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SaveSystem<Vector3>.SaveInternalBinary(transform.position, 1);
        }

        if (Input.GetMouseButtonDown(1))
        {
            var dato = SaveSystem<Vector3>.LoadInternalBinary<Vector3>(1);
            Debug.Log(dato);
        }
    }
}
