using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

    private void Start()
    {
        //Chamando o método RandomizePosition() no Start que procura a área da grid
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        //Identificando o tamanho total de área do projeto através de bounds
        Bounds bounds = this.gridArea.bounds;

        //Random Range pegando as partes limetes do bounds, tanto o máximo quanto o mínimo, para que a comida sempre apareça na tela
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        //A execução do posicionamento de cada "Food" spawnada aleatóriamente
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        this.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Chamando o método RandomizePosition() no Start que procura a área da grid
        RandomizePosition();
    }

}
