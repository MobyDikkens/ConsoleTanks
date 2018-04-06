/*  IFireble
 *  
 *  Provides fire(damage) behaviour to 
 *  an object
 */  

namespace ConsoleTanks.Behavior
{
    interface IFireble
    {
        void Fire();
        int GetDamage();
    }
}
