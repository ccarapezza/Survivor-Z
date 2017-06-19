using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorSelector : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode;

    // Use this for initialization
    void Start () {
        //cursorTexture.Resize(cursorTexture.width*2, cursorTexture.height*2, cursorTexture.format, cursorTexture.mipmapCount>0);

        Vector2 cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, cursorMode);
    }
}
