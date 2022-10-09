using UnityEngine;
using UnityEngine.UI;
public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;// instance ad�nda public ve static olacak �ekilde GamePlayController t�rettik.
    public Text enemyKillCountTxt;//�ld�rd���m�z d��man say�s�n� ekranda g�stermek i�in Text tan�mlad�k.
    private int enemyKillCount;//�ld�rd���m�z polis say�s�n� tutacak de�i�ken.
    private void Awake()
    {
        if (instance == null)//E�er instance null ise
            instance = this;//O zaman instance budur.
        //Burada instance'a referans vermi� olduk.
    }
    public void EnemyKilled()
    {
        enemyKillCount++;//D��man �ld�k�e buradaki say� artacak. 1'den 2'ye 2'den 3'e...vb.
        enemyKillCountTxt.text = "Enemies Killed: " + enemyKillCount;
        //Ekranda �ld�rd���m�z polis kadar Enemies Killed:16, 17, 18.. vb. yazacak.
    }
    public void RestartGame()
    {
        Invoke("Restart", 3f);//3 saniye sonra Restart fonksiyonunu oynat.
    }
    private void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");//GamePlay sahnesini y�kle.
    }
}//Class GamePlayController
