using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

using Sirenix.OdinInspector;

public class InteractionManager : MonoBehaviour
{
    public float jumpDuration = 3f;

    public float cooldownClickTimer = 1f;
    private float cooldownClickTimerLeft;

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
    public Ease invadeLeverStartEasing;
    [BoxGroup("Invade")]
    public Ease invadeLeverFailedEasing;
    [BoxGroup("Invade")]
    public float invadeLeverAnimTime = 1f;
    [BoxGroup("Invade")]
    public invadeManager invadeManager;
    public resourcesManager rm;

    [BoxGroup("Audio")]
    public AudioManager audioManager;

    [Space]

    public UnityEvent onPowerOn;
    public UnityEvent onPowerOff;
    public UnityEvent onTrade;
    public UnityEvent onTradeFailed;
    public UnityEvent<float> onInvadeStart;
    public UnityEvent<float> onJumpStart;
    public UnityEvent onJumpEnd;
    public UnityEvent onJumpStartFailed;


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

     void Start() {
         cameraRig = Camera.main.transform.parent.GetComponent<CameraRig>();
         cooldownClickTimerLeft = cooldownClickTimer;
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

                if (name == powerButton.name)
                    onPowerButtonClicked(hit.transform);

                if(poweredOn) {
                    // Debug.Log("hit : " + hit.transform.name);
                    if(name == tradeButton.name)
                        onTradeButtonClicked(hit.transform);
                    if(name == invadeLever.name)
                        onInvadeLeverClicked(hit.transform);
                    if(name == jumpButton.name)
                        onJumpStartButtonClicked(hit.transform);
                    
                    if (name == wheel.name)
                    {
                        WheelHold = true;
                        audioManager.Play_wheel(true);
                    }
                }
            }  
        }

        if(Input.GetMouseButtonUp(0)) {
            WheelHold = false;
            audioManager.Play_wheel(false);
        }

        //Cooldown click
        if(clickCooldown) {
            cooldownClickTimerLeft -= Time.deltaTime;
            if(cooldownClickTimerLeft < 0) {
                clickCooldown = false;
                cooldownClickTimerLeft = cooldownClickTimer;
            }
        }

        if(wheelHold) {
            float mouseX = Input.GetAxis("Mouse X") * wheelRotSpeed * Time.deltaTime;
            audioManager.SetWheelSpeed(Input.GetAxis("Mouse X"));
            wheel.transform.Rotate(new Vector3(0, mouseX, 0));
        }
    }

    void onTradeButtonClicked(Transform t) {
        if(player.Astre_actuel && !rm.isTradeDone()) {
            Debug.Log("TRADE");
            onTrade.Invoke();
            t.DOLocalMoveZ(t.localPosition.z - 0.03f, 0.4f).SetLoops(2, LoopType.Yoyo);
        } else {
            onTradeFailed.Invoke();
        }
    }

    void onInvadeLeverClicked(Transform t) {
        if(!invadeManager.isInvadeInProgress()) {
            Debug.Log("INVADE");
            onInvadeStart.Invoke(invadeLeverAnimTime);
            // t.DORotate(t.eulerAngles - new Vector3(90f, 0, 0), invadeLeverAnimTime).SetEase(invadeLeverStartEasing);
        } else {
            invadeManager.invadeFailed.Invoke();
        }
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

    void onJumpStartButtonClicked(Transform t){
        if(player.isJumpAllowed()) {
            Debug.Log("JUMP");
            onJumpStart.Invoke(jumpDuration);
            t.DOLocalMoveZ(t.localPosition.x - 0.05f, 0.5f).SetLoops(2, LoopType.Yoyo);
            StartCoroutine(jumpWait());
        } else {
            onJumpStartFailed.Invoke();
        }
    }

    IEnumerator jumpWait()
    {
        yield return new WaitForSeconds(jumpDuration + 0.05f);
        onJumpEnd.Invoke();
    }
}
