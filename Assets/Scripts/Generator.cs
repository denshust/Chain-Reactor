using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    [Header("Tank Settings")]
    public Image inputTankFill;
    public Image outputTankFill;

    [Header("Resource Settings")]
    public ResourceType inputResourceType;
    public ResourceType outputResourceType;

    [Header("Resource Levels")]
    public float inputResource = 0f;
    public float outputResource = 0f;
    public float maxResource = 100f;

    [Header("Generation Settings")]
    public float generationRate = 2f;
    public float consumptionRate = 1f;

    void Update()
    {
        GenerateResources();
        UpdateTankVisuals();
    }

    private void GenerateResources()
    {
        // Consume from the input tank to produce output resources
        if (inputResource > 0)
        {
            inputResource -= consumptionRate * Time.deltaTime;
            outputResource += generationRate * Time.deltaTime;

            // Clamp values to max limits
            inputResource = Mathf.Clamp(inputResource, 0, maxResource);
            outputResource = Mathf.Clamp(outputResource, 0, maxResource);
        }
    }

    private void UpdateTankVisuals()
    {
        // Update the visual representation of the tanks based on resource levels
        inputTankFill.fillAmount = inputResource / maxResource;
        outputTankFill.fillAmount = outputResource / maxResource;
    }
}
