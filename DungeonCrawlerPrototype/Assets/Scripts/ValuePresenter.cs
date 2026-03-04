using UnityEngine;

public abstract class AbstractProgressPresenter : MonoBehaviour
{
    public abstract void Present(int current, int max);
}