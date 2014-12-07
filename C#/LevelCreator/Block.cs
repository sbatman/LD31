namespace LevelCreator
{
    public class Block
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int T { get; set; }

        public override string ToString()
        {
            return X.ToString() + " " + Y.ToString() + " " + Z.ToString() + " " + T.ToString();
        }
    }
}
