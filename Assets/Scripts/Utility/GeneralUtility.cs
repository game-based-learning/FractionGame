using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class GeneralUtility
{
    public static bool IsInRange(float value, float min, float max)
    {
        return value >= min && value <= max;
        //Might consider updating this later if needed to handle the floating point comparison in a safer way
    }

    /// <summary>
    /// Returns the percent of the range (from min to max) the amount from the min to the value is
    /// (where 1% is represented by 0.01f)
    /// </summary>
    public static float PercentOfRange(float value, float min, float max)
    {
        float range = max - min;
        float amount = value - min;
        return amount / range;
    }
}
