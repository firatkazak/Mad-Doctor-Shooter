using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;// instance ad�nda public ve static olacak �ekilde EnemySpawner t�rettik.
    //Mant�k: Ayn� anda 1'den fazla Enemy spawnlanmas�n diye yapt�k.
    public GameObject enemyPrefab;//D��man prefab�n� eklemek i�in.
    private GameObject newEnemy;//Yeni d��man i�in.
    public Transform[] spawnPosition;//Spawnlanacak pozisyon i�in.
    private List<GameObject> spawnEnemies = new List<GameObject>();//D��manlar�n tutulaca�� liste.
    private int enemySpawnLimit = 10;//Spawnlanacak d��man limiti. Ekranda en fazla 10 d��man olabilecek.
    private float minSpawnTime = 2f, maxSpawnTime = 5f;//Minimum 2, Maksimum 5 saniyede 1 d��man spawnlanacak.
    private void Awake()
    {
        if (instance == null)//E�er instance null ise
            instance = this;// O zaman instance budur.
        //Burada instance'a referans vermi� olduk.
    }
    private void Start()
    {
        Invoke("SpawnEnemy", Random.Range(minSpawnTime, maxSpawnTime));//Oyun ba�lad���nda 2 ile 5 sn aras�nda d��man spawnlanacak.
    }
    private void SpawnEnemy()
    {
        Invoke("SpawnEnemy", Random.Range(minSpawnTime, maxSpawnTime));//2 ile 5 sn aras�nda d��man spawnlanacak.
        if (spawnEnemies.Count == enemySpawnLimit)//E�er spawnEnemies listesinin say�s�, maksimum d��man say�s�na e�it olursa (ikisi de 10)
            return;//d�nd�r. Mant�k hatas� olup oyun hata vermesin diye yapt�k.
        newEnemy = Instantiate(enemyPrefab, spawnPosition[Random.Range(0, spawnPosition.Length)].position, Quaternion.identity);
        //newEnemy ad�nda, yukar�da belirtilen pozisyon ve a��da d��man spawn et.
        spawnEnemies.Add(newEnemy);//1 �st sat�rda Instantiate edilen newEnemy'yi spawnEnemies listesine ekle.
    }
    public void EnemyDied(GameObject enemy)//D��man �ld���nde �al��acak fonksiyon.
    {
        spawnEnemies.Remove(enemy);//Bir d��man �ld���nde spawnEnemies listesinden eleman silinecek.
    }
}//Class EnemySpawner
