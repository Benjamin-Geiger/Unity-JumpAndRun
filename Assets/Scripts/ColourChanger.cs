using UnityEngine;

public class ColourChanger : MonoBehaviour
{ 
    [SerializeField] private Color color;
    
    private MaterialPropertyBlock mpb;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        this.mpb = new MaterialPropertyBlock();
        this.meshRenderer = this.GetComponent<MeshRenderer>();
        this.meshRenderer.SetPropertyBlock(mpb);
    }

    private void Update()
    {
        this.mpb.SetColor("_BaseColor", color);
        this.meshRenderer.SetPropertyBlock(mpb);
    }
}
