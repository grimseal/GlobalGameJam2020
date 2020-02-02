using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager Instance { get; set; }

    [SerializeField] private GameObject[] packages;
    [SerializeField] private int resourceCount = 2;

    public int ResourceCount { get { return resourceCount; } set { resourceCount = value; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < resourceCount; i++)
        {
            packages[i].SetActive(true);
        }
    }

    private void Update()
    {
        
    }

    public void AddResources()
    {
        ResourceCount += 1;
        if (ResourceCount > 5)
        {
            ResourceCount = 5;
        }
        packages[ResourceCount - 1].SetActive(true);
        Debug.Log("Your resource is " + ResourceCount);
    }

    public void TakeOffResources()
    {
        ResourceCount -= 1;
        if (ResourceCount <= 0)
        {
            ResourceCount = 0;
        }
        packages[ResourceCount].SetActive(false);
        Debug.Log("Your resource is " + ResourceCount);
    }
}
