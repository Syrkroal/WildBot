using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Swarm : MonoBehaviour
{
    public int swarmCount = 20;
    public GameObject robotPrefab;
    public GameObject basicFlyingPrefab;
    private List<SwarmGuy> drones = new List<SwarmGuy>();
    private List<Patrol> enemies = new List<Patrol>();
    public Transform player;
    void Awake()
    {
        for (int i = 0; i < swarmCount; i++)
            AddRobot();
    }
    void FixedUpdate()
    {
        // Vector3 swarmCenter = SwarmCenterAverage();
        // Vector3 swarmMovement = SwarmMovementAverage();
        // foreach (SwarmGuy drone in drones) {
        //     drone.SetTargetPosition(swarmCenter, swarmMovement);
        //     // drone.SetPlayer(player.position);
        // }
        // foreach (Patrol enemy in enemies)
        //     enemy.SetPlayer(player);
    }
    private void AddBasicFlying()
    {
        GameObject newEnemy = (GameObject)Instantiate(robotPrefab, transform);
        Follow newFollow = newEnemy.GetComponent<Follow>();
        newFollow.SetPlayer(player);
    }
    private void AddRobot()
    {
        GameObject newDroneGO = (GameObject)Instantiate(basicFlyingPrefab, transform);
        SwarmGuy newDrone = newDroneGO.GetComponent<SwarmGuy>();
        Patrol newPatrol = newDroneGO.GetComponent<Patrol>();
        newPatrol.SetPlayer(player);
        drones.Add(newDrone);
        enemies.Add(newPatrol);
    }
    private Vector3 SwarmCenterAverage()
    {
        // cohesion (swarm center point)
        Vector3 locationTotal = Vector3.zero;
        foreach (SwarmGuy drone in drones)
            locationTotal += drone.GetComponent<Transform>().position;
        return (locationTotal / drones.Count);
    }
    private Vector3 SwarmMovementAverage()
    {
        // alignment (swarm direction average)
        Vector3 velocityTotal = Vector3.zero;
        foreach (SwarmGuy drone in drones)
            velocityTotal += drone.GetComponent<Rigidbody>().velocity;
        return (velocityTotal / drones.Count);
    }
}
