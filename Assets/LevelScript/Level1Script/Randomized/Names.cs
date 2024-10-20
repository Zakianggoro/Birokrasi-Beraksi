using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomizedNames", menuName = "Names")]
public class Names : ScriptableObject
{
    public string[] names;
    public string[] nik;
    public string[] tanggalLahir;
    public string[] alamat;
    public string[] request;

}
