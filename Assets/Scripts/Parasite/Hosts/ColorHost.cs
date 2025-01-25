using UnityEngine;

public class ColorHost : BaseHost
{
    [Header("Color Host Properties")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    public override void AttemptHostAction()
    {
        AssignRandomColor();
    }

    private void AssignRandomColor()
    {
        Color color = new(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );

        spriteRenderer.color = color;
    }
}
