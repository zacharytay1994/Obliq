using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursor_fill_;
    [SerializeField] Texture2D cursor_empty_;
    Vector3 mouse_pos_;
    GameObject cursor_fill_object_;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mouse_pos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //cursor_fill_object_.transform.position = mouse_pos_;
        //ShowCursorFill();
    }

    public void ShowCursorFill()
    {
        Cursor.SetCursor(cursor_fill_, Vector2.zero, CursorMode.Auto);
    }

    public void HideCursorFill()
    {
        Cursor.SetCursor(cursor_empty_, Vector2.zero, CursorMode.Auto);
    }
}
