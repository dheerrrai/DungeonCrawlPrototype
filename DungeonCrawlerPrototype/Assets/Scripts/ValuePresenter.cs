using UnityEngine;

namespace Stats
{
public abstract class ValuePresenter : MonoBehaviour
{
    public abstract void Present(int current, int max);
}}