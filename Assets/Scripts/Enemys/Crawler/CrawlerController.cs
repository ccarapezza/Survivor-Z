using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrawlerController : EnemyBaseController
{
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        print("RESULTADO Suma:"+ SumaMatriz(m));
        print("RESULTADO Cuadrado:" + CuadradoMatriz(m));

        List<List<int>> arr = CuadradoMatriz(m);
        foreach (var item in arr)
        {
            printList(item);
        }

    }

    public void printList(IEnumerable items)
    {
        var list = "";
        foreach (object o in items)
        {
            list = list + " " + o;
        }
        print(list);
    }

    protected override void Dead()
    {
        base.Dead();
        Destroy(gameObject);
    }


    private List<List<int>> m = new List<List<int>>{
        new List<int>{ 1, 2, 3 },
        new List<int>{ 4, 5, 6 },
        new List<int>{ 9, 7, 5 },
        new List<int>{ 9, 7, 9 }
    };

    public int SumaMatriz(List<List<int>> matrix)
    {
        if (matrix.Count == 0) return 0;
        return SumarLista(matrix[0]) + SumaMatriz(matrix.GetRange(1, matrix.Count - 1));
    }

    int SumarLista(List<int> list)
    {
        if (list.Count == 0) return 0;
        return list[0] + SumarLista(list.GetRange(1, list.Count - 1));
    }

    List<int> CuadradoLista(List<int> list)
    {
        if (list.Count == 0) return list;

        int cuadrado = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        list = CuadradoLista(list);
        list.Add(cuadrado * cuadrado);

        return list;
    }

    List<List<int>> CuadradoMatriz(List<List<int>> matrix)
    {
        if (matrix.Count == 0) return matrix;

        List<int> cuadradoLista = matrix[matrix.Count - 1];
        matrix.RemoveAt(matrix.Count - 1);
        matrix = CuadradoMatriz(matrix);
        matrix.Add(CuadradoLista(cuadradoLista));

        return matrix;
    }
}