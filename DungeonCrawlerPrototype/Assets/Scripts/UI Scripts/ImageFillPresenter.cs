using UnityEngine;
using UnityEngine.UI;

namespace Stats
{
public class ImageFillPresenter : ValuePresenter
{
    [SerializeField] Image FillImage;

    public void SetImage(Image fillUI)
    {
        FillImage = fillUI;
    }

    public void Initialise(ProgressModel model)
    {
        model.onValueChanged += Present;
    }

    public override void Present(int current, int max)
    {
        FillImage.fillAmount = (float)current / max;
    }
}
}