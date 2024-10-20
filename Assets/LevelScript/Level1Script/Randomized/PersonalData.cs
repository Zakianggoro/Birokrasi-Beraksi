using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersonalEntry
{
    public string bioName;
    public string bioNIK;
    public string bioTanggalLahir;
    public string bioAlamat;
    public string bioRequest;
}

[CreateAssetMenu(fileName = "PersonalDataList", menuName = "Biography/Personal Data List")]
public class PersonalData : ScriptableObject
{
    public List<PersonalEntry> personalEntries = new List<PersonalEntry>();
}
