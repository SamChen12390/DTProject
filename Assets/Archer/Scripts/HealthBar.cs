using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    public Slider healthbarDisplay;
    public Character character;
    private float health = 100;
    private float minimumHealth = 0;
    private float maximumHealth = 100;
    private int lowHealth = 33;
    private int highHealth = 66;
    private int healthPercentage = 100;

    [Space]
    [Header("Healthbar Colors:")]
    public Image barImage;
    public Color highHealthColor = new Color(0.35f, 1f, 0.35f);
    public Color mediumHealthColor = new Color(0.9450285f, 1f, 0.4481132f);
    public Color lowHealthColor = new Color(1f, 0.259434f, 0.259434f);

    private void Start()
    {
        healthbarDisplay.minValue = minimumHealth;
        healthbarDisplay.maxValue = maximumHealth;

        UpdateHealth();
    }

    private void Update()
    {
        healthPercentage = int.Parse((Mathf.Round(maximumHealth * (health / 100f))).ToString());

        if (health < minimumHealth)
        {
            health = minimumHealth;
        }

        if (health > maximumHealth)
        {
            health = maximumHealth;
        }

        if (health < maximumHealth)
        {
            UpdateHealth();
        }

        transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void GainHealth(float amount)
    {
        health += amount;
        UpdateHealth();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        if (healthPercentage <= lowHealth && health >= minimumHealth && barImage.color != lowHealthColor)
        {
            ChangeHealthbarColor(lowHealthColor);
        }
        else if (healthPercentage <= highHealth && health > lowHealth)
        {
            float lerpedColorValue = (float.Parse(healthPercentage.ToString()) - 25) / 41;
            ChangeHealthbarColor(Color.Lerp(lowHealthColor, mediumHealthColor, lerpedColorValue));
        }
        else if (healthPercentage > highHealth && health <= maximumHealth)
        {
            float lerpedColorValue = (float.Parse(healthPercentage.ToString()) - 67) / 33;
            ChangeHealthbarColor(Color.Lerp(mediumHealthColor, highHealthColor, lerpedColorValue));
        }

        healthbarDisplay.value = health;
        character.maxHealth = health;
    }

    public void ChangeHealthbarColor(Color colorToChangeTo)
    {
        if (LayerMask.LayerToName(transform.parent.parent.gameObject.layer).Contains("Blue team"))
        {
            barImage.color = Color.cyan;
            health = 100;
        }
        else
        {
            barImage.color = colorToChangeTo;
        }
    }
}
