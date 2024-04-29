using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorseeCoins.Engine
{
    public class Portal2 : GameObject
    {
        public Portal2(Texture2D image) : base(image)
        {
        }

        public void CheckCollision(Player player)
        {
            if (_bounds.Intersects(player.Bounds))
            {
                player.Position = new Point(50, 75);
            }
        }
    }
}


