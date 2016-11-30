using UnityEngine;
using System.Collections;
// By @JavierBullrich

public class OffsetBackground : MonoBehaviour {
    //Material texture offset rate
    public float speed = .5f;
    Renderer bRenderer;

    private void Start()
    {
        bRenderer = GetComponent<Renderer>();
    }
    //Offset the material texture at a constant rate
    void Update()
    {
        float offset = Glitch.Manager.GameManagerBase.DeltaTime * speed;
        print(offset + " " + bRenderer.enabled + " offset: " + bRenderer.material.mainTextureOffset);
        //bRenderer.mainTextureOffset = new Vector2(-offset, offset);
        //bRenderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }

}
