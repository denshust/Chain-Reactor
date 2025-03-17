using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TImeControl : MonoBehaviour
{
    [Range(0,3)]
    public float scale;
  
    void Update()
    {
        Time.timeScale = scale;
    }
}
