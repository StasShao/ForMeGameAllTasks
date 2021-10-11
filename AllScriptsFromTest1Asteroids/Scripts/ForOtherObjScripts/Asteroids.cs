using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Asteroids : MonoBehaviour
{   
    [SerializeField] private float rotateSpeed; // Поле Числовой переменной , отвечает за скорость вращения
   
    float gravity; // числовая переменная , отвечает за гравитацию
    
    
    public AudioSource aSource; // Публичная переменная AudioSource
    public AudioClip[] aClip; // Публичная переменная AudioClip
    public Transform boom; // Публичная переменная Transform используется для добавления эффекта взрыва
    Vector2 pos1; // Переменная Vector2
    Vector2 pos2; // Переменная Vector2

    public Transform[] minAsteroids; // Массив Публичная переменная Transform используется для добавления мелких астероидов
    public Transform[] midAsteroids; // Массив Публичная переменная Transform используется для добавления средних астероидов
    

    Rigidbody2D _rb; // Переменная Rigidbody2D
    
    
    void Start()
    {
        rotateSpeed = Random.Range(-3,3); // скорость вращения в старте получает , случайно сгенерированое число от -3 до 3
        gravity = Random.Range(0.5f,2f); // генерируем случайное число для гравитации
       _rb =  GetComponent<Rigidbody2D>(); // инициализируем переменную Rigidbody
       aSource = GameObject.FindGameObjectWithTag("SpawnBeginer").GetComponent<AudioSource>(); // находим объект на сцене с тегом и берем у него компонент AudioSource
       aClip[0] = aSource.clip; // используем AudioClip из массива в AudioSource , найденного объекта
      
    }

    
    void Update()
    {
        
        _rb.AddTorque(1f * rotateSpeed); // прилогаем силу вращения , слугайно сгенерированным числом для Rigidbody
        _rb.gravityScale = gravity; // устанавливаем гравитацию для Rigidbody , можно регулировать из Inspector 
        pos1 = new Vector2(transform.position.x - 0.5f, transform.position.y); // устанавливаем положение по Vector2 слева и справа от данного объекта каждый кадр
        pos2 = new Vector2(transform.position.x + 0.5f, transform.position.y); // устанавливаем положение по Vector2 слева и справа от данного объекта каждый кадр
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
      if(col.gameObject.tag == "Player") // условие при котором объект соприкосновения имеет тег Player
      {
           aClip[1] = aSource.clip; // используем AudioClip из массива под индексом 1
           aSource.PlayOneShot(aClip[1]); // проигрываем AudioClip 1 раз
      } 
     
        if(col.gameObject.tag == "Bullet" || col.gameObject.tag == "BulletNlo") // условие при котором объект соприкосновения имеет тег Bullet или BulletNlo
        {
              aClip[0] = aSource.clip; // используем AudioClip из массива под индексом 0
              aSource.PlayOneShot(aClip[0]); // проигрываем AudioClip 1 раз
              Destroy(col.gameObject); // уничтожаем объект соприкосновения
              
              Instantiate(boom, transform.position, Quaternion.identity); // Транслируем Transform объекта (взрыв эффект) на место текущего объекта с ориентацией Quaternion.identity
              if(gameObject.tag == "AsterMax") // условие при котором объект соприкосновения имеет тег AsterMax
            {
            Instantiate(midAsteroids[0],pos1, Quaternion.identity); // Транслируем Transform объекта (сердний астероид) на место pos1(слева от текущего объекта) с ориентацией Quaternion.identity
            Instantiate(midAsteroids[1],pos2, Quaternion.identity); // Транслируем Transform объекта (взрыв эффект) на место pos2(справа от текущего объекта) с ориентацией Quaternion.identity
            }
            if(gameObject.tag == "AsterM") // условие при котором объект соприкосновения имеет тег AsterM
            {
            Instantiate(minAsteroids[0],pos1, Quaternion.identity); // Транслируем Transform объекта (мелкий астероид) на место pos1(слева от текущего объекта) с ориентацией Quaternion.identity
            Instantiate(minAsteroids[1],pos2, Quaternion.identity); // Транслируем Transform объекта (мелкий астероид) на место pos1(слева от текущего объекта) с ориентацией Quaternion.identity
                                       
            }
              Destroy(gameObject); // уничтожаем объект соприкосновения
        }
    }
}
