using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour, ISelectable
{
    [SerializeField, Range(0, 1)] private float paleAmount = 0.4f;
    private Color originalColor;
    private MaterialPropertyBlock block;
    private Renderer selectableRenderer;
    private static readonly int ColorID = Shader.PropertyToID("_Color");

    void Awake()
    {
        // Saving original color
        selectableRenderer = GetComponent<Renderer>();
        if (selectableRenderer == null) return;

        block = new MaterialPropertyBlock();
        selectableRenderer.GetPropertyBlock(block);

        if (block.HasColor(ColorID))
            originalColor = block.GetColor(ColorID);
        else
            originalColor = selectableRenderer.sharedMaterial.GetColor(ColorID);
    }
    
    public void Highlight()
    {
        if (selectableRenderer == null) return;
        block.SetColor(ColorID, Color.Lerp(originalColor, Color.white, paleAmount));
        selectableRenderer.SetPropertyBlock(block);
    }

    public void ResetColor()
    {
        if (selectableRenderer == null) return;
        block.SetColor(ColorID, originalColor);
        selectableRenderer.SetPropertyBlock(block);
    }
}
