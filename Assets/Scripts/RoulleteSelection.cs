using System.Collections.Generic;

public static class RoulleteSelection
{
	public static int RoulleteWheelSelection(List<float> list)
    {
        float sum = 0;
        foreach (var Numero in list)
            sum += Numero;

        List<float> newValues = new List<float>();
        foreach (var Numero in list)
        {
            newValues.Add(Numero / sum);
        }

        float Rnd = UnityEngine.Random.Range(0f, 1f);

        float Sum2 = 0;
        for (int i = 0; i < newValues.Count; i++)
        {
            Sum2 += newValues[i];
            if (Sum2 > Rnd)
                return i;
        }

        return -1;
    }
}
