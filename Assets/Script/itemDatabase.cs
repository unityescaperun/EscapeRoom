using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 데이터베이스. 여기에 필요한 아이템들을 작성하여 적용한다.
public class itemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();

    void Start() {
        items.Add(new Item("Item1", "나무막대기", 1001, "나무막대기"));
        items.Add(new Item("Item2", "갈고리", 1002, "갈고리"));
        items.Add(new Item("Key2", "긴 막대기", 2003, "무언가를 헤집을 수 있을 것 같다."));
        items.Add(new Item("Key", "창문 열쇠", 2007, "창문의 열쇠와 비슷한가?."));
        items.Add(new Item("Item5", "열쇠", 2009, "빨리 탈출해야겟다."));
    }
}
