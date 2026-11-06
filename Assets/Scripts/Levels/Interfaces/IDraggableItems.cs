using UnityEngine;

namespace Levels
{
    public interface IDraggableItems
    {
        void moveItem(Vector2 pos);
        void attachToParentObject();

        
    }
}