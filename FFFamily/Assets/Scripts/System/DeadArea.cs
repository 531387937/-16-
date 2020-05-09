﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class DeadArea : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int num = other.transform.root.GetComponent<APRController>().PlayerNum;
            EventManager.Instance.TriggerEvent("PlayerDead",num);
        }
        else
        {
            GameManager.Instance.audioManager.PlaySound("落水声");
        }
    }
}
