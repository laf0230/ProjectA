using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    
public class TestButton : Button
{
    [SerializeField] private Image playGraphic;
    [SerializeField] private Image pauseGraphic;

    private bool _isPlaying;
}
