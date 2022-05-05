using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class invadeManager : MonoBehaviour
{
    public GameObject invadeBar;
    public GameObject difficultyBar;
    public Gradient difficultyRamp;
    public map_joueur player;
    public float invadeTime = 2f;
    private Image[] blocDifficulty; 
    private Image[] blocInvade; 

    public UnityEvent invadeFailed;
    public UnityEvent invadeEnd;

    private Astre current;

    private bool invadeInProgress = false;
    // Start is called before the first frame update
    void Start()
    {
        blocInvade = invadeBar.GetComponentsInChildren<Image>();
        blocDifficulty = difficultyBar.GetComponentsInChildren<Image>();
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

    public void onJumpEnd() {
        current = player.Astre_actuel;
        difficultyBloc(current.defense);
    }

    private void difficultyBloc(int defense) {
        StartCoroutine(difficultyBlocsAnim(0.5f, defense));
        float ratio = defense / 100f;
        Debug.Log(ratio);
        Debug.Log(defense);
        // if(defense > 0) {
        //     blocDifficulty[blocDifficulty.Length - 1].color = difficultyRamp.Evaluate(ratio);
        //     if(defense > 20)
        //         blocDifficulty[blocDifficulty.Length - 2].color = difficultyRamp.Evaluate(ratio);
        //     if(defense > 40)
        //         blocDifficulty[blocDifficulty.Length - 2].color = difficultyRamp.Evaluate(ratio);
        //     if(defense > 60)
        //         blocDifficulty[blocDifficulty.Length - 2].color = difficultyRamp.Evaluate(ratio);
        //     if(defense > 80)
        //         blocDifficulty[blocDifficulty.Length - 2].color = difficultyRamp.Evaluate(ratio);
        // }
        
    }

    IEnumerator difficultyBlocsAnim(float duration, float defense)
    {
        float ratio = defense / 100f;
        float threshold = 0;
        for(int i = blocDifficulty.Length - 1; i >= 0; i--) {
            blocDifficulty[i].color = Color.black;
            yield return new WaitForSeconds(duration/5f);
            if(defense >= threshold)
                blocDifficulty[i].color = difficultyRamp.Evaluate(1f - (float)i/(float)blocDifficulty.Length);
            threshold += 20;
        }
    }

    public bool isInvadeInProgress() {
        return invadeInProgress;
    }

    IEnumerator lightBlocsAnim(float duration)
    {
        foreach(Image i in blocInvade) {
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
