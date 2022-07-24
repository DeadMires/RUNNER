using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitchWay : MonoBehaviour
{
    [SerializeField] SettingsRoad road;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            road.SwitchWay();
            gameObject.SetActive(false);
        }
    }
}
