using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace NullHeroFix
{   

    internal class NullHeroFixHolder
    {
        public readonly MBReadOnlyList<ItemObject> items;
        public ItemObject null_butter;
        public readonly MBReadOnlyList<CraftingPiece> pieces;
        public readonly CraftingPiece handle;
        public readonly CraftingPiece guard;
        public readonly CraftingPiece blade;
        public readonly CraftingPiece pommel;
        public WeaponDesignElement[] elements;

        public NullHeroFixHolder(Game game)
        {
            this.items = game.ObjectManager.GetObjectTypeList<ItemObject>();
            this.null_butter = items.Where(item => item.StringId.Equals("peasant_maul_t1")).First();
            this.pieces = CraftingPiece.All;
            this.handle = pieces.Where(piece => piece.StringId.Equals("Handle")).First();
            this.guard = pieces.Where(piece => piece.StringId.Equals("Guard")).First();
            this.blade = pieces.Where(piece => piece.StringId.Equals("Blade")).First();
            this.pommel = pieces.Where(piece => piece.StringId.Equals("Pommel")).First();
            this.elements = new WeaponDesignElement[4];
            this.elements[0] = WeaponDesignElement.CreateUsablePiece(this.handle);
            this.elements[1] = WeaponDesignElement.CreateUsablePiece(this.guard);
            this.elements[2] = WeaponDesignElement.CreateUsablePiece(this.blade);
            this.elements[3] = WeaponDesignElement.CreateUsablePiece(this.pommel);
        }

    }
}
