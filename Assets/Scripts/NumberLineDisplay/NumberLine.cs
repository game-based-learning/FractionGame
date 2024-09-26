using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLine : MonoBehaviour
{
    [SerializeField] private GameObject numberLineFillUnitPrefab;
    //Prefab could be a properly set up particle or just a square with a texture
    [SerializeField] private GameObject numberLineFillParent;
    
    [Tooltip("Position of the center of fill unit in the bottom left corner")]
    [SerializeField] private Vector2 firstFillUnitPosition;
    
    [SerializeField] private float fillWidth;
    [SerializeField] private float maxFillHeight;

    private float fillUnitWidth;
    private float fillUnitHeight;

    private float xDisplacementForNewFill;
    private float yDisplacementForNewFill;
    private int numFillUnitsWidth;
    private int numFillUnitsHeight = 0;

    /// <summary>
    /// ( y position, ( x position, numberLineFillUnit instantiated object) )
    /// </summary>
    private Dictionary<float, Dictionary<float, GameObject>> fillUnitsCurrentlyDisplayed;
    /// <summary>
    /// The Y Position of the instantiated fill object furthest from the firstFillUnitPosition
    /// </summary>
    private float currentYFillPosition;

    //Need to switch to getting info from gameObject! Sprite is giving pixels (i think)

    void Awake()
    {
        SpriteRenderer numberLineFillUnitSpriteRenderer = this.numberLineFillUnitPrefab.GetComponentInChildren<SpriteRenderer>();
        if (numberLineFillUnitSpriteRenderer == null)
        {
            Debug.LogError("numberLineFillUnitPrefab should have a Sprite child");
        }
        this.fillUnitWidth = numberLineFillUnitSpriteRenderer.bounds.size.x;
        this.fillUnitHeight = numberLineFillUnitSpriteRenderer.bounds.size.y;

        this.xDisplacementForNewFill = this.fillUnitWidth;
        this.yDisplacementForNewFill = this.fillUnitHeight;

        this.numFillUnitsWidth = (int) (this.fillWidth / this.fillUnitWidth);

        this.fillUnitsCurrentlyDisplayed = new Dictionary<float, Dictionary<float, GameObject>>();
    }

    private void Start()
    {

        //GameObject newFillUnitObject = Instantiate(this.numberLineFillUnitPrefab,
                //new Vector2(this.firstFillUnitPosition.x, this.firstFillUnitPosition.y), Quaternion.identity);
        DrawNumberLineFill(1.0f, this.firstFillUnitPosition.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawNumberLineFill(float percentToFill, float startingYPos, bool negative = false)
    {
        this.numFillUnitsHeight = (int) ( percentToFill * this.maxFillHeight / this.fillUnitHeight );

        Debug.Log("numFillUnitsWidth: " + this.numFillUnitsWidth + ", numFillUnitsHeight: " + this.numFillUnitsHeight);

        float yPos = startingYPos;
        for (int i = 0; i < this.numFillUnitsHeight; i++)
        {
            DrawNumberLineFillLayer(yPos);
            this.currentYFillPosition = yPos;

            if (negative)
            {
                yPos -= this.yDisplacementForNewFill;
            }
            else
            {
                yPos += this.yDisplacementForNewFill;
            }
        }
    }

    private void DrawNumberLineFillLayer(float yPos)
    {
        float xPos = this.firstFillUnitPosition.x;
        Dictionary<float, GameObject> layerFillUnitObjects = new Dictionary<float, GameObject>();

        for (int i = 0; i < this.numFillUnitsWidth; i++)
        {
            GameObject newFillUnitObject = Instantiate(this.numberLineFillUnitPrefab,
                new Vector2(xPos, yPos), Quaternion.identity, this.numberLineFillParent.transform);

            layerFillUnitObjects[xPos] = newFillUnitObject;

            Debug.Log("Drew at Pos: (" + xPos + ", " + yPos + ")");

            xPos += this.xDisplacementForNewFill;
        }

        this.fillUnitsCurrentlyDisplayed[yPos] = layerFillUnitObjects;
    }
}
