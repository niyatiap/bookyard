using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication6.Dapper
{
    public interface IFileDashBoard
    {
        List<FileEbook> GetAll();
        FileEbook Find(int? id);
        FileEbook Add(FileEbook file);
        FileEbook Update(FileEbook file);
        void Remove(int id);
    }
}
