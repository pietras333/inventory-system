using System.Collections;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    [Header("Configuration")] [SerializeField]
    private float timeToShowTooltip = 1f;

    [SerializeField] private Transform tooltipPanel;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;

    private Coroutine tooltipCoroutine;
    private bool isHovering;

    private void Awake()
    {
        SetTooltipActivity(false);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        if (tooltipPanel.gameObject.activeSelf)
            tooltipPanel.transform.position = Input.mousePosition + new Vector3(10, -10, 0);
    }

    public void SetItemName(string name)
    {
        itemNameText.text = name;
    }

    public void SetItemDescription(string description)
    {
        itemDescriptionText.text = description;
    }

    public void SetTooltipActivity(bool active)
    {
        isHovering = active;

        if (active)
        {
            if (tooltipCoroutine == null)
                tooltipCoroutine = StartCoroutine(ShowTooltipWithDelay());
        }
        else
        {
            if (tooltipCoroutine != null)
            {
                StopCoroutine(tooltipCoroutine);
                tooltipCoroutine = null;
            }

            tooltipPanel.gameObject.SetActive(false);
        }
    }

    private IEnumerator ShowTooltipWithDelay()
    {
        var timer = 0f;

        while (timer < timeToShowTooltip)
        {
            // Cancel immediately if hover state is lost
            if (!isHovering)
            {
                tooltipCoroutine = null;
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        if (isHovering) // Double check before showing
            tooltipPanel.gameObject.SetActive(true);

        tooltipCoroutine = null;
    }
}
