using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ApplyStream : MonoBehaviour {

    private Camera cam;
    public RenderTexture tex;
    public GameObject display;
    // Use this for initialization
    public Shader shader;
    [Range(0f, 3f)]
    public float depthLevel = 0.5f;


    private Material _material;
    private Material material
    {
        get
        {
            if (_material == null)
            {
                _material = new Material(shader);
                _material.hideFlags = HideFlags.HideAndDontSave;
            }
            return _material;
        }
    }



    void Start () {
        cam = GetComponent<Camera>();
        cam.targetTexture = tex;
        

        if (!SystemInfo.supportsImageEffects)
        {
            print("System doesn't support image effects");
            enabled = false;
            return;
        }
        if (shader == null || !shader.isSupported)
        {
            enabled = false;
            print("Shader " + shader.name + " is not supported");
            return;
        }

        // turn on depth rendering for the camera so that the shader can access it via _CameraDepthTexture
        cam.depthTextureMode = DepthTextureMode.Depth;
    }
	
	// Update is called once per frame
	void Update () {
        Matrix4x4 m = cam.projectionMatrix * cam.worldToCameraMatrix;
        material.SetFloat("_DepthLevel", depthLevel);
        material.SetMatrix("_WorldToClip", m);
        RenderTexture.active = tex;
        //cam.Render();
        Graphics.Blit(tex, tex, material);

        Texture2D t = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        // Read pixels from screen into the saved texture data.
        t.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        // Actually apply all previous SetPixel and SetPixels changes.
        t.Apply();    // Unnecessary? YES Necessary!!!

        display.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = t;

        RenderTexture.active = null;
    }
}
