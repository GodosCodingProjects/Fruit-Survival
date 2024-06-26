
using System.Collections.Generic;

public class QttDict<T> : Dictionary<T, int>
{
    public void Add(T t)
    {
		if(ContainsKey(t))
		{
			++this[t];
		}
		else
		{
			base.Add(t, 1);
		}
	}

	public void Remove(T t)
	{
		if(this[t] <= 1)
		{
			base.Remove(t);
		}
		else
		{
			--this[t];
		}
	}
}
