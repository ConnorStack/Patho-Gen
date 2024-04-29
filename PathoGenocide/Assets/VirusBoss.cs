using UnityEngine;

public class Boss : MonoBehaviour
{
    public BossLeg[] legs;
    public int totalHealth = 200;
    public bool isVulnerable => CheckIfAllLegsDestroyed();

    void Update()
    {
        if (isVulnerable)
        {
            // Change behavior, make boss vulnerable or change attack patterns
        }
    }

    bool CheckIfAllLegsDestroyed()
    {
        foreach (var leg in legs)
        {
            if (leg.gameObject.activeSelf)
                return false;
        }
        return true;
    }
}