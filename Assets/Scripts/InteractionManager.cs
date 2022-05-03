using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

using Sirenix.OdinInspector;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager manager;

    public float cooldownTimer = 1f;
    private float cooldownTimerLeft;

    [BoxGroup("OnButton")]
    public GameObject powerButton;

    [BoxGroup("Wheel")]
    public GameObject wheel;
    [BoxGroup("Wheel")]
    public map_joueur player;
    [BoxGroup("Wheel")]
    public float wheelRotSpeed = 200f;
    [BoxGroup("Jump")]
    public GameObject jumpButton;
    [BoxGroup("Trade")]
    public GameObject tradeButton;

    [BoxGroup("Invade")]
    public GameObject invadeLever;
    [BoxGroup("Invade")]
    public Ease invadeLeverEasing;
    [BoxGroup("Invade")]
    public float invadeLeverAnimTime = 1f;

    [Space]

    public UnityEvent onPowerOn;
    public UnityEvent onPowerOff;
    public UnityEvent onTrade;
    public UnityEvent onInvade;
    public UnityEvent onJump;
    public UnityEvent onJumpFailed;


    private bool wheelHold = false;
    private bool clickCooldown = false;

    private bool poweredOn = false;

    private CameraRig cameraRig;

    public bool WheelHold { 
        get => wheelHold; 
        set {
            wheelHold = value; 
            cameraRig.MouseHold = value;
            player.MouseHold = value;
        }
    }

    void Awake()
     {
         if (manager != null && manager != this)
             Destroy(gameObject);
 
         manager = this;
     }

     void Start() {
         cameraRig = Camera.main.transform.parent.GetComponent<CameraRig>();
         cooldownTimerLeft = cooldownTimer;
     }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !clickCooldown) {  
            // Debug.Log("click " + Camera.main.name);
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            // Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) {  
                string name = hit.transform.name;
                if(hit.transform.tag == "Interactible") {
                    clickCooldown = true;
                }
                // Debug.Log("hit : " + hit.transform.name);
                if(name == tradeButton.name)
                    onTradeButtonClicked(hit.transform);
                if(name == invadeLever.name)
                    onInvadeLeverClicked(hit.transform);
                if(name == jumpButton.name)
                    onJumpButtonClicked(hit.transform);
                if (name == powerButton.name)
                    onPowerButtonClicked(hit.transform);
                if (name == wheel.name)
                    WheelHold = true;
            }  
        }

        if(Input.GetMouseButtonUp(0)) {
            WheelHold = false;
        }

        //Cooldown click
        if(clickCooldown) {
            cooldownTimerLeft -= Time.deltaTime;
            if(cooldownTimerLeft < 0) {
                clickCooldown = false;
                cooldownTimerLeft = cooldownTimer;
            }
        }

        if(wheelHold) {
            float mouseX = Input.GetAxis("Mouse X") * wheelRotSpeed * Time.deltaTime;
            wheel.transform.Rotate(new Vector3(0, mouseX, 0));
        }
    }

    void onTradeButtonClicked(Transform t) {
        Debug.Log("TRADE");
        onTrade.Invoke();
        t.DOLocalMoveZ(t.localPosition.z - 0.02f, 0.4f).SetLoops(2, LoopType.Yoyo);
    }

    void onInvadeLeverClicked(Transform t) {
        Debug.Log("INVADE");
        t.DORotate(t.eulerAngles - new Vector3(90f, 0, 0), invadeLeverAnimTime).SetEase(invadeLeverEasing).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
            onInvade.Invoke();
        });
    }

    void onPowerButtonClicked(Transform t){
        if (!poweredOn){ // si �teint
            poweredOn = true;
            Debug.Log("POWER ON");
            onPowerOn.Invoke();
        }
        else if (poweredOn){ // si allum�
            poweredOn = false;
            Debug.Log("POWER OFF");
            onPowerOff.Invoke();
        }
        t.DOLocalMoveZ(t.localPosition.z - 0.05f, 0.5f).SetLoops(2, LoopType.Yoyo);
    }

    void onJumpButtonClicked(Transform t){
        if(player.isJumpAllowed()) {
            Debug.Log("JUMP");
            onJump.Invoke();
            t.DOLocalMoveZ(t.localPosition.x - 0.05f, 0.5f).SetLoops(2, LoopType.Yoyo);
        } else {
            onJumpFailed.Invoke();
        }
        
    }
}
