using UnityEngine;

public class GameOverScripts : MonoBehaviour
{
    [SerializeField] GameObject resultPanel;

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Charactor") && !MainScripts.isGameOver)
        {
            GammeOver();
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.1f); // 少し遅延させて削除
        }
    }

    void GammeOver()
    {
        //ゲームオーバー処理
        MainScripts.isGameOver = true;
        resultPanel.SetActive (true);
    }
}
