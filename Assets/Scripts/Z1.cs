using UnityEngine;
using System.Collections;

public class Z1 : MonoBehaviour
{
    private GameObject z1;
    public static bool one;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        z1 = this.gameObject;
        StartCoroutine(FirstZ());
    }
 
    IEnumerator FirstZ()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            z1.transform.position = new Vector3(-0.0219999999f, 1.93700004f, 0);
            z1.transform.localScale = new Vector3(0.989868581f, 0.80773735f, 1);
            yield return new WaitForSeconds(0.5f);
            z1.transform.position = new Vector3(-0.337000012f, 2.73399997f, 0);
            z1.transform.localScale = new Vector3(1.40115905f, 1.14900637f, 1);
            yield return new WaitForSeconds(0.5f);
            z1.transform.position = new Vector3(0.181099996f, 1.34710002f, 0);
            z1.transform.localScale = new Vector3(0.743088782f, 0.599611998f, 1);

        }
        
    }
}
