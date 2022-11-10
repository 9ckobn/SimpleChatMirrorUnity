using UnityEngine;

[CreateAssetMenu(fileName = "Ability Config")]
public class AbilityConfig : ScriptableObject
{
    public AbilityType type;
    public float dashDistance;
    public float CoolDown;

    [Min(0.01f)]
    public float CastDuration = 0.5f;
}
