using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public int hitPoints = 5;
    public bool visited = false;
    public GameObject[] platformChoices;
    private MaterialPropertyBlock propBlock;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        propBlock = new MaterialPropertyBlock();
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoints <= 0)
        {
            Destroy(GetComponent<BoxCollider>());
            Destroy(gameObject);
        }
    }

    public void visitThisPlatform(GameObject platform, GameObject player)
    {
        if (visited == false)
        {
            updateVisual();
            spawnNewPlatforms(platform, player);
            visited = true;
        }
    }
    private void spawnNewPlatforms(GameObject platform, GameObject player)
    {
        Vector3 pos = transform.position;
        Vector3 player_orientation = player.transform.forward.normalized;
        Vector3 next_pos = pos + player_orientation * 35;
        int choice_index = Random.Range(0, platformChoices.Length);
        GameObject paltform_choice = platformChoices[choice_index];
        GameObject next_platform1 = Instantiate(paltform_choice, next_pos, Quaternion.identity) as GameObject;
        next_platform1.GetComponent<PlatformManager>().platformChoices = platformChoices;
    }
    private void updateVisual()
    {
        rend.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_GlowStrength", 0.0f);
        rend.SetPropertyBlock(propBlock);
    }
}
