using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLineTest1 : MonoBehaviour
{
    [SerializeField] private EquationManager equationManager;

    // Start is called before the first frame update
    void Start()
    {
        equationManager.CheckEquation(1, 4, 1, 4);
    }
}