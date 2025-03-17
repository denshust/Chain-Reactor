using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourceDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject draggingIcon;
    private RectTransform dragRectTransform;
    private Generator sourceGenerator;
    public float resourceAmount = 10f; // Amount of resource to transfer

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Check if the source is from the output tank before starting the drag
        if (GetComponent<Image>().tag == "OutputTank")
        {
            // Create a visual representation of the resource being dragged
            draggingIcon = new GameObject("DraggingIcon");
            draggingIcon.transform.SetParent(transform.root, false);
            dragRectTransform = draggingIcon.AddComponent<RectTransform>();
            dragRectTransform.sizeDelta = new Vector2(50, 50);
            draggingIcon.AddComponent<CanvasRenderer>();
            draggingIcon.AddComponent<Image>().color = Color.green; // Visual cue for the resource
            sourceGenerator = GetComponentInParent<Generator>();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingIcon != null)
        {
            dragRectTransform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingIcon != null)
        {
            Destroy(draggingIcon);

            // Ensure the drag ends over an input tank image
            GameObject targetObject = eventData.pointerEnter;
            if (targetObject != null && targetObject.tag == "InputTank" && targetObject.GetComponentInParent<Generator>() != null)
            {
                Generator targetGenerator = targetObject.GetComponentInParent<Generator>();
                TransferResource(sourceGenerator, targetGenerator);
            }
        }
    }

    private void TransferResource(Generator fromGenerator, Generator toGenerator)
    {
        // Ensure that the output resource type of the source matches the input resource type of the target
        if (fromGenerator.outputResourceType == toGenerator.inputResourceType && fromGenerator.outputResource >= resourceAmount)
        {
            fromGenerator.outputResource -= resourceAmount;
            toGenerator.inputResource += resourceAmount;
        }
    }
}
