[System.Serializable]
public class Vector3Serializable
{
    public float x, y, z;

    public Vector3Serializable(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3Serializable()
    {
        //Empty constructor method that allows to create an instance of this class without specicfying any parameters
    }



}
