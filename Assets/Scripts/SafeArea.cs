using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeArea : MonoBehaviour
{
    [SerializeField] GameObject ResultPanel;
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Charactor"))
        {
            if (!MainScripts.isGameOver)
            {
                GameOver();
            }
            Destroy(collision.gameObject); //上記コード実行後生成されたゲームオブジェクトを削除
        }
    }
    void GameOver()
    {
        //ゲームオーバー処理
        ResultPanel.SetActive(true); //パネル表示を出現へ
        MainScripts.isGameOver = true; //ゲームオーバーフラグをオンに

        //GameOverのパネルを表示した時に、Characterタグを持っているGameObjectを削除
        DestroyAllCharactars();
    }
    public void ResltLogic()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void DestroyAllCharactars()
    {
        
    }
}
