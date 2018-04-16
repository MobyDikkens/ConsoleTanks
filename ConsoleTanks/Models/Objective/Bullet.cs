/*  Bullet
 *  
 *  Basic bullet model
 */



using ConsoleTanks.Behavior;
using System;

namespace ConsoleTanks.Models.Objective
{
    class Bullet
    {
        private int id = 0;

        private int damage = 0;

        private Directions direction = Directions.Up;

        private char skin = '*';

        private ConsoleColor color;


        public Bullet(int id, int damage, char skin, Directions direction, ConsoleColor color)
        {
            this.id = id;
            this.damage = damage;
            this.direction = direction;
            this.skin = skin;
            this.color = color;
        }
        
        public override int GetHashCode()
        {
            return this.id;
        }

        public int GetDamage()
        {
            return this.damage;
        }

        public Directions GetDirection()
        {
            return this.direction;
        }

        public char GetSkin()
        {
            return this.skin;
        }

        public ConsoleColor GetColor()
        {
            return this.color;
        }
    }
}
