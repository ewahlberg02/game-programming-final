using System.Collections;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBox;
    float direction;
    Vector3 moveVector;
    public void initialize(int damage){
        textBox.text = damage.ToString();
        direction = Random.Range(0, Mathf.PI);
        moveVector = new Vector3(1, direction, 0);
        StartCoroutine(decay());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveVector * Time.deltaTime * 0.5f;
    }

    private IEnumerator decay(){
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
