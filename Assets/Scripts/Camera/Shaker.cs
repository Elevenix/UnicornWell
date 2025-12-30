using UnityEngine;

[CreateAssetMenu(fileName ="Shaker")]
public class Shaker : ScriptableObject
{
    public float duration = .1f;
    public float magnitude = .1f;
    public float smoothTime = .05f;
    public Vector2 direction = Vector2.one;
}
