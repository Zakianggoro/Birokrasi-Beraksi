using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomizedNames", menuName = "Names")]
public class Names : ScriptableObject
{
    [Header("Default/Domisili")]
    public string[] noSurat;
    public string[] names;
    public string[] nik;
    public string[] tanggalLahir;
    public string[] jenisKelamin;
    public string[] statusPerkawinan;
    public string[] pekerjaan;
    public string[] alamat;
    public string[] request;
    public string[] namaPemohon;

    [Header("Kehilangan")]
    public string[] tanggalSurat;
    public string[] barang;
    public string[] atasNama;
    public string[] atasNIK;

    [Header("Usaha")]
    public string[] jenisJasa;
    public string[] letakJasa;
}
