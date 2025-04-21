using UnityEngine;

public class ElementalAnimationOffset : MonoBehaviour
{
    public enum ElementType { Fire, Water, Nature }
    public ElementType element;

    public string stateName = "HoverSpin";

    private Animator animator;

void Start()
{
    animator = GetComponent<Animator>();

    float offset = GetOffsetForElement(element) + Random.Range(-0.1f, 0.1f); // adds a random nudge
    float speed = GetSpeedForElement(element) * Random.Range(0.9f, 1.1f); // small speed drift

    animator.Play(stateName, 0, Mathf.Repeat(offset, 1f));
    animator.speed = speed;
}


    float GetOffsetForElement(ElementType type)
    {
        switch (type)
        {
            case ElementType.Fire: return 0.0f;
            case ElementType.Water: return 0.33f;
            case ElementType.Nature: return 0.66f;
            default: return Random.Range(0f, 1f); // fallback
        }
    }

    float GetSpeedForElement(ElementType type)
    {
        switch (type)
        {
            case ElementType.Fire: return 1.2f;    // faster, more energetic
            case ElementType.Water: return 1.0f;   // smooth, natural pace
            case ElementType.Nature: return 0.85f; // slower, more grounded
            default: return 1.0f;
        }
    }
}
