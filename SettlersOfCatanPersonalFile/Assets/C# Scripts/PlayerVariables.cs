using UnityEngine;
using System.Collections.Generic;

public class PlayerVariables
{

    //Each list order = wood, brick, ore, wheat, sheep, VP
    public List<int>[] playersInfo = {new List<int>(6), new List<int>(6), new List<int>(6)};

    //index 0 = player 1, 1 = player 2, 2 = player 3
    //Each Vector = resourceNum, number on tile, give amount
    public List<GameObject>[] playersSettlements = {new List<GameObject>(), new List<GameObject>(), new List<GameObject>()};
    public List<Vector3>[] playersResTiles = {new List<Vector3>(), new List<Vector3>(), new List<Vector3>()};
    public List<GameObject>[] playersRoadAccess = {new List<GameObject>(), new List<GameObject>(), new List<GameObject>()};
    public List<GameObject>[] playersSettlementAccess = { new List<GameObject>(), new List<GameObject>(), new List<GameObject>() };

    public int currentPlayer;
    
}

