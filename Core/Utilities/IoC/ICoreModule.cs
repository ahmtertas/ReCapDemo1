using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public interface ICoreModule
    {
        //genel bağımlılıkları yükleyecek o sebeple void bir metot yazıyoruz.
        void Load(IServiceCollection serviceCollection);
    }
}
