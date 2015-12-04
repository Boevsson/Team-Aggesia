using SoftUniGame.Models.Figures;
using SoftUniGame.Models.Figures.Enemies;
using SoftUniGame.Models.Figures.Players;
using RogueSharp.DiceNotation;
using System.Collections.Generic;
using System.Diagnostics;

namespace SoftUniGame.Engine
{
    public class CombatManager
    {
        private readonly Player _player;
        private readonly List<AggressiveEnemy> _aggressiveEnemies;

        // When we construct the CombatManager class we want to pass in references
        // to the player and the list of enemies.
        public CombatManager(Player player, List<AggressiveEnemy> aggressiveEnemies)
        {
            _player = player;
            _aggressiveEnemies = aggressiveEnemies;
        }

        // Use this method to resolve attacks between Figures
        public void Attack(Figure attacker, Figure defender)
        {
            // Create a twenty-sided die and
            // roll the dice, add the attack bonus, and compare to the defender's armor class
            if (Dice.Roll("d20") + attacker.AttackBonus >= defender.ArmorClass)
            {
                // Roll damage dice and sum them up
                int damage = attacker.Damage.Roll().Value;
                // Lower the defender's health by the amount of damage
                defender.Health -= damage;
                // Write a combat message to the debug log.
                // Later we'll add this to the game UI
                
                Trace.WriteLine($"{attacker.Name} hit {defender.Name} for {damage} and he has {defender.Health} health remaining.");
                  
                if (defender.Health <= 0)
                {
                    if (defender is AggressiveEnemy)
                    {
                        var enemy = defender as AggressiveEnemy;
                        // When an enemies health dropped below 0 they died
                        // Remove that enemy from the game
                        _aggressiveEnemies.Remove(enemy);
                    }
                    // Later we'll want to display this kill message in the UI
                    Trace.WriteLine($"{attacker.Name} killed {defender.Name}");
                }
            }
            else
            {
                // Show the miss message in the Debug log for now
                Trace.WriteLine($"{attacker.Name} missed {defender.Name}");
            }
        }

        // Helper method which returns the figure at a certain map cell
        public Figure FigureAt(int x, int y)
        {
            if (IsPlayerAt(x, y))
            {
                return _player;
            }
            return EnemyAt(x, y);
        }

        // Helper method for checking if the player is at a map cell
        public bool IsPlayerAt(int x, int y)
        {
            return (_player.X == x && _player.Y == y);
        }

        // Helper method for getting an enemy at a map cell
        public AggressiveEnemy EnemyAt(int x, int y)
        {
            foreach (var enemy in _aggressiveEnemies)
            {
                if (enemy.X == x && enemy.Y == y)
                {
                    return enemy;
                }
            }
            return null;
        }

        // Helper method for checking if an enemy is at a map cell
        public bool IsEnemyAt(int x, int y)
        {
            return EnemyAt(x, y) != null;
        }
    }
}
