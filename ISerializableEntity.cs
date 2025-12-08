using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceProject
{
    //-------------------------------------------------------------------
    // 2. ІНТЕРФЕЙСИ та ООП
    public interface ISerializableEntity // Інтерфейс для класів, які можуть зберігатися в текст/файл.
    {
        string Serialize();
    }
}
