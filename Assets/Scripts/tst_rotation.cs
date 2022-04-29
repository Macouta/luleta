using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tst_rotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        print(angle);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = rotation;// Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

    }
}
