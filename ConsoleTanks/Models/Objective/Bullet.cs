/*  Bullet
 *  
 *  Basic bullet model
 */



using ConsoleTanks.Behavior;

namespace ConsoleTanks.Models.Objective
{
    class Bullet
    {
        private int id = 0;

        private int damage = 0;

        private Directions direction = Directions.Up;


        public Bullet(int id, int damage, Directions direction)
        {
            this.id = id;
            this.damage = damage;
            this.direction = direction;
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

    }
}
