using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class invadeManager : MonoBehaviour
{
    public GameObject invadeBar;
    public map_joueur player;
    public float invadeTime = 2f;
    private Image[] bloc; 

    public UnityEvent invadeFailed;
    public UnityEvent invadeEnd;

    private bool invadeInProgress = false;
    // Start is called before the first frame update
    void Start()
    {
        bloc = invadeBar.GetComponentsInChildren<Image>();
    }

    public void onInvade(float duration) {
        if(!invadeInProgress) {
            invadeInProgress = true;
            StartCoroutine(lightBlocsAnim(duration));
            StartCoroutine(waitInvade());
        } else {
            invadeFailed.Invoke();
        }
        
    }

    public bool isInvadeInProgress() {
        return invadeInProgress;
    }

    IEnumerator lightBlocsAnim(float duration)
    {
        foreach(Image i in bloc) {
            i.color = Color.white;
            yield return new WaitForSeconds(duration/4f);
        }
        
    }

    IEnumerator waitInvade() {
        yield return new WaitForSeconds(invadeTime);
        invadeInProgress = false;
        Debug.Log("INVADE END");
        invadeEnd.Invoke();
    }

}
