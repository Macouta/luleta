using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesBox : MonoBehaviour
{

    public GameObject tradeButton;
    public GameObject invadeLever;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {  
            // Debug.Log("click " + Camera.main.name);
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            // Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) {  
                // Debug.Log("hit : " + hit.transform.name);
                if(hit.transform.name == tradeButton.name)
                    onTradeButtonClicked(hit.transform);

                if(hit.transform.name == invadeLever.name) {

                }
            }  
        }  
    }

    void onTradeButtonClicked(Transform t) {

    }

    void onInvadeLeverClicked(Transform t) {
        
    }
}
