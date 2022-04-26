using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager manager;

    public GameObject wheel;

    public GameObject jumpLever;
    public GameObject tradeButton;
    public GameObject invadeLever;
    public Ease invadeLeverEasing;
    public float invadeLeverAnimTime = 1f;

    [Space]

    public UnityEvent onTrade;
    public UnityEvent onInvade;
    public UnityEvent onJump;

    // Update is called once per frame

    private bool wheelHold = false;

    void Awake()
     {
         if (manager != null && manager != this)
             Destroy(gameObject);
 
         manager = this;
     }

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
                    onInvadeLeverClicked(hit.transform);
                }

                if(hit.transform.name == wheel.name) {
                    wheelHold = true;
                }
            }  
        }

        if(Input.GetMouseButtonUp(0)) {
            wheelHold = false;
        }

        // Debug.Log(wheelHold);
    }

    void onTradeButtonClicked(Transform t) {
        onTrade.Invoke();
        t.DOLocalMoveZ(t.localPosition.z - 0.03f, 0.5f).SetLoops(2, LoopType.Yoyo);
    }

    void onInvadeLeverClicked(Transform t) {
        t.DORotate(t.eulerAngles - new Vector3(90f, 0, 0), invadeLeverAnimTime).SetEase(invadeLeverEasing).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
            onInvade.Invoke();
        });
    }
}
