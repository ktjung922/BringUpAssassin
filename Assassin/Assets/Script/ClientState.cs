using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eStatus
{ 
    왕,
    왕비,
    왕자,
    공주,
    공작,
    후작,
    백작,
    자작,
    남작,
    상인,
    농부,
    노예
}
public class ClientState : MonoBehaviour
{
    public string sClientName;
    public float fReputation;
    public eStatus Status;
}
