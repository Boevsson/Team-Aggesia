using RogueSharp.Random;

namespace SoftUniGame.Engine
{
    
    public class Global
    {      
        public static readonly IRandom Random = new DotNetRandom();
        public static readonly Camera Camera = new Camera();
        public static GameStates.States GameState { get; set; }
        public static CombatManager CombatManager;
        public static readonly int MapWidth = 50;
        public static readonly int MapHeight = 30;
        public static readonly int SpriteWidth = 64;
        public static readonly int SpriteHeight = 64;
       
    }
}
