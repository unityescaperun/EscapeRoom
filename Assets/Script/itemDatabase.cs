using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 데이터베이스. 여기에 필요한 아이템들을 작성하여 적용한다.
public class itemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();

    void Start() {
        // 스테이지 1
        items.Add(new Item("Item1", "나무막대기", 1001, "나무막대기"));
        items.Add(new Item("Item2", "갈고리", 1002, "갈고리"));
        items.Add(new Item("Key2", "긴 막대기", 2003, "무언가를 헤집을 수 있을 것 같다."));
        items.Add(new Item("Key", "창문 열쇠", 2007, "창문의 열쇠와 비슷한가?."));
        items.Add(new Item("Item5", "열쇠", 2009, "빨리 탈출해야겟다."));

        // 스테이지 2
        // 요리재료 2개 독국뮬 요리 이상한 요리 키
        items.Add(new Item("Material1", "고기", 3001, "신선한 고기다."));
        items.Add(new Item("Material2", "버섯", 3002, "식용 버섯을 얻었다."));
        items.Add(new Item("Material3", "파프리카", 3003, "파프리카를 발견했다."));
        items.Add(new Item("Poison", "??", 3004, "알 수 없는 액체다."));
        items.Add(new Item("Food1", "완성된 요리", 3005, "맛있어 보이는 요리"));
        items.Add(new Item("Food2", "??? 요리", 6009, "뭔가 수상한 냄새가 난다."));
        items.Add(new Item("Stage2Key", "열쇠", 3007, "열쇠를 얻었다."));
    }
}
