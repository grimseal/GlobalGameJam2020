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

    public Collider2D RotorCollider;
    public Collider2D WheelColider;
    
    public SpriteRenderer RotorArrowSprite;
    public SpriteRenderer WheelArrowSprite;

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
        hullProgress[damageCount - 1].gameObject.SetActive(false);
        AudioManager.Instance.RepairSoundPlay();
        damageCount++;
        if (damageCount >= 5)
        {
            GameController.Instance.Win();
        }

        UpdateShipState();
        ResourceManager.Instance.TakeOffResources();
    }

    public void SubHPPoint()
    {
        hullProgress[damageCount].gameObject.SetActive(false);
        AudioManager.Instance.CrushSoundPlay();
        damageCount--;
        if (damageCount <= 0)
        {
            GameController.Instance.GameOver();
        }
        UpdateShipState();
    }

    private void UpdateShipState()
    {
        RotorCollider.enabled = damageCount > 1;
        WheelColider.enabled = damageCount > 2;
        // RotorArrowSprite.enabled = damageCount > 1;
        // WheelArrowSprite.enabled = damageCount > 2;
        
        if (hullProgress.Length > 0 && damageCount - 1 >= 0)
        {
            hullProgress[damageCount - 1].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("There is no sprites");
        }
    }
}
