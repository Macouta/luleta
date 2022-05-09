using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AudioManager : MonoBehaviour
{
    //variables
    [BoxGroup("Audio Srouces")]
    public AudioSource sourceAlarm, sourceAmbiance, sourceInteraction, sourceWheel;
    [BoxGroup("Audio Clips")]
    public AudioClip alarm, ambianceReality, ambianceFantasy, failed, gameover, invade, powerOn, powerOff, spaceJump, trade, wheel, click;

    //PUBLIC
    public void Play_alarm()
    {
        if (!sourceAlarm.isPlaying)
            sourceAlarm.Play();
    }
    public void Stop_alarm()
    {
        sourceAlarm.Stop();
    }
    public void Play_ambianceReality()
    {
        AudioFade(sourceAmbiance, ambianceReality);
    }
    public void Play_ambianceFantasy()
    {
        AudioFade(sourceAmbiance, ambianceFantasy);
    }
    public void Play_failed()
    {
        sourceInteraction.clip = failed;
        sourceInteraction.Play();
    }
    public void Play_gameOver()
    {
        sourceAlarm.clip = gameover;
        sourceAlarm.Play();
    }
    public void Play_invade()
    {
        sourceInteraction.clip = invade;
        sourceInteraction.Play();
    }

    public void Play_invadeEnd() {
        sourceInteraction.clip = click;
        sourceInteraction.Play();
    }

    public void Play_powerOn()
    {
        sourceInteraction.clip = powerOn;
        sourceInteraction.Play();
    }
    public void Play_powerOff()
    {
        sourceInteraction.clip = powerOff;
        sourceInteraction.Play();
    }
    public void Play_spaceJump()
    {
        sourceInteraction.clip = spaceJump;
        sourceInteraction.Play();
    }
    public void Play_trade()
    {
        sourceInteraction.clip = trade;
        sourceInteraction.Play();
    }
    public void Play_wheel(bool on)
    {
        if (on)
            sourceWheel.UnPause();
        else
            sourceWheel.Pause();
    }
    public void SetWheelSpeed(float speed)
    {
        float aspeed = Mathf.Abs(speed);
        sourceWheel.volume = Mathf.Clamp(aspeed, 0f, 2f);
        sourceWheel.pitch = Mathf.Clamp(aspeed, 1f, 3f);
    }

    //PRIVATE
    void Start()
    {
        //configuration audio source
        sourceAlarm.clip = alarm;
        sourceAlarm.loop = true;

        sourceAmbiance.loop = true;
        sourceAmbiance.clip = ambianceReality;
        sourceAmbiance.Play();

        sourceInteraction.loop = false;

        sourceWheel.clip = wheel;
        sourceWheel.loop = true;
        sourceWheel.Play();
        sourceWheel.Pause();
    }

    void Update()
    {
        
    }

    private void AudioFade(AudioSource source, AudioClip newClip)
    {
        IEnumerator coroutine = Fading(source, newClip);
        StartCoroutine(coroutine);
    }

    private IEnumerator Fading(AudioSource source, AudioClip newClip)
    {
        //fade out
        float delay = 0.5f;
        float fac = 1f / delay;
        while(delay >= 0f)
        {
            source.volume -= fac * Time.deltaTime;
            delay -= Time.deltaTime;

            yield return null;
        }
        //nouveau clip
        source.volume = 1f;
        source.clip = newClip;
        source.Play();
    }
}
