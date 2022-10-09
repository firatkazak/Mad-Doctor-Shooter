using UnityEngine;
using UnityEngine.UI;
public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;// instance adýnda public ve static olacak þekilde GamePlayController türettik.
    public Text enemyKillCountTxt;//Öldürdüðümüz düþman sayýsýný ekranda göstermek için Text tanýmladýk.
    private int enemyKillCount;//Öldürdüðümüz polis sayýsýný tutacak deðiþken.
    private void Awake()
    {
        if (instance == null)//Eðer instance null ise
            instance = this;//O zaman instance budur.
        //Burada instance'a referans vermiþ olduk.
    }
    public void EnemyKilled()
    {
        enemyKillCount++;//Düþman öldükçe buradaki sayý artacak. 1'den 2'ye 2'den 3'e...vb.
        enemyKillCountTxt.text = "Enemies Killed: " + enemyKillCount;
        //Ekranda öldürdüðümüz polis kadar Enemies Killed:16, 17, 18.. vb. yazacak.
    }
    public void RestartGame()
    {
        Invoke("Restart", 3f);//3 saniye sonra Restart fonksiyonunu oynat.
    }
    private void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");//GamePlay sahnesini yükle.
    }
}//Class GamePlayController
