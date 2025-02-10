using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;

public class Utils
{
    public static NormalItem.eNormalType GetRandomNormalType()
    {
        Array values = Enum.GetValues(typeof(NormalItem.eNormalType));
        NormalItem.eNormalType result = (NormalItem.eNormalType)values.GetValue(URandom.Range(0, values.Length));

        return result;
    }

    public static NormalItem.eNormalType GetRandomNormalTypeExcept(NormalItem.eNormalType[] types)
    {
        List<NormalItem.eNormalType> list = Enum.GetValues(typeof(NormalItem.eNormalType)).Cast<NormalItem.eNormalType>().Except(types).ToList();

        int rnd = URandom.Range(0, list.Count);
        NormalItem.eNormalType result = list[rnd];

        return result;
    }
    
    public static NormalItem.eNormalType GetLeastAmountNormalTypeExcept(NormalItem.eNormalType[] types, Dictionary<NormalItem.eNormalType, int> typeCounts)
    {
        List<NormalItem.eNormalType> list = Enum.GetValues(typeof(NormalItem.eNormalType)).Cast<NormalItem.eNormalType>().Except(types).ToList();

        NormalItem.eNormalType result = list[0];
        int minCount = typeCounts[result];
        foreach (NormalItem.eNormalType type in list)
        {
            if (typeCounts[type] < minCount)
            {
                result = type;
                minCount = typeCounts[type];
            }
        }

        return result;
    }
}
