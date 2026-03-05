using UnityEngine;
using UnityEngine.UI;

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

    //[SerializeField] private Image healthFill;
    //[SerializeField] private Image manaFill;
    //[SerializeField] private Image expFill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        health = CreateUIFrame(healthPresenter, startingMaxHp);
        mana = CreateUIFrame(manaPresenter, startingMaxMp);
        exp  = CreateUIFrame(xpPresenter, levelReq, false);
    }

    ProgressModel CreateUIFrame(ImageFillPresenter presenter, int max,bool startingFull = true)
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
