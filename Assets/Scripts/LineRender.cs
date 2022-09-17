using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineRender : MonoBehaviour
{
    private Vector3 _startpos;
    private Vector3 _endpos;
    private LineRenderer _lr;

    private void Start()
    {
        _lr = GetComponent<LineRenderer>();
    }
 
    public void DrawLine(Vector3 startpos, Vector3 endpos )
    {
        _lr.enabled = true;
        _startpos = startpos;
        _startpos.y = 0;
        _lr.SetPosition(0, _startpos);
        _endpos = endpos;
        _endpos.y = 0;
        _lr.SetPosition(1, _endpos);
    }
 
    public void EndDrawLine()
    {
        _lr.enabled = false;

    }
    private void OnEnable()
    {
        InputController._speedCalc += DrawLine;
        InputController.apply += EndDrawLine;
    }

    private void OnDisable()
    {
        InputController._speedCalc -= DrawLine;
        InputController.apply -= EndDrawLine;
    }
}
