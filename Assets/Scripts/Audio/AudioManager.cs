using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource windSound;
    [SerializeField] private AudioSource gearsSound;
    [SerializeField] private AudioSource stormSound;
    [SerializeField] private AudioSource fogSound;
    [SerializeField] private AudioSource engineSound;
    [SerializeField] private AudioSource runSound;
    [SerializeField] private AudioSource childSound;
    [SerializeField] private AudioSource chainSound;
    [SerializeField] private AudioSource interactSound;
    [SerializeField] private AudioSource crushSound;
    [SerializeField] private AudioSource repairSound;
    [SerializeField] private AudioSource parrotSound;

    public static AudioManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void WindSoundPlay(bool state)
    {
        if (state)
        {
            windSound.Play();
        }
        else
        {
            windSound.Stop();
        }
    }

    public void GearsSoundPlay(bool state)
    {
        if (state)
        {
            gearsSound.Play();
        }
        else
        {
            gearsSound.Stop();
        }
    }

    public void StormSoundPlay(bool state)
    {
        if (state)
        {
            stormSound.Play();
        }
        else
        {
            stormSound.Stop();
        }
    }

    public void FogSoundPlay(bool state)
    {
        if (state)
        {
            fogSound.Play();
        }
        else
        {
            fogSound.Stop();
        }
    }

    public void EngineSoundPlay(bool state)
    {
        if (state)
        {
            engineSound.Play();
        }
        else
        {
            engineSound.Stop();
        }
    }

    public void RunSoundPlay(bool state)
    {
        if (state)
        {
            runSound.Play();
        }
        else
        {
            runSound.Stop();
        }
    }

    public void ChildSoundPlay() //once
    {
        childSound.Play();
    }

    public void ChainSoundPlay() //once
    {
        chainSound.Play();
    }

    public void InteractSoundPlay() //once
    {
        interactSound.Play();
    }

    public void CrushSoundPlay() //once
    {
        crushSound.Play();
    }

    public void RepairSoundPlay() //once
    {
        repairSound.Play();
    }

    public void ParrotSoundPlay() //once
    {
        parrotSound.Play();
    }

}
