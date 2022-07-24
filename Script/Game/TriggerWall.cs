using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWall : MonoBehaviour
{
    [SerializeField] SettingsRoad settingsRoad;
    [SerializeField] MeshRenderer MyColor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerMove.inc.MyMaterial.color == MyColor.material.color)
                StartGame.inc.UpdateScore(1);
            else StartGame.inc.UpdateScore(-1);
            settingsRoad.DisableWall();
        }
    }
}
