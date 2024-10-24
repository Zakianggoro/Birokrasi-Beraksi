using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaperLayoutSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown layoutDropdown; // Reference to the dropdown UI

    [Header("Document Surat Kehilangan")]
    [SerializeField] private GameObject kehilanganLayout; // Kehilangan layout
    [SerializeField] private TextMeshProUGUI kehilanganFlavourText; // Kehilangan flavour text

    [Header("Document Surat Default/Domisili")]
    [SerializeField] private GameObject domisiliLayout; // Domisili layout
    [SerializeField] private TextMeshProUGUI domisiliFlavourText; // Domisili flavour text

    [Header("Document Surat Usaha")]
    [SerializeField] private GameObject usahaLayout; // Usaha layout
    [SerializeField] private TextMeshProUGUI usahaFlavourText; // Usaha flavour text

    // Start is called before the first frame update
    void Start()
    {
        // Deactivate all layouts and texts at the start
        DeactivateAllLayoutsAndTexts();

        // Initially activate only the default layout
        SetLayout(0);

        // Add listener to handle dropdown value changes
        layoutDropdown.onValueChanged.AddListener(delegate { SetLayout(layoutDropdown.value); });
    }

    // Method to deactivate all layouts and their flavour texts
    private void DeactivateAllLayoutsAndTexts()
    {
        // Deactivate layouts
        kehilanganLayout.SetActive(false);
        domisiliLayout.SetActive(false);
        usahaLayout.SetActive(false);

        // Deactivate flavour texts
        if (kehilanganFlavourText != null) kehilanganFlavourText.gameObject.SetActive(false);
        if (domisiliFlavourText != null) domisiliFlavourText.gameObject.SetActive(false);
        if (usahaFlavourText != null) usahaFlavourText.gameObject.SetActive(false);
    }

    // Method to switch between paper layouts
    private void SetLayout(int index)
    {
        // Deactivate all layouts and texts first
        DeactivateAllLayoutsAndTexts();

        // Activate the selected layout and its corresponding flavour text
        switch (index)
        {
            case 0: // Tipe Surat (default)
                break;
            case 1: // Kehilangan
                domisiliLayout.SetActive(true);
                kehilanganLayout.SetActive(true);
                if (kehilanganFlavourText != null) kehilanganFlavourText.gameObject.SetActive(true);
                break;
            case 2: // Domisili
                domisiliLayout.SetActive(true);
                if (domisiliFlavourText != null) domisiliFlavourText.gameObject.SetActive(true);
                break;
            case 3: // Usaha
                domisiliLayout.SetActive(true);
                usahaLayout.SetActive(true);
                if (usahaFlavourText != null) usahaFlavourText.gameObject.SetActive(true);
                break;
        }
    }
}
