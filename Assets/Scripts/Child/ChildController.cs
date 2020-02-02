using System;
using System.Collections;
using System.Collections.Generic;
using InteractiveObject;
using UnityEngine;
using Object = System.Object;

[RequireComponent(typeof(Collider2D))]
public class ChildController : MonoBehaviour
{
    [Header("Sub - substuct (otnyat')"), SerializeField] private float subTime = 1f;
    [SerializeField] private float subValue = 10f;
    [SerializeField] private float problemPregress = 100f;
    [SerializeField] private float problemLimit = 30f;
    [Header("Part settings"), SerializeField] public int partIncrementValue = 10;
    [Header("Timer settings"), SerializeField] private int timerTime = 30;
    [SerializeField] private GameObject alertObject;

    [SerializeField] private bool hasPlayer = false;

    public IChildAction childAction;
    private Coroutine timerIenumerator = null, alertCoroutine = null;
    private bool isProblemActive = false;

    public ObjectArrow indicator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SubstuctProgress());
        childAction = GetComponent(typeof(IChildAction)) as IChildAction;
        Debug.LogError(string.Format("Child interface null is {0}", childAction == null));
        indicator.SetValue(problemPregress);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SubstuctProgress()
    {
        yield return new WaitForSeconds(subTime);
        problemPregress -= subValue;
        if (problemPregress > 0)
        {
            if (timerIenumerator != null)
            {
                StopCoroutine(timerIenumerator);
                timerIenumerator = null;
            }
            if (problemPregress <= problemLimit)
            {
                //Alert
                Debug.Log(string.Format("{0} has {1}%, set animation to Cry", gameObject.name, problemLimit));
                if (!isProblemActive)
                {
                    AudioManager.Instance.ChildSoundPlay();
                    alertCoroutine = StartCoroutine(Alert());
                }
                isProblemActive = true;
            }
            else
            {
                if (alertCoroutine != null)
                {
                    StopCoroutine(alertCoroutine);
                    alertObject.SetActive(false);
                    alertCoroutine = null;
                    isProblemActive = false;
                }
            }
        }
        if (problemPregress <= 0)
        {
            problemPregress = 0;
            //Timer to gameover
            if (timerIenumerator == null)
                timerIenumerator = StartCoroutine(TimerToGameOver());
        }
        StartCoroutine(SubstuctProgress());
    }

    private IEnumerator Alert()
    {
        alertObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        alertObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        alertCoroutine = StartCoroutine(Alert());
    }

    private IEnumerator TimerToGameOver()
    {
        Debug.Log("Start timer to game over");
        yield return new WaitForSeconds(timerTime);
        GameController.Instance.GameOver();
    }

    public void AddValueToProblem(int value)
    {
        Debug.Log($"child value change {value}");
        ProblemPregress += value;
        if (ProblemPregress > 100)
        {
            ProblemPregress = 100;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //UI show
            hasPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hasPlayer = false;
            //UI close
        }
    }

    public float SubTime { get => subTime; set => subTime = value; }
    public float SubValue { get => subValue; set => subValue = value; }
    public float ProblemPregress
    {
        get => problemPregress;
        set
        {
            problemPregress = value;
            indicator.SetValue(value);
        }
    }
}
