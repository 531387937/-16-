﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlateControl : MonoBehaviour
{
    public float MaxAngle;
    public float test;

    public float rotSpeed;
    public float slideSpeed;
    private Quaternion targetRot;
    GameObject p;
    private Vector3 jumpAffect;
    private PlayerManager m_PlayerManager
    {
        get { return PlayerManager.Instance; }
    }
    void Awake() 
    {
        test = -10;
    }

    void Start()
    {
        p = gameObject.transform.Find("Players").gameObject;
    }

    
    void Update()
    {
        targetRot = WeightCore();

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot,rotSpeed * Time.deltaTime);

        p.transform.localPosition += slideSpeed * new Vector3(-transform.rotation.z, 0, transform.rotation.x)*Time.deltaTime;

        jumpAffect = Vector3.Slerp(jumpAffect, Vector3.zero, 1 * Time.deltaTime);
    }

    Quaternion WeightCore()
    {
        float angle;
        Vector3 Wdirection = new Vector3(0, 0, 1);
        List<Transform> players = new List<Transform>();
        //GameObject p = gameObject.transform.Find("Players").gameObject;
        foreach(Transform child in p.transform)
        {
            players.Add(child);
            //Debug.Log(child.gameObject.name);
        }

        for (int i = 0; i < players.Count; i++)
        {
            PlayerBase Pb;
            float mass = 1;
            if (m_PlayerManager._players.TryGetValue(players[i].gameObject, out Pb))
            {
                mass = Pb.weight;
            }
            Wdirection += new Vector3(players[i].gameObject.transform.localPosition.x, players[i].transform.localPosition.z, 0) * mass;
            //Debug.Log(players[i].gameObject + "(" + players[i].gameObject.transform.position.x+"," +players[i].gameObject.transform.position.z+ ")");
        }
        Wdirection += jumpAffect;
        angle = Mathf.Sqrt(Wdirection.x * Wdirection.x + Wdirection.y * Wdirection.y);
        Vector3 from = Vector3.up;
        Vector3 to = new Vector3(-Wdirection.x, test, Wdirection.y);
        //transform.rotation = Quaternion.FromToRotation(from, to);
        Vector3 Newrotation = Quaternion.FromToRotation(from, to).eulerAngles;
        Newrotation += new Vector3(0, 0, 180);
        Newrotation.y = 0;
        return Quaternion.Euler(Newrotation);
    }

    private void OnCollisionExit(Collision collision)
    {
        PlayerBase pb;
        if(m_PlayerManager._players.TryGetValue(collision.gameObject,out pb))
        {
            pb.state = playerState.Jump;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("Enter");
        PlayerBase pb;
        if (m_PlayerManager._players.TryGetValue(collision.gameObject, out pb))
        {
            pb.state = playerState.OnGround;
            jumpAffect += -collision.gameObject.GetComponent<Rigidbody>().velocity.y* 10 * new Vector3(collision.gameObject.transform.localPosition.x, collision.gameObject.transform.localPosition.z, 0);
        }
    }
}
