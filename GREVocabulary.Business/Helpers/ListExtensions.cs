namespace GREVocabulary.Business.Helpers;

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static List<List<T>> SplitList<T>(this List<T> list, int parts)
    {
        List<List<T>> dividedList = new List<List<T>>();
        int chunkSize = list.Count / parts;
        int remainder = list.Count % parts;
        int index = 0;

        for (int i = 0; i < parts; i++)
        {
            int size = chunkSize;
            if (remainder > 0)
            {
                size++;
                remainder--;
            }

            dividedList.Add(list.GetRange(index, size));
            index += size;
        }

        return dividedList;
    }
}
