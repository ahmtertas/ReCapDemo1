using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 55)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            //reflectedtype--> ismini al yani şu demek örneğin:Business-Concrete-IProductService,
            //invocation.methodName ise metodun ismini verdi
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            //string join:virgülle ayrılanları listele                       //varsa ekle  yoksa null gönder
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            //cache'de ekli olarak ilgili metot işlemi duruyor mu
            if (_cacheManager.IsAdd(key))
            {
                //true ise zaten varmış ön bellekte geri dön 
                //invocation.ReturnValue--> return değeri cache'deki data olsun anlamına gelmektedir.
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            //ekli değil ise yani false ise ekleme işlemini yap 
            invocation.Proceed();
            //invocation.ReturnValue-->datayı veritabanından getir cache'te yok anlamına geliyor.
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
