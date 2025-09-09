using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    
    public int PointValue;

    public static class Palette
    {
        public static readonly Color32 MidnightBlue = new Color32(27, 38, 59, 255);   // #1B263B
        public static readonly Color32 TealGreen = new Color32(42, 157, 143, 255); // #2A9D8F
        public static readonly Color32 SunsetOrange = new Color32(231, 111, 81, 255); // #E76F51
        public static readonly Color32 SandBeige = new Color32(244, 162, 97, 255); // #F4A261
    }

    void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (PointValue)
        {
            case 1:
                block.SetColor("_BaseColor", Palette.MidnightBlue);
                break;
            case 2:
                block.SetColor("_BaseColor", Palette.TealGreen);
                break;
            case 5:
                block.SetColor("_BaseColor", Palette.SunsetOrange);
                break;
            default:
                block.SetColor("_BaseColor", Palette.SandBeige);
                break;
        }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        onDestroyed.Invoke(PointValue);
        
        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.1f);
    }
}
