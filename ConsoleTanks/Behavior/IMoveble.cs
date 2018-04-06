/*  IMoveble
 *  
 *  Descbybes object behavior 
 *  that can move
 */
 

namespace ConsoleTanks.Behavior
{
    interface IMoveble
    {
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        Directions GetCurrentDirection();
        event System.EventHandler<Directions> Moved;
    }


    public enum Directions
    {
        Left,
        Right,
        Down,
        Up
    }

}
