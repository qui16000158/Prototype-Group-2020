using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public float damage = 20f;
    public float delay = 1f;
    public float attackRadius = 1f;
    public float range = 2f;
    public int attackAnimation = 0;
    public GameObject weaponPrefab; // The prefab to instantiate when dropping/placed in hand
}
