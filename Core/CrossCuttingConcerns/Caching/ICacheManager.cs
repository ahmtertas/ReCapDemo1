using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    //farklı cache alternatifleri kullanabiliriz
    //bütün alternatif cache tekniklerini kullanmak için bunu yazıyoruz
    //herhangi bir teknolojiden bağımsız bir interface
    public interface ICacheManager
    {
        //her şeyin base'i object'dir. Duration--> cache süresi
        void Add(string key, object value,int duration);
        T Get<T>(string key);
        object Get(string key);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);

    }
}
