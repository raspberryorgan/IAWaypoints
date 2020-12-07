using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Guard : MonoBehaviour
{
    public Transform character;
    public float speed;
    public float shootDelay;
    public float analyzeTime;
    public List<Node> nodes;
    public float distanceToAvoid;
    public GameObject bulletPrefab;
    public Light viewLight;
    Seek seek;
    WaypointHandler wp;
    Rigidbody rb;
    float bulletTimer;
    float analyzeTimer;
    bool analyzing;
    Roulette _roulette;
    Dictionary<Action, int> _shootprob = new Dictionary<Action, int>();
    public void StartGuard()
    {
        _roulette = new Roulette();
        seek = new Seek(this.transform, character);
        rb = GetComponent<Rigidbody>();

        _shootprob.Add(() => ShootingBullet(Vector3.zero), 60);
        _shootprob.Add(() => ShootingBullet(Vector3.right * 1), 35);
        _shootprob.Add(() => ShootingBullet(Vector3.right * 1.5f + Vector3.up * 3), 5);

        wp = new WaypointHandler(nodes, distanceToAvoid);
    }
    public void MoveSeekPlayer()
    {
        viewLight.color = Color.red;
        Debug.Log("seeeeeeek");
        Vector3 dir = seek.GetDir();
        Debug.Log(dir);
        dir.y = 0f;
        rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
        transform.forward = Vector3.Slerp(transform.forward, dir, 0.5f);
    }
    public void MoveWaypointRoute()
    {
        viewLight.color = Color.cyan;
        Vector3 dir = wp.WaypDirection(transform.position);
        dir.y = 0f;
        rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
        transform.forward = Vector3.Slerp(transform.forward, dir, 0.5f);
    }
    public void Shoot()
    {
        viewLight.color = Color.red;
        _roulette.Execute(_shootprob).Invoke();

    }
    public void ShootingBullet(Vector3 dir)
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > shootDelay)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.forward = (character.position + dir) - transform.position;
            bulletTimer = 0f;
        }
    }
    public void StartAnalyze()
    {
        analyzing = true;
        analyzeTimer = 0f;
        Debug.Log("uwuanalyze");
    }
    public void Analyze()
    {
        analyzeTimer += Time.deltaTime;
        Debug.Log("owowow");
        if (analyzeTimer > analyzeTime)
        {
            analyzing = false;
        }
        //analyzebehaviour
    }
    public bool IsAnalyzing()
    {
        return analyzing;
    }


    public void SetPatrolCallback(Action callback)
    {
        wp.SubscribeEndCallback(callback);
    }
}