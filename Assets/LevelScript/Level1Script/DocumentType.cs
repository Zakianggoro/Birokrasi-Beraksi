using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DocumentType : MonoBehaviour
{
    public TextMeshProUGUI docType;

    public void PickDoc(int index)
    {
        switch (index)
        {
            case 0:
                docType.text = "-Tipe Surat-";
                break;
            case 1:
                docType.text = "Kehilangan";
                break;
            case 2:
                docType.text = "Domisili";
                break;
            case 3:
                docType.text = "Usaha";
                break;
        }
    }
}
