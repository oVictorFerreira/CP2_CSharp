namespace CP2_CSharp.Models
{
    public static class Cardapio
    {
        public static readonly List<Produto> Produtos = new()
        {
            new Produto("Café Expresso", 6.00m),
            new Produto("Café com Leite", 8.50m),
            new Produto("Cappuccino", 10.00m),
            new Produto("Mocha", 12.00m),
            new Produto("Cold Brew", 11.00m),
            new Produto("Pão de Queijo", 5.50m),
            new Produto("Bolo de Cenoura (fatia)", 9.00m),
            new Produto("Sanduíche Natural", 14.00m)
        };
    }
}
