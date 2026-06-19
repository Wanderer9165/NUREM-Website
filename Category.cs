using System.Collections.Generic;

namespace NUREM.Menu
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } // Örn: Ana Yemekler, İçecekler

        // Bir kategorinin altında birden fazla ürün olabilir (Bire-Çok İlişki)
        public List<Product> Products { get; set; } = new List<Product>();
    }
}