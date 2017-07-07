using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayColorStream : MonoBehaviour {
    private Camera cam;
    public RenderTexture tex;
    public GameObject display;
    // Use this for initialization
    

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.targetTexture = tex;
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderTexture.active = tex;
        //cam.Render();
        Texture2D t = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        // Read pixels from screen into the saved texture data.
        t.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        // Actually apply all previous SetPixel and SetPixels changes.
        t.Apply();    // Unnecessary? YES Necessary!!!

        display.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = t;

        RenderTexture.active = null;
    }
}
