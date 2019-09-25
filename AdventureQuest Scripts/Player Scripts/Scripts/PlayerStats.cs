using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerStats: MonoBehaviour
{
    #region Sigleton
    private static PlayerHealth instance;
    public static PlayerHealth Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerHealth>();
            return instance;
        }
    }
    #endregion

    //XP System
    [SerializeField] private XPStat xpBar;
    [SerializeField] private Text levelText;
    [SerializeField] private int level;
    [SerializeField] public int baseXP;

    private void Start()
    {
        xpBar.Initialize(0, Mathf.Floor(100 * MyLevel * Mathf.Pow(MyLevel, 0.5f)));
        levelText.text = MyLevel.ToString(); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GainXP(baseXP);
        }
        baseXP = (MyLevel * 5) + 45;
    }

    public int MyLevel
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public void GainXP(int xp)
    {
        xpBar.MyCurrentValue += xp;

        if (xpBar.MyCurrentValue >= xpBar.MyMaxValue)
        {
            StartCoroutine(GainLevel());
        }
    }

    private IEnumerator GainLevel()
    {
        while (!xpBar.IsFull)
        {
            yield return null;
        }

        MyLevel++;
        levelText.text = MyLevel.ToString();
        xpBar.MyMaxValue = 100 * MyLevel * Mathf.Pow(MyLevel, 0.5f);
        xpBar.MyMaxValue = Mathf.Floor(xpBar.MyMaxValue);
        xpBar.MyCurrentValue = xpBar.MyOverflow;
        xpBar.Reset();
    }
}
