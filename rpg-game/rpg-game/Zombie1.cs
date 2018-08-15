using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace rpg_game
{
    class Zombie1 : Monster
    {
        private Dir direction = Dir.Down;

        public AnimatedSprite anim;
        public AnimatedSprite[] animations = new AnimatedSprite[4];
        public Zombie1(Vector2 nextMonsterPos) : base(nextMonsterPos)
        {
            currentSpeed = 20;
            monsterRad = 42;
            currentHealth = 5;

        }

    }
}
