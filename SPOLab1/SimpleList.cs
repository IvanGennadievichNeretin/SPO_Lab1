using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOLab1
{
    class SimpleList
    {
        String[] List;
        int Size;
        int count;
        public int dupes;
        public SimpleList(String[] list)
        {
            int operations = 0;
            dupes = 0;
            Size = list.Length;
            List = new string[Size+500];
            for (int i = 0; i < Size; i++)
            {
                this.put(list[i], ref operations, ref dupes);
            }
            count = Size;
        }
        public bool IsItExist(String Value, ref int operations)
        {
            for (int i = 0; i < count; i++)
            {
                operations++;
                if (List[i] == Value)
                {
                    return true;
                }
            }
            return false;
        }

        public void put(String Value, ref int operations, ref int Dupes)
        {
            int oper2 = 0;
            //if (this.IsItExist(Value, ref oper2))
            //{
            //    Dupes++;
            //}
            //else
            //{
                List[count] = Value;
                count++;
                operations++;
            //}
        }
    }
}
