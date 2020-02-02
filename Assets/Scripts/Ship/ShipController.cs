using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShipController : MonoBehaviour
{

    [SerializeField] private Sprite[] shipSprites;

    public static ShipController Instance { get; set; }
    [SerializeField] private int damageCount = 5;

    [SerializeField] private SpriteRenderer spriteRenderer;

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
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddHPPoint()
    {
        AudioManager.Instance.RepairSoundPlay();
        damageCount++;
        UpdateShipState();
    }

    public void SubHPPoint()
    {
        AudioManager.Instance.CrushSoundPlay();
        damageCount--;
        UpdateShipState();
    }

    private void UpdateShipState()
    {
        if (shipSprites.Length > 0)
        {
            spriteRenderer.sprite = shipSprites[damageCount - 1];
        }
        else
        {
            Debug.LogError("There is no sprites");
        }
    }
}
