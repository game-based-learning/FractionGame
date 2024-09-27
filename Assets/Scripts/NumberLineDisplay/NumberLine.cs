using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberLine : MonoBehaviour
{
    [SerializeField] private GameObject numberLineFillUnitPrefab;
    //Prefab could be a properly set up particle or just a square with a texture
    [SerializeField] private GameObject numberLineFillParent;
    
    [Tooltip("Position of the center of the fill unit in the bottom left corner (if positive) or top left corner (if negative)")]
    [SerializeField] private Vector2 firstFillUnitPosition;
    
    [SerializeField] private float fillWidth;
    [SerializeField] private float maxFillHeight;

    [SerializeField] private GameObject largeTickPrefab;
    [SerializeField] private GameObject smallTickPrefab;
    [SerializeField] private GameObject tickMarksParent;
    [SerializeField] private Vector2 originTickPosition;
    [SerializeField] private float maxTickYPosition;
    [SerializeField] private int numLargeTicks;
    [SerializeField] private int numSmallTicksBetweenLargeTicks;
    [SerializeField] private float lineRangeMin = 0.0f;
    [SerializeField] private float lineRangeMax = 1.0f;

    private float fillUnitWidth;
    private float fillUnitHeight;

    private float xDisplacementForNewFill;
    private float yDisplacementForNewFill;
    private int numFillUnitsWidth;
    private int numFillUnitsHeight = 0;   

    public float LineRangeMin
    {
        get { return lineRangeMin; }
        set { 
                lineRangeMin = value;
                DrawTickMarks();
            }
    }

    public float LineRangeMax
    {
        get { return lineRangeMax; }
        set
        {
            lineRangeMax = value;
            DrawTickMarks();
        }
    }

    /// <summary>
    /// Position of the center of the fill unit in the bottom left corner (if positive) or top left corner (if negative)
    /// </summary>
    public Vector2 FirstFillUnitPosition { get; set; }

    /// <summary>
    /// ( y position, ( x position, numberLineFillUnit instantiated object) )
    /// </summary>
    private Dictionary<float, Dictionary<float, GameObject>> fillUnitsCurrentlyDisplayed = new Dictionary<float, Dictionary<float, GameObject>>();
    /// <summary>
    /// The Y Position of the instantiated fill object furthest from the firstFillUnitPosition
    /// </summary>
    private float currentYFillPosition; //useful to have for some of my ideas for animating operations later

    private List<GameObject> tickMarkObjects = new List<GameObject>();

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
    }

    private void Start()
    {

        //GameObject newFillUnitObject = Instantiate(this.numberLineFillUnitPrefab,
        //new Vector2(this.firstFillUnitPosition.x, this.firstFillUnitPosition.y), Quaternion.identity);
        DrawTickMarks();
        DrawNumberLineFill(0.75f, this.firstFillUnitPosition.y);

    }

    float time = 0.0f;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 5)
        {
            DestroyAllNumberLineFill();
        }
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

    private void DestroyAllNumberLineFill()
    {
        //Create a copy of the keys to iterate over (since you cannot remove from a Dictionary you are iterating over)
        float[] keys = new float[this.fillUnitsCurrentlyDisplayed.Count];
        int i = 0;
        foreach (float key in this.fillUnitsCurrentlyDisplayed.Keys)
        {
            keys[i] = key;
            i++;
        }

        foreach (float yPos in keys)
        {
            DestroyNumberLineFillLayer(yPos);
        }

        if (this.fillUnitsCurrentlyDisplayed.Count != 0)
        {
            Debug.LogError("Did not successfully destroy all Number Line Fill Units");
        }
    }

    //May use this later outside of DestroyAllNumberLineFill for animations
    private void DestroyNumberLineFillLayer(float yPos)
    {
        if (!this.fillUnitsCurrentlyDisplayed.Remove(yPos, out Dictionary<float, GameObject> fillUnitsAtYPos))
        {
            Debug.LogError("No fill units currently at given Y Position: " + yPos);
            return;
        }

        foreach (KeyValuePair<float, GameObject> fillUnitPair in fillUnitsAtYPos)
        {
            Destroy(fillUnitPair.Value);
        }
    }

    private void DrawTickMarks()
    {
        DestroyPreviousTickMarks();

        float distanceBetweenLargeTickMarks
            = (this.maxTickYPosition - this.originTickPosition.y) / (this.numLargeTicks - 1);

        float distanceBetweenSmallTickMarks
            = distanceBetweenLargeTickMarks / (this.numSmallTicksBetweenLargeTicks + 1);

        float numericalDifferenceBetweenLargeTicks
            = (this.LineRangeMax - this.LineRangeMin) / (this.numLargeTicks - 1);

        float largeTickValue = this.LineRangeMin;
        float largeTickYPosition = this.originTickPosition.y;
        CreateLargeTick(largeTickValue, largeTickYPosition); //Min/origin tick mark

        for (int i = 1; i < this.numLargeTicks; i++)
        {
            largeTickValue += numericalDifferenceBetweenLargeTicks;
            largeTickYPosition += distanceBetweenLargeTickMarks;

            DrawIntermediateTickMarks(largeTickValue, largeTickYPosition, distanceBetweenSmallTickMarks);
        }
    }

    private void CreateLargeTick(float largeTickValue, float largeTickYPosition)
    {
        GameObject newLargeTickObject = Instantiate(this.largeTickPrefab,
                new Vector2(this.originTickPosition.x, largeTickYPosition), Quaternion.identity, this.tickMarksParent.transform);
        this.tickMarkObjects.Add(newLargeTickObject);

        TMP_Text numberText = newLargeTickObject.GetComponentInChildren<TMP_Text>();
        if (numberText == null)
        {
            Debug.LogError("largeTickPrefab should have a TMP_Text child");
        }
        numberText.text = largeTickValue.ToString();
    }

    private void DrawIntermediateTickMarks(float largeTickValue, float largeTickYPosition, float distanceBetweenSmallTickMarks)
    {
        CreateLargeTick(largeTickValue, largeTickYPosition);

        float smallTickYPosition = largeTickYPosition;
        for (int i = 0; i < this.numSmallTicksBetweenLargeTicks; i++)
        {
            smallTickYPosition -= distanceBetweenSmallTickMarks;

            GameObject newSmallTickObject = Instantiate(this.smallTickPrefab,
                new Vector2(this.originTickPosition.x, smallTickYPosition), Quaternion.identity, this.tickMarksParent.transform);
            this.tickMarkObjects.Add(newSmallTickObject);
        }
    }

    private void DestroyPreviousTickMarks()
    {
        while (this.tickMarkObjects.Count > 0)
        {
            int index = this.tickMarkObjects.Count - 1;
            GameObject gameObject = this.tickMarkObjects[index];
            this.tickMarkObjects.RemoveAt(index);
            Destroy(gameObject);
        }
    }
}
