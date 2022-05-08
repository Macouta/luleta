using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using TMPro;


public class invadeManager : MonoBehaviour
{
    [BoxGroup("UI")]
    public TextMeshProUGUI valueText;
    [BoxGroup("UI")]
    public Image reward_icon;
    [BoxGroup("UI")]
    public Sprite degat_icon;
    [BoxGroup("UI")]
    public Sprite energie_icon;
    [BoxGroup("UI")]
    public Sprite comestible_icon;
    [BoxGroup("UI")]
    public Sprite argent_icon;
    [Space]
    [BoxGroup("UI")]
    public GameObject invadeBar;
    [BoxGroup("UI")]
    public GameObject difficultyBar;
    [BoxGroup("UI")]
    public Gradient difficultyRamp;
    
    public GameObject attackBar;
    public map_joueur player;
    public float invadeTime = 2f;
    private Image[] blocDifficulty; 
    private Image[] blocInvade; 

    public UnityEvent invadeFailed;
    public UnityEvent<ResourceType, float> invadeEnd;

    private Astre current;
    private ResourceType currentReward;
    private float currentRewardValue;

    private bool invadeInProgress = false;
    // Start is called before the first frame update
    void Start()
    {
        blocInvade = invadeBar.GetComponentsInChildren<Image>();
        blocDifficulty = difficultyBar.GetComponentsInChildren<Image>();
    }

    public void onInvade(float duration) {
        invadeInProgress = true;
        StartCoroutine(lightBlocsAnim(duration));
        StartCoroutine(waitInvade());
    }

    public void onJumpEnd() {
        current = player.Astre_actuel;
        invadeInProgress = false;
        attackBar.SetActive(false);
        setReward(); 
        difficultyBloc(current.defense);
    }

    private void setReward() {
        currentReward = (ResourceType)Random.Range(0,3);

        
        reward_icon.color = Color.white;
        switch(currentReward) {
            case ResourceType.Argent:
                reward_icon.sprite = argent_icon;
                break;
            case ResourceType.Comestible:
                reward_icon.sprite = comestible_icon;
                break;
            case ResourceType.Energie:
                reward_icon.sprite = energie_icon;
                break;
            case ResourceType.Degats:
                reward_icon.sprite = degat_icon;
                break;
        }
        currentRewardValue = calculateReward();
        valueText.text = "+ " + currentRewardValue * 100f;
    }

    private float calculateReward() {
        float reward = 0.1f;
        return reward;
    }

    private void difficultyBloc(int defense) {
        StartCoroutine(difficultyBlocsAnim(0.5f, defense));
    }

    IEnumerator difficultyBlocsAnim(float duration, float defense)
    {
        float ratio = defense / 100f;
        float threshold = 0;
        foreach(Image i in blocDifficulty) {
            yield return new WaitForSeconds(duration/10f);
            i.color = new Color(0.1490196f, 0.1490196f, 0.1490196f, 1f );
        }
        for(int i = blocDifficulty.Length - 1; i >= 0; i--) {
            yield return new WaitForSeconds(duration/10f);
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

    IEnumerator lightDifficultyEndAnim(float success, float duration)
    {
       for(int i = blocDifficulty.Length - 1; i >= 0; i--) {
            blocDifficulty[i].color = difficultyRamp.Evaluate(success);
            yield return new WaitForSeconds(duration/4f);
        }
        
    }

    private void onInvadeEnd() {
        attackBar.SetActive(true);
        reward_icon.sprite = null;
        reward_icon.color = Color.black;
        valueText.text = "Vide";

        float succes = Random.Range(0, 100);
        float defense = current.defense > 80 ? 80 : current.defense;
        Debug.Log(succes + " " + defense);
        if( succes > current.defense) {
            invadeEnd.Invoke(currentReward, currentRewardValue);
        } else {
            invadeFailed.Invoke();
        }
        StartCoroutine(lightDifficultyEndAnim(0.9f, 1f));
    }

    IEnumerator waitInvade() {
        // yield return new WaitForSeconds(invadeTime);
        // invadeInProgress = false;
        Debug.Log("INVADE END");
        for(int i = blocInvade.Length - 1; i >= 0; i--) {
            yield return new WaitForSeconds(invadeTime/5f);
            blocInvade[i].color = new Color(0.1490196f, 0.1490196f, 0.1490196f, 1f );
        }
        onInvadeEnd();
    }

}
