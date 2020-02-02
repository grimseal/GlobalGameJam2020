using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class StormWeather : MonoBehaviour, IWeather
{

    [SerializeField] private ChildController wheelChild;
    [SerializeField] private ChildController rotorChild;
    [SerializeField] private PostProcessVolume postProcessVolume;
    
    private ShipController shipController;

    public SpriteRenderer lightning;

    private Coroutine lightningCoroutine;

    public void EndWeather()
    {
        AudioManager.Instance.StormSoundPlay(false);
        Debug.Log(this.name + " is end");
        if ((wheelChild.ProblemPregress >= 0 && wheelChild.ProblemPregress <= 30)
            && rotorChild.ProblemPregress >= 30 && rotorChild.ProblemPregress <= 60)
        {
            Debug.Log("All is good");
        }
        else
        {
            Debug.Log("Ship take a 1 damage");
            shipController.SubHPPoint();
        }
        StartCoroutine(ChangeValue(1, 0.6f, 0, false));
        StopCoroutine(lightningCoroutine);
        lightning.enabled = false;
    }

    private IEnumerator ChangeValue(float time, float from, float to, bool state)
    {
        if (state)
        {
            postProcessVolume.gameObject.SetActive(true);
        }
        Vignette vignette;
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            var startTime = Time.time;
            var finishTime = Time.time + time;
            while (Time.time < finishTime)
            {
                var t = 1f / time * (Time.time - startTime);
                var l = Mathf.Lerp(from, to, t);
                vignette.intensity.value = l;
                yield return null;
            }
            vignette.intensity.value = to;
        }
        if (!state)
        {
            postProcessVolume.gameObject.SetActive(false);
        }
    }
    
    public void StartWeather()
    {
        Debug.Log(this.name + " is start");
        AudioManager.Instance.StormSoundPlay(true);
        StartCoroutine(ChangeValue(1, 0, 0.6f, true));
        lightning.enabled = false;
        lightningCoroutine = StartCoroutine(LightningCoroutine());
    }


    IEnumerator LightningCoroutine()
    {
        while (true)
        {
            var current = Time.time;
            var s = Mathf.Sin(current);
            lightning.enabled = Mathf.Abs(s) < 0.05f;
            yield return null;
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        shipController = FindObjectOfType<ShipController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
