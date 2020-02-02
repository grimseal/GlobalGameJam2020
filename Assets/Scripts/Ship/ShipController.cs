using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(SpriteRenderer))]
public class ShipController : MonoBehaviour
{

    // [SerializeField] private Sprite[] shipSprites;
    [SerializeField] private GameObject[] hullProgress;
    
    public static ShipController Instance { get; set; }
    [SerializeField] private int damageCount = 5;

    // [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateShipState();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddHPPoint()
    {
        hullProgress[damageCount].gameObject.SetActive(false);
        AudioManager.Instance.RepairSoundPlay();
        damageCount++;
        UpdateShipState();
    }

    public void SubHPPoint()
    {
        hullProgress[damageCount].gameObject.SetActive(false);
        AudioManager.Instance.CrushSoundPlay();
        damageCount--;
        UpdateShipState();
    }

    private void UpdateShipState()
    {
        if (hullProgress.Length > 0)
        {
            hullProgress[damageCount - 1].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("There is no sprites");
        }
    }
}
