using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MainScripts : MonoBehaviour
{
    public GameObject characters; //キャラクター設定
    bool isGene = false; //キャラクター生成のフラグ
    GameObject geneChara; //キャラクター生成の感覚調整用
    bool isInterval = false;
    bool isButonHover = false;
    bool isButtonReleasedOnBUtton = false;
    public static bool isGameOver = false;
    int score;
    public Text scoreText; //スコア表示用
    bool isGameStarTed = false; //ゲーム開始状態を管理
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
