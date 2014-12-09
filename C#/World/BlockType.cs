namespace LD31.World
{
    /// <summary>
    /// Denotes what type a Block is. Different types will have different default colors.
    /// </summary>
    public enum BlockType
    {
        DEFAULT = 0, //Color 'Grey' by default   (R: 1,  G: 1,    B:   1)
        GROUND = 1,  //Color 'Green' by default  (R: 0,  G: 255,  B:   0)
        WALL = 2,    //Color '???' by default    (R: ?,  G: ?,    B:   ?)
        SKY = 3      //Color 'Blue' by default   (R: 0,  G: 0,    B: 255)
    }


}
