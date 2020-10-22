﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    public GamePlayManager GameplayManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mario"))
        {
            GameplayManager.RestartLevel();
        }
    }

}
