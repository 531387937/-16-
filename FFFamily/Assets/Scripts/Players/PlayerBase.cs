﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase
{
    public float weight;

    public float jumpForce;

    public float moveSpeed;

    public int playerNum;
    /// <summary>
    /// 0--A
    /// 1--B
    /// 2--X
    /// 3--Y
    /// 4--LB
    /// 5--RB
    /// </summary>
    public KeyCode[] Buttons
    {
        get
        {
            if (_button == null)
            {
                _button = new KeyCode[6];
                int keycode = 0;
                switch (playerNum)
                {
                    case 1:
                        keycode = (int)KeyCode.Joystick1Button0;
                        break;
                    case 2:
                        keycode = (int)KeyCode.Joystick2Button0;
                        break;
                    case 3:
                        keycode = (int)KeyCode.Joystick3Button0;
                        break;
                    case 4:
                        keycode = (int)KeyCode.Joystick4Button0;
                        break;
                }
                for (int i = 0; i < 6; i++)
                {
                    _button[i] = (KeyCode)keycode;
                }
            }
            return _button;
        }
    }

    public PlayerBase(int _playerNum)
    {
        playerNum = _playerNum;
    }

    private KeyCode[] _button = null;
    public virtual void InitPlayer()
    {

    }
    public virtual Vector3 Move()
    {
        Vector3 _moveDir = Vector3.zero;

        return _moveDir;
    }


}
