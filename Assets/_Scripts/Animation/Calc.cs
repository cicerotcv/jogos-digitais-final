public class Calc
{
    public static bool Around(float value, float target, float thresh)
    {
        return target - thresh < value && value < target + thresh;
    }
}
