using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    //Criação de uma lista Transform
    private List<Transform> _segments = new List<Transform>();
    //criação de um transform público, ou seja, para na unity escolher o prefeb que será utilizado como snake
    public Transform segmentPrefab;
    //Criação de um vetor de direção para toda a vez que iniciar ele spawnar para a direita
    public Vector2 direction = Vector2.right;
    //Criação de um int para determinar o tamanho do personagem
    public int initialSize = 4;
        
    //Quando o jogo inicia ele executa tudo o que está no Start()
    private void Start()
    {
        //Executando o método ResetState() no Start()
        ResetState();
    }
    
    //Após o jogo iniciado e o método start executado, no update, ele executo tudo que está nele 60 vezes por segundo
    private void Update()
    {
        //O controle do personagem, para identificar a direção que ele está apontando pelo X do plano cartesinado, ou seja, para cima ou para baixo
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                this.direction = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                this.direction = Vector2.down;
            }
        }
        //O controle do personagem, para identificar a direção que ele está apontando pelo Y do plano cartesinado, ou seja, para a esquerda ou para a direita
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                this.direction = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.direction = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        //Fazendo surgir os segmentos do corpo e a cabeça do snake através de uma lógica usando o for
        for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        //A execução do posicionamento de cada um dos segmentos e fazer a movimentação ao decorrer de cada segmento que ele for criando tanto X quanto Y
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;

        this.transform.position = new Vector2(x, y);
    }
    
    //Um método para ele aumentar o segmento, ou seja, toda vez que pegar um item o snake sofre uma adição no corpo
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    //Criação do método ResetState() para fazer um ciclo de estágio para quando o jogo acabar e ele começar de novo
    public void ResetState()
    {
        //Resetando a direção e a posição da snake
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        //For para destruir todos os segmentos e recomeçar
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        //Limpando a lista e adicionado o transform do snake á ela
        _segments.Clear();
        _segments.Add(this.transform);

        //Fazendo surgir o tamanho inicial quando recomeça através de um for
        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }

    //Criação do método OnTriggerEnter2D, ou seja, tudo oq entrar no colisor, executa-rá o código abaixo
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Quando colidir com um objeto com a tag 'Food', executar o método Grow()
        if (other.tag == "Food") {
            Grow();
        }
        //Se não quando colidir com um objeto com a tag 'Obstacle', executar o método ResetState()
        else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

}
