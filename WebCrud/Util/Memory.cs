using System.Runtime.Caching;
using MemoryCache = System.Runtime.Caching.MemoryCache;

namespace Util
{
    public class Memory
    {
        private int _MemoryTimeout = 0;
        private int MemoryTimeout
        {
            get
            {                
                return (_MemoryTimeout < 0 ? 60 : 60000);
            }
        }        

        
        /// <summary>
        /// Insere um objeto ou arquivo na memória
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto</typeparam>
        /// <param name="key">Chave de identificação</param>
        /// <param name="value">Conteúdo ou caminho</param>
        public T SetMemoryItem<T>(string key, object value) { return SetMemoryItem<T>(key, value, MemoryTimeout); }

        /// <summary>
        /// Insere um objeto na memória
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto</typeparam>
        /// <param name="key">Chave de identificação</param>
        /// <param name="timeout">Tempo em que ficará no cash da memória</param>
        public T SetMemoryItem<T>(string key, object value, double timeout)
        {
            try
            {
                object content = MemoryCache.Default[key];

                if (content != null) { MemoryCache.Default.Remove(key); }

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddSeconds(timeout));                

                MemoryCache.Default.Set(key, value, policy);
                T obj = (T)MemoryCache.Default[key];
                if (obj == null) { return (T)value; }
                else { return obj; }               
            }
            catch { return default(T); }
        }

        /// <summary>
        /// Verifica de uma chave está presente no cach da memória
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto</typeparam>
        /// <param name="key">Chave de identificação</param>
        /// <returns>Conteúdo do Objeto</returns>
        public T GetMemoryItem<T>(string key)
        {
            try { return (T)MemoryCache.Default[key]; }
            catch { return default(T); }
        }

        /// <summary>
        /// Remove do cash da memória o conteúdo de uma chave
        /// </summary>
        /// <param name="key">Chave de identificação</param>
        public void RemoveMemoryItem(string key)
        {
            MemoryCache.Default.Remove(key);
        }
    }
}
