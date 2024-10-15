using UnityEngine;

[CreateAssetMenu(fileName = "Zombie", menuName = "Gameplay/ZombieData")]
public class ZombieData : ScriptableObject
{
    public int speed = 2;
    public int health = 10;
    public int points = 7;
    public int spawnChance = 60;
}
