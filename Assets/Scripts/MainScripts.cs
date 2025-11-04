using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MainScripts : MonoBehaviour
{
    public GameObject[] characters; //キャラクター設定
    bool isGene = false; //キャラクター生成のフラグ
    GameObject geneChara; //キャラクター生成の感覚調整用
    bool isInterval = false;
    bool isButonHover = false;
    bool isButtonReleasedOnBUtton = false;
    public static bool isGameOver = false;
    int score;
    //public Text scoreText; //スコア表示用
    bool isGameStarTed = false; //ゲーム開始状態を管理

    [SerializeField] AudioClip fall_se;
    [SerializeField] AudioClip rotate_se;
    AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ゲーム開始時の初期化
        isGameOver = false; //ゲーム開始時はゲームオーバーではない
        isGene = false; //キャラクター生成フラグをリセット
        isInterval = false; //間隔制御フラグをリセット
        isButonHover = false; //ボタンのホバー状態をリセット

        audioSource = GetComponent<AudioSource>();

        score = 0; //スコア情報初期値

        isGameStarTed = false; //ゲームが始まったかのフラグ
        //scoreText.text = score.ToString();//スコアテキストの初期化
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームオーバーならこの先処理をさせない
        if (isGameOver)
        {
            return;
        }

        //キャラクターが生成されていないかつキャラが静止している場合
        if (!isGene && !isInterval && !CheckMove())
        {
            CreateCaractor(); //キャラクターを生成
            isGene = true;
            if (isGameStarTed)
                UpdateScore();
            else
                isGameStarTed = true;
        }
        else if (Input.GetMouseButton(0) && isGene)
        {
            //マウスの左ボタンが押されている間、キャラクターを移動させる(x座標のみ)
            float mousePositionX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            geneChara.transform.position = new Vector2(mousePositionX, transform.position.y);
        }
        else if (Input.GetMouseButtonUp(0) && isGene)
        {
            //マウスボタンを離したときにキャラクターを落下させる
            DropCharacter();
        }
    }

    void DropCharacter()
    {
        if (geneChara != null && isGene)
        {
            //物理挙動を有効にする
            geneChara.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            //落下音を再生
            audioSource.PlayOneShot(fall_se);

            //キャラクター生成フラグをリセット
            isGene = false;

            //間隔制御コルーチンを開始
            StartCoroutine(IntervalCoroutine());
        }
    }

    public void RotateCharacter()
    {
        if (isGene)
        {
            geneChara.transform.Rotate(0, 0, -30);//30度ずつ回転
            audioSource.PlayOneShot(rotate_se); //回転音を再生
            isButonHover = true; //ボタンから指を離した直後のフラグを立てる
        }
    }

    //マウスカーソルがボタンの上にあるかの状態を更新する
    public void IsButtonChange(bool isX)
    {
        isButonHover = isX; //ボタンの状態を変更
    }

    void CreateCaractor()
    {
        //回転せずにGameManagerの座標にランダムにキャラ生成
        geneChara = Instantiate(characters[Random.Range(0, characters.Length)],
        transform.position, Quaternion.identity);
        //物理挙動をさせない状態にする
        geneChara.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    IEnumerator IntervalCoroutine()
    {
        isInterval = true; //間隔制御フラグを立てる
        yield return new WaitForSeconds(1); //1秒待機
        isInterval = false; //間隔制御フラグをリセット
    }

    bool CheckMove()
    {
        //Characterタグのオブジェクトを取得
        GameObject[] characterObjects = GameObject.FindGameObjectsWithTag("Caractor");
        foreach (GameObject character in characterObjects)
        {
            //キャラクターの速度が0.001以上なら動いていると判断
            if (character.GetComponent<Rigidbody2D>().linearVelocity.magnitude > 0.001f)
            {
                return true; //キャラクターが動いている場合はtrue
            }
        }
        return false; //キャラクターが動いていない場合はfalse
    }

    void UpdateScore()
    {
        score++; //スコアを加算
        //scoreText.text = score.ToString(); //スコア情報を更新
        Debug.Log("現在のスコア" + score + "です");
    }
}