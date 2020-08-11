﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using LitJson;

public class BehaviorData
{

    #region  BehaviorTree
    private Dictionary<string, BehaviorTreeData> _behaviorDic = new Dictionary<string, BehaviorTreeData>();
    public void LoadData(string fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        BehaviorTreeData data = LitJson.JsonMapper.ToObject<BehaviorTreeData>(textAsset.text);
        _behaviorDic[fileName] = data;
    }

    public BehaviorTreeData GetBehaviorInfo(string handleFile)
    {
        BehaviorTreeData skillHsmData = null;
        if (_behaviorDic.TryGetValue(handleFile, out skillHsmData))
        {
            return skillHsmData;
        }

        return skillHsmData;
    }
    #endregion

}
