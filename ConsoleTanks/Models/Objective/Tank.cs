﻿using ConsoleTanks.Models.Abstract;
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

        public Tank(int id, int damage, char bulletSkin,int hp, char skin) : base(hp,skin)
        {
            if(bulletSkin != ' ')
            {
                this.id = id;
                this.damage = damage;
                this.bulletSkin = bulletSkin;
            }
        }

        #region IFireble

        public void Fire()
        {

            if(OnShot != null)
            {
                TimeSpan delta = DateTime.Now - lastFire;

                if (delta.Milliseconds >= 5000)
                {

                    Bullet bullet = new Bullet(this.id, this.damage, this.GetCurrentDirection());
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
