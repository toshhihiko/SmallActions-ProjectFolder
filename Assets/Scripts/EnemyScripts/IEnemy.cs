using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public int id {get; set;}
    public string type {get; set;}
    public int HP {get; set;}
    public int power{get; set;}
}
