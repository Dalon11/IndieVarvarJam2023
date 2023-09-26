using Jam.Player.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTeleportPortal : MonoBehaviour
{
    [SerializeField] GameObject hero;
    [SerializeField] Transform nextPosition;


    private void OnTriggerEnter(Collider other)
    { 
        hero.transform.position = nextPosition.position;
    }
}
