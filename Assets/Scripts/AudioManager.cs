using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //variables
    public AudioSource sourceAlarm, sourceAmbiance, sourceInteraction, sourceWheel;
    public AudioClip alarm, ambianceReality, ambianceFantasy, ambianceInvade, failed, gameover, invade, powerOn, powerOff, spaceJump, trade, wheel;

    //PUBLIC
    public void Play_alarm()
    {
        sourceAlarm.Play();
    }
    public void Stop_alarm()
    {
        sourceAlarm.Stop();
    }
    public void Play_ambianceReality()
    {
        AudioFadeOut(sourceAmbiance, 0.5f);
        sourceAmbiance.clip = ambianceReality;
        sourceAmbiance.Play();
    }
    public void Play_ambianceFantasy()
    {
        AudioFadeOut(sourceAmbiance, 0.5f);
        sourceAmbiance.clip = ambianceFantasy;
        sourceAmbiance.Play();
    }
    public void Play_ambianceInvade()
    {
        AudioFadeOut(sourceAmbiance, 0.5f);
        sourceAmbiance.clip = ambianceInvade;
        sourceAmbiance.Play();
    }
    public void Play_failed()
    {
        sourceInteraction.clip = failed;
        sourceInteraction.Play();
    }
    public void Play_gameOver()
    {
        sourceInteraction.clip = gameover;
        sourceInteraction.Play();
    }
    public void Play_invade()
    {
        sourceInteraction.clip = invade;
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
        Play_ambianceReality();

        sourceInteraction.loop = false;

        sourceWheel.clip = wheel;
        sourceWheel.loop = true;
        sourceWheel.Play();
        sourceWheel.Pause();
    }

    void Update()
    {
        
    }

    private void AudioFadeOut (AudioSource source, float delay)
    {
        IEnumerator coroutine = FadingOut(source, delay);
        StartCoroutine(coroutine);
    }

    private IEnumerator FadingOut(AudioSource source, float delay)
    {
        float fac = 1f / delay;
        while(delay >= 0f)
        {
            source.volume -= fac * Time.deltaTime;
            delay -= Time.deltaTime;

            yield return null;
        }
    }

}
