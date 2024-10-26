using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersonalEntry
{
    public string bioDocType;

    [Header("Default/Domisili")]
    public string bioNomorSurat;
    public string bioName;
    public string bioNIK;
    public string bioTanggalLahir;
    public string bioJenisKelamin;
    public string statusPerkawinan;
    public string pekerjaan;
    public string bioAlamat;
    public string bioPurpose;
    public string bioNamaPemohon;

    [Header("Kehilangan")]
    public string bioTanggalSurat;
    public string bioBarang;
    public string bioAtasNama;
    public string bioAtasNIK;

    [Header("Usaha")]
    public string bioJenisJasa;
    public string bioLetakJasa;

    [Header("Kronologi")]
    public string bioKronologi;
}

[CreateAssetMenu(fileName = "PersonalDataList", menuName = "Biography/Personal Data List")]
public class PersonalData : ScriptableObject
{
    public List<PersonalEntry> personalEntries = new List<PersonalEntry>();
}
