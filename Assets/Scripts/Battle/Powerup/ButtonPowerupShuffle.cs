using Dislands;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPowerupShuffle : ButtonPowerup
{
    public override void OnClick()
    {
        var cards = CardManager.Instance.cards;
        List<Vector2Int> positions = new List<Vector2Int>();
        var listPos = new List<Vector2Int>();
        int heroIndex = cards.IndexOf(CardManager.Instance.heroCard);
        for (int i = 0; i < cards.Count; i++)
        {
            listPos.Add(cards[i].Pos);
        }
        listPos.Remove(CardManager.Instance.heroCard.Pos);
        listPos.Shuffle();



        List<Vector2Int> newPositions = new List<Vector2Int>();

        int index = -1;
        for (int i = 0; i < cards.Count; i++)
        {
            if (i != heroIndex)
            {
                index++;
                newPositions.Add(listPos[index]);
            } else
            {
                newPositions.Add(CardManager.Instance.heroCard.Pos);
            }
        }
        for (int i = 0; i < newPositions.Count; i++)
        {
            cards[i].MoveToPos(newPositions[i]);
        }

        //var list = new List<Card>();
        //list.AddRange(cards);
        //list.Remove(CardManager.Instance.heroCard);
        //var listPos = new List<Vector2Int>();
        //for (int i = 0; i < cards.Count; i++)
        //{
        //    if (cards[i] != CardManager.Instance.heroCard)
        //    {
        //        listPos.Add(cards[i].Pos);
        //    }
        //}
        //listPos.Shuffle();
        //for (int i = 0; i < list.Count; i++)
        //{
        //    list[i].MoveToPos(listPos[i]);
        //}
    }
    //static void Main(string[] args)
    //{
    //    List<Vector2Int> list = new List<Vector2Int>()
    //    {
    //        new Vector2Int(1, 1),
    //        new Vector2Int(2, 2),
    //        new Vector2Int(3, 3),
    //        new Vector2Int(4, 4),
    //        new Vector2Int(5, 5)
    //    };
    //    Vector2Int excludeIndex = new Vector2Int(0, 2); // Vector2Int chỉ định vị trí cần tránh khi xáo trộn

    //    ShuffleList(list, excludeIndex);

    //    Console.WriteLine("Danh sách sau khi xáo trộn:");
    //    foreach (Vector2Int vec in list)
    //    {
    //        Console.WriteLine("(" + vec.x + ", " + vec.y + ")");
    //    }
    //}

    static void ShuffleList<T>(List<T> list, Vector2Int excludeIndex)
    {
        System.Random rnd = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            Vector2Int currentIndex = new Vector2Int(n, k);
            if (currentIndex != excludeIndex)
            {
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}