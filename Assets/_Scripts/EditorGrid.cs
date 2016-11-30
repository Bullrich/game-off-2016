using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EditorGrid : MonoBehaviour {
	
	public float cell_size = 1f; // = larghezza/altezza delle celle
	private float x, y, z;
    [Tooltip("Defines if this function should be applyed or not")]
    public bool lockedOnGrid;
    SpriteRenderer spr;
    [Header("Sprite dimensions")]
    public float width;
    public float height;
	
	void Start() {
		x = 0f;
		y = 0f;
		z = 0f;
        spr = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
        if (!Application.isPlaying)
        {
            if(spr != null)
                spr = GetComponent<SpriteRenderer>();
            width = spr.sprite.textureRect.width / 100;
            height = spr.sprite.textureRect.height / 100;
            if (lockedOnGrid)
            {
                x = Mathf.Round(transform.position.x / cell_size) * cell_size;
                y = Mathf.Round(transform.position.y / cell_size) * cell_size;
                z = transform.position.z;
                transform.position = new Vector3(x, y, z);
            }
        }
	}
	
}