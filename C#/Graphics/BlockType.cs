namespace LD31.Graphics
{
    /// <summary>
    /// Denotes what type a Block is. Different types will have different default colors.
    /// </summary>
    public enum BlockType
    {
        Default = 0, //Color 'Grey' by default   (R: 1,  G: 1,    B:   1)
        Ground = 1,  //Color 'Green' by default  (R: 0,  G: 255,  B:   0)
        Wall = 2,    //Color '???' by default    (R: ?,  G: ?,    B:   ?)
        Sky = 3      //Color 'Blue' by default   (R: 0,  G: 0,    B: 255)
    }
}
