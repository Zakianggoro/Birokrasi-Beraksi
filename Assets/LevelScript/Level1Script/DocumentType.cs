using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DocumentType : MonoBehaviour
{
    public TextMeshProUGUI docType;
    public string document = "";

    public void PickDoc(int index)
    {
        switch (index)
        {
            case 0:
                docType.text = "-Tipe Surat-";
                document = "Null";
                break;
            case 1:
                docType.text = "Kehilangan";
                document = "Kehilangan";
                break;
            case 2:
                docType.text = "Domisili";
                document = "Domisili";
                break;
            case 3:
                docType.text = "Usaha";
                document = "Usaha";
                break;
            default:
                break;
        }
    }
}
