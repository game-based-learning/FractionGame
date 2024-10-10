using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class NumberLine : MonoBehaviour
{
    [SerializeField] private GameObject numberLineFillUnitPrefab;
    //Prefab could be a properly set up particle or just a square with a texture
    [SerializeField] private GameObject numberLineFillParent;
    [SerializeField] private GameObject fillLayerPrefab;

    [Space]
    [Tooltip("Position of the center of the fill unit in the bottom left corner")]
    [SerializeField] private Vector2 firstFillUnitPosition;
    [Tooltip("Y Value of the Position of the center of the fill unit in the top left corner")]
    [SerializeField] private float firstNegativeFillUnitYValue;

    [Space]
    [SerializeField] private float fillWidth;
    [SerializeField] private float maxFillHeight;

    [Space]
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
        set 
        { 
            lineRangeMin = value;
            //DrawTickMarks();
        }
    }

    public float LineRangeMax
    {
        get { return lineRangeMax; }
        set
        {
            lineRangeMax = value;
            //DrawTickMarks();
        }
    }

    public int NumberOfLargeTicks
    {
        get { return numLargeTicks; }
        set { numLargeTicks = value; }
    }

    public int NumSmallTicksBetweenLargeTicks
    {
        get { return numSmallTicksBetweenLargeTicks; }
        set { numSmallTicksBetweenLargeTicks = value; }
    }

    /// <summary>
    /// ( y position, parent object holding the fill unit objects for that row )
    /// </summary>
    public Dictionary<float, GameObject> FillUnits { get { return fillUnitsCurrentlyDisplayed; } }

    /// <summary>
    /// ( y position, parent object holding the fill unit objects for that row )
    /// </summary>
    private Dictionary<float, GameObject> fillUnitsCurrentlyDisplayed = new Dictionary<float, GameObject>();
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

    /// <summary>
    /// Draws the Number Line
    /// </summary>
    /// <param name="currentValue">value to display</param>
    /// <param name="negative">true if should use the negative form of the display</param>
    public void DisplayInfo(float currentValue, bool negative = false)
    {
        if (!GeneralUtility.IsInRange(currentValue, this.LineRangeMin, this.LineRangeMax))
        {
            Debug.Log("The current value ( " + currentValue + " ) is not within the Number Line Range from " + this.LineRangeMin + " to " + this.LineRangeMax);
            return;
        }

        float percentToFill = GeneralUtility.PercentOfRange(currentValue, this.LineRangeMin, this.LineRangeMax);
        float startingYPos;
        if (negative)
        {
            startingYPos = this.firstNegativeFillUnitYValue;
        }
        else
        {
            startingYPos = this.firstFillUnitPosition.y;
        }

        DrawTickMarks();
        DestroyAllNumberLineFill();

        DrawNumberLineFill(percentToFill, startingYPos, negative);
    }

    /// <summary>
    /// Use this to destroy and redraw fill units at the Y Positions saved in the Dictionary
    /// </summary>
    public void RefreshInfo()
    {
        foreach (KeyValuePair<float, GameObject> fillRow in this.fillUnitsCurrentlyDisplayed)
        {
            //Reset row object position
            fillRow.Value.transform.position = new Vector2(firstFillUnitPosition.x, fillRow.Key);

            DestroyNumberLineFillLayer(fillRow.Value);
            DrawNumberLineFillLayer(fillRow.Value, fillRow.Key);
        }
    }

    private void DrawNumberLineFill(float percentToFill, float startingYPos, bool negative = false)
    {
        this.numFillUnitsHeight = (int) ( percentToFill * this.maxFillHeight / this.fillUnitHeight );

        //Debug.Log("numFillUnitsWidth: " + this.numFillUnitsWidth + ", numFillUnitsHeight: " + this.numFillUnitsHeight);

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
        GameObject layerFillUnitObjects = Instantiate(this.fillLayerPrefab,
                new Vector2(xPos, yPos), Quaternion.identity, this.numberLineFillParent.transform);

        DrawNumberLineFillLayer(layerFillUnitObjects, yPos);

        this.fillUnitsCurrentlyDisplayed[yPos] = layerFillUnitObjects;
    }

    //unlike the method this overloads, does not create layerFillUnitObjects or add it to the dictionary
    private void DrawNumberLineFillLayer(GameObject layerFillUnitObjects, float yPos)
    {
        float xPos = this.firstFillUnitPosition.x;

        for (int i = 0; i < this.numFillUnitsWidth; i++)
        {
            GameObject newFillUnitObject = Instantiate(this.numberLineFillUnitPrefab,
                new Vector2(xPos, yPos), Quaternion.identity, layerFillUnitObjects.transform);

            //Debug.Log("Drew at Pos: (" + xPos + ", " + yPos + ")");

            xPos += this.xDisplacementForNewFill;
        }
    }

    private void DestroyAllNumberLineFill()
    {
        float[] keys = this.fillUnitsCurrentlyDisplayed.Keys.ToArray();

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
        if (!this.fillUnitsCurrentlyDisplayed.Remove(yPos, out GameObject fillUnitsAtYPos))
        {
            Debug.LogError("No fill units currently at given Y Position: " + yPos);
            return;
        }

        //DestroyNumberLineFillLayer(fillUnitsAtYPos);

        Destroy(fillUnitsAtYPos);
    }

    //unlike the method this overloads, does not destroy fillUnitsAtYPos or remove it from the dictionary
    private void DestroyNumberLineFillLayer(GameObject fillUnitsAtYPos)
    {
        for (int i = 0; i < fillUnitsAtYPos.transform.childCount; i++)
        {
            GameObject fillUnit = fillUnitsAtYPos.transform.GetChild(i).gameObject;
            Destroy(fillUnit);
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
        numberText.text = LargeTickValueRepresentation(largeTickValue);
    }

    private string LargeTickValueRepresentation(float largeTickValue)
    {
        return largeTickValue.ToString();
        //Replace this later with system to determine what representation of the number to use
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

    /// <summary>
    /// Adds the fill units of the given NumberLine to the dictionary in this
    /// </summary>
    public void Add(NumberLine otherLine, bool negative = false)
    {
        float yPos = this.currentYFillPosition;
        foreach (KeyValuePair<float, GameObject> row in otherLine.FillUnits)
        {
            if (!negative)
            {
                yPos = this.currentYFillPosition + row.Key;
            }
            else
            {
                yPos = this.currentYFillPosition - row.Key;
            }

            this.fillUnitsCurrentlyDisplayed[yPos] = row.Value;
        }

        this.currentYFillPosition = yPos;
    }
}
