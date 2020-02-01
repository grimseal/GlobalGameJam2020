using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class ChildController : MonoBehaviour
{
    [Header("Sub - substuct (otnyat')"), SerializeField] private float subTime = 1f;
    [SerializeField] private float subValue = 10f;
    [SerializeField] private float problemPregress = 100f;
    [SerializeField] private float problemLimit = 30f;
    [Header("Part settings"), SerializeField]
    public int partIncrementValue = 10;
    [Header("Timer settings"), SerializeField] private int timerTime = 30;

    [SerializeField] private bool hasPlayer = false;

    public IChildAction childAction;
    private Coroutine timerIenumerator = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SubstuctProgress());
        childAction = GetComponent(typeof(IChildAction)) as IChildAction;
        Debug.LogError(string.Format("Child interface null is {0}", childAction == null));
    }

    // Update is called once per frame
    void Update()
    {
        // if (hasPlayer && Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //     Debug.Log("Up key pressed");
        //     childAction.ChangeProblemValue(partIncrementValue);
        // }
        // if (hasPlayer && Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //     Debug.Log("Down key pressed");
        //     childAction.ChangeProblemValue(-partIncrementValue);
        // }
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
                Debug.Log(string.Format("{0} has {1}%, set animation to Cry", gameObject.name, problemLimit));
                //Alert
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

    private IEnumerator TimerToGameOver()
    {
        Debug.Log("Start timer to game over");
        yield return new WaitForSeconds(timerTime);
        GameOver();
    }

    private void GameOver()
    {
        Debug.Log("Game over");
        Time.timeScale = 0.00000000001f;
    }

    public void AddValueToProblem(int value)
    {
        Debug.Log($"child value change {value}");
        ProblemPregress += value;
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
    public float ProblemPregress { get => problemPregress; set => problemPregress = value; }
}
