#include <iostream>

int main()
{
	int a[3]; b[3];
	int res = 0;
	cin >> a[2] >> a[1] >> a[0] >> b[2] >> b[1] >> b[0];
	for (int i = 0; i < 3; i++)
	{
		int res1[3] = 0;
		int carry = 0;
		for (int j = 0; j < 3; j++)
		{
			res1[j] = b[i] * a[j] * 10 (j + 1);
		}
	}

	res = res[1] + res[2] + res[3];
	std::cout << res;
	
}