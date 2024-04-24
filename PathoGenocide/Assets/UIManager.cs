using UnityEngine;
using TMPro;
using UnityEngine.UI; // Make sure you have the TextMeshPro namespace

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI expCountText;
    public TextMeshProUGUI tokenCountText;
    public TextMeshProUGUI levelText;
    public Slider experienceSlider;
    public Slider healthSlider;
    private int killCount = 0;
    // private int expCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealth(int health)
    {
        healthText.text = "Health: " + health;
        healthSlider.value = health;
    }

    public void UpdateKillCount()
    {
        killCount++;
        if (killCountText != null)
        {
            killCountText.text = "Kill Count: " + killCount;
        }
    }

    public void UpdateExperience(int experience)
    {
        if (expCountText != null)
        {
            expCountText.text = "EXP: " + experience;
            experienceSlider.value = experience;
        }
    }
    public void UpdateLevel(int level)
    {
        levelText.text = "" + level;
    }

    public void UpdateTokenCount(int DNATokenCount)
    {
        // expCount++;
        if (DNATokenCount != null)
        {
            tokenCountText.text = "DNA: " + DNATokenCount;

        }
    }

    public void UpdateExperienceBar(float currentExp, float maxExp)
    {
        experienceSlider.maxValue = maxExp;
        experienceSlider.value = currentExp;
    }
}
