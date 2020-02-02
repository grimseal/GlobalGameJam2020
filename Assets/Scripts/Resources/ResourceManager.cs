using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager Instance { get; set; }

    [SerializeField] private float resourceCount = 0;

    public float ResourceCount { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddResources(int count)
    {
        ResourceCount += count;
        Debug.Log("Your resource is " + ResourceCount);
    }

    public void TakeOffResources(int count)
    {
        ResourceCount -= count;
        if (ResourceCount <= 0)
        {
            ResourceCount = 0;
            Debug.LogError("There is not resources in your bag");
            return;
        }
        Debug.Log("Your resource is " + ResourceCount);
    }
}
