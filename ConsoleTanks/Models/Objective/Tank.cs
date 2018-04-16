/*  Tank
 *  
 *  An class
 *  that descibes
 *  objective tank
 */


using ConsoleTanks.Models.Abstract;
using ConsoleTanks.Behavior;
using System;

namespace ConsoleTanks.Models.Objective
{
    class Tank : Vechile, IFireble
    {
        private int id = 0;

        private int damage = 0;

        private char bulletSkin = '.';

        private DateTime lastFire = DateTime.Now;


        public event EventHandler<Bullet> OnShot = null;

        public override int GetHashCode()
        {
            return this.id;
        }

        public Tank(int id, int damage, char bulletSkin,int hp, char skin, ConsoleColor color) : base(hp,skin, color)
        {
            if(bulletSkin != ' ')
            {
                this.id = id;
                this.damage = damage;
                this.bulletSkin = bulletSkin;
            }
        }


        public char GetBulletSkin()
        {
            return this.bulletSkin;
        }


        #region IFireble

        public void Fire()
        {

            if(OnShot != null)
            {
                TimeSpan delta = DateTime.Now - lastFire;

                if (delta.Milliseconds >= 15)
                {

                    Bullet bullet = new Bullet(this.id, this.damage, this.bulletSkin, this.GetCurrentDirection(), this.GetColor());
                    OnShot(this, bullet);
                }
            }
        }

        public int GetDamage()
        {
            return this.damage;
        }


        #endregion

    }
}
