using System;

namespace Stats{
    [Serializable]
public class ProgressModel
{
    public event Action<int, int> onValueChanged;
    public event Action onEmpty;
    public event Action onFull;

    public int currentVal { get; private set; }
    public int maxVal { get; private set; }


    public ProgressModel(int max, bool StartFull = true)
    {
        maxVal = max;
        currentVal = StartFull ? max : 0;


    }

    public void ChangeValue(int amount) //Any Modifications can be calling this instead of hard coding the logic
    {
        int previousVal = currentVal;
        currentVal += amount;
        currentVal = Math.Clamp(currentVal, 0, maxVal);

        onValueChanged?.Invoke(currentVal, maxVal);

        if (previousVal > 0 && currentVal == 0) onEmpty?.Invoke();


        if (previousVal < maxVal && currentVal == maxVal) onFull?.Invoke();

    }

    public void Reset(bool resetToZero) //Allows for easier use in parameters like HP and XP
    {
        currentVal = resetToZero ? 0 : maxVal;
        onValueChanged?.Invoke(currentVal, maxVal);
    }
    public void SetNewMax(int newMax)
    {
        maxVal = newMax;
        onValueChanged?.Invoke(currentVal, maxVal);
    }
}}