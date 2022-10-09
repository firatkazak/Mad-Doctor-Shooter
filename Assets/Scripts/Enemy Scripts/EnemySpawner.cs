using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;// instance adýnda public ve static olacak þekilde EnemySpawner türettik.
    //Mantýk: Ayný anda 1'den fazla Enemy spawnlanmasýn diye yaptýk.
    public GameObject enemyPrefab;//Düþman prefabýný eklemek için.
    private GameObject newEnemy;//Yeni düþman için.
    public Transform[] spawnPosition;//Spawnlanacak pozisyon için.
    private List<GameObject> spawnEnemies = new List<GameObject>();//Düþmanlarýn tutulacaðý liste.
    private int enemySpawnLimit = 10;//Spawnlanacak düþman limiti. Ekranda en fazla 10 düþman olabilecek.
    private float minSpawnTime = 2f, maxSpawnTime = 5f;//Minimum 2, Maksimum 5 saniyede 1 düþman spawnlanacak.
    private void Awake()
    {
        if (instance == null)//Eðer instance null ise
            instance = this;// O zaman instance budur.
        //Burada instance'a referans vermiþ olduk.
    }
    private void Start()
    {
        Invoke("SpawnEnemy", Random.Range(minSpawnTime, maxSpawnTime));//Oyun baþladýðýnda 2 ile 5 sn arasýnda düþman spawnlanacak.
    }
    private void SpawnEnemy()
    {
        Invoke("SpawnEnemy", Random.Range(minSpawnTime, maxSpawnTime));//2 ile 5 sn arasýnda düþman spawnlanacak.
        if (spawnEnemies.Count == enemySpawnLimit)//Eðer spawnEnemies listesinin sayýsý, maksimum düþman sayýsýna eþit olursa (ikisi de 10)
            return;//döndür. Mantýk hatasý olup oyun hata vermesin diye yaptýk.
        newEnemy = Instantiate(enemyPrefab, spawnPosition[Random.Range(0, spawnPosition.Length)].position, Quaternion.identity);
        //newEnemy adýnda, yukarýda belirtilen pozisyon ve açýda düþman spawn et.
        spawnEnemies.Add(newEnemy);//1 üst satýrda Instantiate edilen newEnemy'yi spawnEnemies listesine ekle.
    }
    public void EnemyDied(GameObject enemy)//Düþman öldüðünde çalýþacak fonksiyon.
    {
        spawnEnemies.Remove(enemy);//Bir düþman öldüðünde spawnEnemies listesinden eleman silinecek.
    }
}//Class EnemySpawner
