using UnityEngine;

namespace Stats
{
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private ProgressModel health;
    [SerializeField] private int startingMaxHp;

    [SerializeField] private ProgressModel mana;
    [SerializeField] private int startingMaxMp;

    [SerializeField] private ProgressModel exp;
    [SerializeField] private int levelReq;


    [SerializeField] private ImageFillPresenter healthPresenter;
    [SerializeField] private ImageFillPresenter manaPresenter;
    [SerializeField] private ImageFillPresenter xpPresenter;

    void Awake()
    {
        health = CreateUIFrame(healthPresenter, startingMaxHp);
        mana = CreateUIFrame(manaPresenter, startingMaxMp);
        exp = CreateUIFrame(xpPresenter, levelReq, false);

        //Initiates the 3 Main Player Stats
    }

    ProgressModel CreateUIFrame(ImageFillPresenter presenter, int max, bool startingFull = true)
    {
        ProgressModel model = new ProgressModel(max, startingFull);

        presenter.Initialise(model);
        model.ChangeValue(0);
        return model;
    }




    public void TakeDamage(int damage)
    {
        health.ChangeValue(-damage);
    }

    public void Heal(int amount)
    {
        health.ChangeValue(amount);
    }

    public void GainXP(int amount)
    {
        exp.ChangeValue(amount);
    }

    public void RestoreMana(int amount)
    {
        mana.ChangeValue(amount);
    }
}
}