namespace task2 {
    enum Material
    {
        brick, wood, concrete, rock, metal
    }
    class MaterialsPrice
    {
        public static readonly int[] prices = {800, 500, 250, 640, 1000};
        public static int getPrice(Material mat)
        {
            return prices[(int) mat];
        }
    }
}