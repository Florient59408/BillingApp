using InvoiceApp.BO;
using SerializationLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.DAL
{
    public class Repository
    {
        protected List<Product> datas;
        protected readonly string PATH;
        private Serializer<List<Product>> serializer;
        public Repository(string clientName, int phoneNumber)
        {
            datas = new List<Product>();
            PATH = $"Data/{clientName+'_'+phoneNumber+'_'+DateTime.Now.ToString("-dd-MM-yy-hh-mm-ss")}.json";
            FileInfo fi = new FileInfo(PATH);

            if (!fi.Directory.Exists)
                fi.Directory.Create();

            serializer = new Serializer<List<Product>>(Mode.JSON, PATH, Format.Indented);
            Restore();
        }

        public int IndexOf(Product Obj)
        {
            var index = -1;
            for (int i = 0; i < datas.Count; i++)
                if (Obj.Equals(datas[i]))
                    index = i;
            return index;
        }

        public void Add(Product obj)
        {
            int index = IndexOf(obj);
            if (index != -1)
                throw new DuplicateWaitObjectException($"{typeof(Product).Name} already exists !");

            datas.Add(obj);
            Save();
        }

        public void Set(Product oldObj, Product newObj)
        {
            int oldIndex = IndexOf(oldObj);
            if (oldIndex < 0)
                throw new KeyNotFoundException($"{typeof(Product).Name} not found !");

            var newIndex = IndexOf(newObj);

            if (newIndex >= 0 && newIndex != oldIndex)
                throw new KeyNotFoundException($"{typeof(Product).Name} already exists !");

            datas[oldIndex] = newObj;
            Save();
        }

        public void Delete(Product obj)
        {
            var index = IndexOf(obj);

            if (index >= 0)
                datas.RemoveAt(index);
            Save();
        }

        public void Save()
        {
            serializer.Serialize(datas);
        }

        public void Restore()
        {
            FileInfo fi = new FileInfo(PATH);
            if (fi.Exists && fi.Length > 0)
                datas = serializer.Deserialize();
        }

        public List<Product> GetAll()
        {
            Restore();
            Product[] items = new Product[datas.Count];
            datas.CopyTo(items);
            return items.ToList<Product>();
        }

    }
}
