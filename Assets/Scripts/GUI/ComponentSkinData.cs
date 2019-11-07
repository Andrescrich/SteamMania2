using UnityEngine;

[CreateAssetMenu(menuName = "UI/ComponentSkin", fileName = "UI/New Component Skin Data")]
public class ComponentSkinData : ScriptableObject
{
    public Color onHighlighted;
    public Color onPressed;
    public Color onSelected;
    public Color onDisabled;

    public Color defaultColor;
    
    public Color confirmColor;

    public Color declineColor;
    /*
    public Sprite sprite;
    public SpriteState spriteState;
    */

}